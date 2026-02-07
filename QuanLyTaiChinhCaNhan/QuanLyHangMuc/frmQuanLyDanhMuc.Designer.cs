
namespace QuanLyTaiChinh
{
    partial class frmQuanLyHangMuc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyHangMuc));
            this.btnQuanLyThu = new System.Windows.Forms.Button();
            this.btnQuanLyChi = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuayLai = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneShadowPanel1 = new Siticone.UI.WinForms.SiticoneShadowPanel();
            this.listBoxHangMuc = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.siticoneShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuanLyThu
            // 
            this.btnQuanLyThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuanLyThu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnQuanLyThu.Location = new System.Drawing.Point(75, 82);
            this.btnQuanLyThu.Name = "btnQuanLyThu";
            this.btnQuanLyThu.Size = new System.Drawing.Size(189, 50);
            this.btnQuanLyThu.TabIndex = 4;
            this.btnQuanLyThu.Text = "Quản lý Thu";
            this.btnQuanLyThu.UseVisualStyleBackColor = false;
            this.btnQuanLyThu.Click += new System.EventHandler(this.btnQuanLyThu_Click);
            // 
            // btnQuanLyChi
            // 
            this.btnQuanLyChi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuanLyChi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnQuanLyChi.Location = new System.Drawing.Point(261, 82);
            this.btnQuanLyChi.Name = "btnQuanLyChi";
            this.btnQuanLyChi.Size = new System.Drawing.Size(188, 50);
            this.btnQuanLyChi.TabIndex = 5;
            this.btnQuanLyChi.Text = "Quản lý Chi";
            this.btnQuanLyChi.UseVisualStyleBackColor = false;
            this.btnQuanLyChi.Click += new System.EventHandler(this.btnQuanLyChi_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(231, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 15;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.FillColor = System.Drawing.Color.LightGreen;
            this.btnThem.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.ImageSize = new System.Drawing.Size(35, 35);
            this.btnThem.Location = new System.Drawing.Point(22, 441);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(180, 34);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm hạng mục khác ";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 15;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.LightPink;
            this.btnSua.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.Location = new System.Drawing.Point(235, 441);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(142, 34);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "  Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 15;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.Tomato;
            this.btnXoa.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(413, 441);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(130, 34);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "   Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.CheckedState.Parent = this.btnQuayLai;
            this.btnQuayLai.CustomImages.Parent = this.btnQuayLai;
            this.btnQuayLai.FillColor = System.Drawing.SystemColors.Window;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.HoveredState.Parent = this.btnQuayLai;
            this.btnQuayLai.Image = ((System.Drawing.Image)(resources.GetObject("btnQuayLai.Image")));
            this.btnQuayLai.ImageSize = new System.Drawing.Size(70, 70);
            this.btnQuayLai.Location = new System.Drawing.Point(16, 16);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.ShadowDecoration.Parent = this.btnQuayLai;
            this.btnQuayLai.Size = new System.Drawing.Size(60, 45);
            this.btnQuayLai.TabIndex = 19;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // siticoneShadowPanel1
            // 
            this.siticoneShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneShadowPanel1.Controls.Add(this.btnQuayLai);
            this.siticoneShadowPanel1.Controls.Add(this.pictureBox1);
            this.siticoneShadowPanel1.Controls.Add(this.listBoxHangMuc);
            this.siticoneShadowPanel1.Controls.Add(this.btnQuanLyChi);
            this.siticoneShadowPanel1.Controls.Add(this.btnQuanLyThu);
            this.siticoneShadowPanel1.FillColor = System.Drawing.Color.White;
            this.siticoneShadowPanel1.Location = new System.Drawing.Point(22, 12);
            this.siticoneShadowPanel1.Name = "siticoneShadowPanel1";
            this.siticoneShadowPanel1.Radius = 20;
            this.siticoneShadowPanel1.ShadowColor = System.Drawing.Color.IndianRed;
            this.siticoneShadowPanel1.Size = new System.Drawing.Size(521, 413);
            this.siticoneShadowPanel1.TabIndex = 20;
            // 
            // listBoxHangMuc
            // 
            this.listBoxHangMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.listBoxHangMuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxHangMuc.FormattingEnabled = true;
            this.listBoxHangMuc.ItemHeight = 40;
            this.listBoxHangMuc.Location = new System.Drawing.Point(75, 138);
            this.listBoxHangMuc.Name = "listBoxHangMuc";
            this.listBoxHangMuc.Size = new System.Drawing.Size(374, 244);
            this.listBoxHangMuc.TabIndex = 0;
            // 
            // frmQuanLyHangMuc
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(565, 497);
            this.Controls.Add(this.siticoneShadowPanel1);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Name = "frmQuanLyHangMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hạng mục";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.siticoneShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button btnQuanLyThu;
        private System.Windows.Forms.Button btnQuanLyChi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Siticone.UI.WinForms.SiticoneRoundedButton btnQuayLai;
        private System.Windows.Forms.ListBox listBoxHangMuc;
        private Siticone.UI.WinForms.SiticoneShadowPanel siticoneShadowPanel1;
    }
}