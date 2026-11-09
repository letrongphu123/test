namespace Lab03
{
    partial class client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_datve = new Button();
            lbl_ghengoi = new Label();
            clb_ghengoi = new CheckedListBox();
            lbl_phongve = new Label();
            cmb_phongchieu = new ComboBox();
            lbl_phim = new Label();
            cmb_tenphim = new ComboBox();
            txt_name = new MaskedTextBox();
            lbl_name = new Label();
            txt_output = new TextBox();
            lbl_tittle = new Label();
            SuspendLayout();
            // 
            // btn_datve
            // 
            btn_datve.BackColor = SystemColors.ActiveCaption;
            btn_datve.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_datve.Location = new Point(430, 357);
            btn_datve.Name = "btn_datve";
            btn_datve.Size = new Size(103, 50);
            btn_datve.TabIndex = 25;
            btn_datve.Text = "Đặt vé";
            btn_datve.UseVisualStyleBackColor = false;
            btn_datve.Click += btn_datve_Click;
            // 
            // lbl_ghengoi
            // 
            lbl_ghengoi.AutoSize = true;
            lbl_ghengoi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_ghengoi.Location = new Point(28, 271);
            lbl_ghengoi.Name = "lbl_ghengoi";
            lbl_ghengoi.Size = new Size(92, 28);
            lbl_ghengoi.TabIndex = 24;
            lbl_ghengoi.Text = "Ghế ngồi";
            // 
            // clb_ghengoi
            // 
            clb_ghengoi.FormattingEnabled = true;
            clb_ghengoi.Location = new Point(146, 271);
            clb_ghengoi.Name = "clb_ghengoi";
            clb_ghengoi.Size = new Size(239, 136);
            clb_ghengoi.TabIndex = 23;
            // 
            // lbl_phongve
            // 
            lbl_phongve.AutoSize = true;
            lbl_phongve.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_phongve.Location = new Point(28, 213);
            lbl_phongve.Name = "lbl_phongve";
            lbl_phongve.Size = new Size(94, 28);
            lbl_phongve.TabIndex = 22;
            lbl_phongve.Text = "Phòng vé";
            // 
            // cmb_phongchieu
            // 
            cmb_phongchieu.FormattingEnabled = true;
            cmb_phongchieu.Location = new Point(146, 213);
            cmb_phongchieu.Name = "cmb_phongchieu";
            cmb_phongchieu.Size = new Size(239, 28);
            cmb_phongchieu.TabIndex = 21;
            // 
            // lbl_phim
            // 
            lbl_phim.AutoSize = true;
            lbl_phim.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_phim.Location = new Point(28, 161);
            lbl_phim.Name = "lbl_phim";
            lbl_phim.Size = new Size(56, 28);
            lbl_phim.TabIndex = 20;
            lbl_phim.Text = "Phim";
            // 
            // cmb_tenphim
            // 
            cmb_tenphim.FormattingEnabled = true;
            cmb_tenphim.Location = new Point(146, 161);
            cmb_tenphim.Name = "cmb_tenphim";
            cmb_tenphim.Size = new Size(239, 28);
            cmb_tenphim.TabIndex = 19;
            cmb_tenphim.SelectedIndexChanged += cmb_tenphim_SelectedIndexChanged;
            // 
            // txt_name
            // 
            txt_name.Location = new Point(146, 109);
            txt_name.Name = "txt_name";
            txt_name.Size = new Size(239, 27);
            txt_name.TabIndex = 18;
            txt_name.MaskInputRejected += txt_name_MaskInputRejected;
            // 
            // lbl_name
            // 
            lbl_name.AutoSize = true;
            lbl_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_name.Location = new Point(28, 105);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(96, 28);
            lbl_name.TabIndex = 17;
            lbl_name.Text = "Họ và tên";
            // 
            // txt_output
            // 
            txt_output.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_output.Location = new Point(430, 105);
            txt_output.Multiline = true;
            txt_output.Name = "txt_output";
            txt_output.ReadOnly = true;
            txt_output.ScrollBars = ScrollBars.Vertical;
            txt_output.Size = new Size(343, 220);
            txt_output.TabIndex = 16;
            // 
            // lbl_tittle
            // 
            lbl_tittle.AutoSize = true;
            lbl_tittle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_tittle.Location = new Point(248, 44);
            lbl_tittle.Name = "lbl_tittle";
            lbl_tittle.Size = new Size(309, 41);
            lbl_tittle.TabIndex = 15;
            lbl_tittle.Text = "QUẢN LÝ PHÒNG VÉ";
            // 
            // client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_datve);
            Controls.Add(lbl_ghengoi);
            Controls.Add(clb_ghengoi);
            Controls.Add(lbl_phongve);
            Controls.Add(cmb_phongchieu);
            Controls.Add(lbl_phim);
            Controls.Add(cmb_tenphim);
            Controls.Add(txt_name);
            Controls.Add(lbl_name);
            Controls.Add(txt_output);
            Controls.Add(lbl_tittle);
            Name = "client";
            Text = "client";
            Load += client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_datve;
        private Label lbl_ghengoi;
        private CheckedListBox clb_ghengoi;
        private Label lbl_phongve;
        private ComboBox cmb_phongchieu;
        private Label lbl_phim;
        private ComboBox cmb_tenphim;
        private MaskedTextBox txt_name;
        private Label lbl_name;
        private TextBox txt_output;
        private Label lbl_tittle;
    }
}