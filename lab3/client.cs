using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; 
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class client : Form
    {
        private TcpClient Client;
        private StreamReader reader;
        private StreamWriter writer;
        private string pendingResult = "";

        //cau truc du lieu luu thong tin phim
        private Dictionary<string, Thongtinphim> phim = new Dictionary<string, Thongtinphim>();

        public class Thongtinphim
        {
            public decimal giatieuchuan { get; set; }
            public List<int> phong { get; set; }
        }

        public client()
        {
            InitializeComponent();
            SetupSeatList();
        }

        private void SetupSeatList()
        {
            clb_ghengoi.Items.Clear();
            string[] allseats =
            {
                "A1", "A2", "A3", "A4", "A5",
                "B1", "B2", "B3", "B4", "B5",
                "C1", "C2", "C3", "C4", "C5"
            };
            clb_ghengoi.Items.AddRange(allseats);
        }

        private async Task ConnectToServerAsync()
        {
            try
            {
                Client = new TcpClient();
                await Client.ConnectAsync("127.0.0.1", 8080);

                var stream = Client.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                //this.Invoke(new Action(() =>
                //{
                //    this.Text = "Kết nối đến server thành công!";
                //}));

                //lang nghe lien tuc tu server day xuong
                string message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    if (message.StartsWith("INIT_MOVIES"))
                    {
                        ProcessInitMovies(message);
                    }
                    else if (message.StartsWith("UPDATE_SEATS"))
                    {
                        ProcessUpdateSeats(message);
                    }
                    else if (message.StartsWith("BOOK_FAIL"))
                    {
                        string[] errInfo = message.Split('|');
                        this.Invoke(new Action(() =>
                            MessageBox.Show(errInfo.Length > 1 ? errInfo[1] : "Đặt vé thất bại!",
                                "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)));
                    }
                    else if (message.StartsWith("BOOK_SUCCESS"))
                    {
                        this.Invoke(new Action(() =>
                        {
                            txt_output.Text = pendingResult;
                            MessageBox.Show("Đặt vé thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            for (int i = 0; i < clb_ghengoi.Items.Count; i++)
                                clb_ghengoi.SetItemChecked(i, false);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến server!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //xu ly danh sach phim nhan tu server
        private void ProcessInitMovies(string data)
        {
            try
            {
                string[] parts = data.Split('|');
                phim.Clear();

                for (int i = 1; i < parts.Length; i += 3)
                {
                    if (i + 2 < parts.Length)
                    {
                        string tenphim = parts[i];
                        decimal giachuan = decimal.Parse(parts[i + 1]);
                        List<int> cacphong = parts[i + 2].Split(',').Select(int.Parse).ToList();

                        phim[tenphim] = new Thongtinphim
                        {
                            giatieuchuan = giachuan,
                            phong = cacphong
                        };

                    }
                }
                // dua du lieu len combobox
                this.Invoke(new Action(() =>
                {
                    cmb_tenphim.DataSource = new BindingSource(phim, null);
                    cmb_tenphim.DisplayMember = "Key";
                    cmb_tenphim.ValueMember = "Value";
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phân tích dữ liệu phim từ Server: " + ex.Message);
            }
        }

        // Xử lý khi các client khác đặt ghế, tự động bỏ chọn trên giao diện nếu trùng
        private void ProcessUpdateSeats(string data)
        {
            try
            {
                string[] parts = data.Split('|');
                string movie = parts[1];
                int room = int.Parse(parts[2]);
                string[] bookedSeats = parts[3].Split(',');

                this.Invoke(new Action(() =>
                {
                    if (cmb_tenphim.SelectedItem is KeyValuePair<string, Thongtinphim> currentMovie && cmb_phongchieu.SelectedItem != null)
                    {
                        string selectedMovieName = currentMovie.Key;
                        int selectedRoom = (int)cmb_phongchieu.SelectedItem;

                        if (selectedMovieName == movie && selectedRoom == room)
                        {
                            for (int i = 0; i < clb_ghengoi.Items.Count; i++)
                            {
                                string seatName = clb_ghengoi.Items[i].ToString();
                                if (bookedSeats.Contains(seatName))
                                {
                                    clb_ghengoi.SetItemChecked(i, false);
                                }
                            }
                        }
                    }
                }));
            }
            catch { }
        }

        private void txt_name_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cmb_tenphim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_tenphim.SelectedItem is KeyValuePair<string, Thongtinphim> selectedPhim)
            {
                cmb_phongchieu.DataSource = selectedPhim.Value.phong;
            }
        }

        private async void btn_datve_Click(object sender, EventArgs e)
        {
            string fullname = txt_name.Text.Trim();
            if (string.IsNullOrEmpty(fullname))
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_name.Focus();
                return;
            }

            if (cmb_tenphim.SelectedItem == null || cmb_phongchieu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phim và phòng chiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenphim = ((KeyValuePair<string, Thongtinphim>)cmb_tenphim.SelectedItem).Key;
            int phongduocchon = (int)cmb_phongchieu.SelectedItem;

            //lay du lieu danh sach ghe dang duoc chon trong checlistbox
            var gheduocchon = clb_ghengoi.CheckedItems.Cast<string>().ToList();
            if (gheduocchon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một vé xem phim!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gheduocchon.Count > 2)
            {
                MessageBox.Show("Mỗi lần đặt không được chọn nhiều hơn 2 vé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Client == null || !Client.Connected)
            {
                MessageBox.Show("Chưa kết nối đến Server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                pendingResult = $"=== ĐẶT VÉ THÀNH CÔNG ===\r\n" +
                                $"- Khách hàng: {fullname}\r\n" +
                                $"- Phim: {tenphim}\r\n" +
                                $"- Phòng: {phongduocchon}\r\n" +
                                $"- Ghế: {string.Join(", ", gheduocchon)}";

                string request = $"BOOK|{tenphim}|{phongduocchon}|{string.Join(",", gheduocchon)}|{fullname}";
                await writer.WriteLineAsync(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi dữ liệu lên Server: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void client_Load(object sender, EventArgs e)
        {
            await ConnectToServerAsync();
        }
    }
}