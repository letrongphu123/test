using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace Lab03
{
    public partial class server4 : Form
    {
        //tao class Phim
        public class Phim
        {
            public string tenphim { get; set; }
            public double giatieuchuan { get; set; }
            public List<int> cacphong { get; set; } = new List<int>();
        }

        private TcpListener tcpListener;
        private bool isRuning = false;

        //su dung ConcurrentDictionary de quan ly danh sach client
        //key: dia chi EndPoint cua client(IP:Port), value: doi tuong tcp client tuong ung
        private ConcurrentDictionary<string, TcpClient> ds_client = new ConcurrentDictionary<string, TcpClient>();

        //luu du lieu film doc tu file
        private List<Phim> ds_Phim = new List<Phim>();

        //quan ly trang thai cua ghe ngoi
        private ConcurrentDictionary<string, bool> ghedadat = new ConcurrentDictionary<string, bool>();     //key: ten ghe; value: false (trong)
        private string filepath = "D:\\project_nt106\\Lab03\\input5.txt"; // file luu tru data phong ve
        public server4()
        {
            InitializeComponent();
            LoadDataFromFile(filepath);
        }

        //Ham doc du lieu tu file khi server khoi dong
        private void LoadDataFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (i + 2 < lines.Length)
                {
                    Phim film = new Phim();
                    film.tenphim = lines[i].Trim();
                    film.giatieuchuan = double.Parse(lines[i + 1].Trim());
                    string[] cacphong = lines[i + 2].Split(',');

                    foreach (var r in cacphong)
                        film.cacphong.Add(int.Parse(r.Trim()));

                    ds_Phim.Add(film);
                }
            }
        }

        private async Task HandleClient(TcpClient client, string clientkey)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];

            try
            {
                //khi client vua ket noi thi gui ngay danh sach phim va trang thai ghe ngoi
                SendInitialData(stream);

                while (isRuning && client.Connected)
                {
                    int bytesread = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesread == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesread).Trim();

                    //dinh dang goi tin dat ve
                    if (msg.StartsWith("BOOK|"))
                    {
                        string[] parts = msg.Split('|');
                        string movie = parts[1];
                        string room = parts[2];
                        string[] seats = parts[3].Split(',');
                        string customer = parts[4];

                        //danh dau ghe da dat
                        bool ghedaduocdat = false;
                        foreach (var seat in seats)
                        {
                            string key = $"{movie}_{room}_{seat}";
                            if (ghedadat.ContainsKey(key) && ghedadat[key])
                            {
                                ghedaduocdat = true;
                                break;
                            }
                        }

                        if (ghedaduocdat)
                        {
                            //phan hoi cho client neu ghe da duoc dat truoc do
                            string errResponse = "BOOK_FAIL|Ghế đã được đặt trước đó!";
                            byte[] errBytes = Encoding.UTF8.GetBytes(errResponse + "\r\n");
                            await stream.WriteAsync(errBytes, 0, errBytes.Length);
                        }
                        else
                        {
                            //neu chua ai dat thi danh dau da dat
                            foreach (var seat in seats)
                            {
                                string key = $"{movie}_{room}_{seat}";
                                ghedadat[key] = true;
                            }

                            //Phản hồi thành công về cho Client vừa đặt (ĐÃ ĐƯA VÀO TRONG ELSE ĐỂ KHÔNG BỊ CHẠY ĐÈ)
                            string successResponse = $"BOOK_SUCCESS|Khách: {customer}|Phim: {movie}|Phòng: {room}|Ghế: {string.Join(",", seats)}";
                            byte[] succBytes = Encoding.UTF8.GetBytes(successResponse + "\r\n");
                            await stream.WriteAsync(succBytes, 0, succBytes.Length);

                            //broadcast cho tat ca cac client biet ghe da dat
                            string updateMsg = $"UPDATE_SEATS|{movie}|{room}|{parts[3]}";
                            _ = BroadCast(updateMsg);
                        }
                    }
                }
            }
            catch { }
            finally
            {
                ds_client.TryRemove(clientkey, out _); //xóa client ra khỏi danh sách
                client.Close(); //thu hồi ram và cổng mạng mà OS đã cung cấp
            }
        }

        private async Task SendInitialData(NetworkStream stream)
        {
            // chuan hoa du lieu gui danh sach film cho client 
            StringBuilder sb = new StringBuilder("INIT_MOVIES");
            foreach (var m in ds_Phim)
            {
                sb.Append($"|{m.tenphim}|{m.giatieuchuan}|{string.Join(",", m.cacphong)}");
            }
            sb.Append("\r\n");
            byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
            stream.WriteAsync(data, 0, data.Length);
        }

        private async Task BroadCast(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
            foreach (var kvp in ds_client)
            {
                if (kvp.Value.Connected)
                {
                    try
                    {
                        await kvp.Value.GetStream().WriteAsync(data, 0, data.Length);
                    }
                    catch { }
                }
            }
        }

        //run server khi mo form
        private async void server4_Load(object sender, EventArgs e)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8080);
                tcpListener.Start();
                isRuning = true;

                while (isRuning)
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    string clientkey = client.Client.RemoteEndPoint.ToString(); //client.Client: Trỏ tới đối tượng Socket (ip, port, protocol) nằm bên dưới đối tượng TcpClient.
                    ds_client.TryAdd(clientkey, client);

                    _ = HandleClient(client, clientkey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo Server: " + ex.Message);
            }
        }
    }
}
