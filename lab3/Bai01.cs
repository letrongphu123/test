namespace Lab03
{
    public partial class Bai01 : Form
    {
        public Bai01()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client Client4 = new client();
            Client4.Show();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            server4 serverForm = new server4();
            serverForm.Show();
        }
    }
}
