namespace Lab03
{
    partial class Bai01
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_server = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // btn_server
            // 
            btn_server.BackColor = SystemColors.ActiveCaption;
            btn_server.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_server.Location = new Point(111, 166);
            btn_server.Name = "btn_server";
            btn_server.Size = new Size(180, 64);
            btn_server.TabIndex = 0;
            btn_server.Text = "Server";
            btn_server.UseVisualStyleBackColor = false;
            btn_server.Click += btn_server_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(496, 166);
            button1.Name = "button1";
            button1.Size = new Size(180, 64);
            button1.TabIndex = 1;
            button1.Text = "Client";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Bai01
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btn_server);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Bai01";
            Text = "DashBoard";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_server;
        private Button button1;
    }
}
