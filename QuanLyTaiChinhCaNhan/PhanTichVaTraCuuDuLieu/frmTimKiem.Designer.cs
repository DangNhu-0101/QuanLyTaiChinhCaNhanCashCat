namespace QuanLyTaiChinhCaNhan
{
    partial class frmTimKiem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimKiem));
            this.dgvKetQua = new Siticone.UI.WinForms.SiticoneDataGridView();
            this.Ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HangMuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTuNgay = new Siticone.UI.WinForms.SiticoneDateTimePicker();
            this.dtpDenNgay = new Siticone.UI.WinForms.SiticoneDateTimePicker();
            this.cbLoaiGiaoDich = new Siticone.UI.WinForms.SiticoneComboBox();
            this.cbTenHangMuc = new Siticone.UI.WinForms.SiticoneComboBox();
            this.nudTuSoTien = new Siticone.UI.WinForms.SiticoneNumericUpDown();
            this.nudDenSoTien = new Siticone.UI.WinForms.SiticoneNumericUpDown();
            this.btnFilter = new Siticone.UI.WinForms.SiticoneButton();
            this.btnClear = new Siticone.UI.WinForms.SiticoneButton();
            this.siticoneLabel1 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel2 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel3 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel4 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel5 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel6 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticoneLabel7 = new Siticone.UI.WinForms.SiticoneLabel();
            this.btnQuayLai = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneShadowPanel1 = new Siticone.UI.WinForms.SiticoneShadowPanel();
            this.siticoneLabel8 = new Siticone.UI.WinForms.SiticoneLabel();
            this.siticonePictureBox1 = new Siticone.UI.WinForms.SiticonePictureBox();
            this.txtKeyword = new Siticone.UI.WinForms.SiticoneTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTuSoTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDenSoTien)).BeginInit();
            this.siticoneShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKetQua
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKetQua.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKetQua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQua.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQua.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvKetQua.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvKetQua.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKetQua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKetQua.ColumnHeadersHeight = 40;
            this.dgvKetQua.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ngay,
            this.MoTa,
            this.Loai,
            this.HangMuc,
            this.SoTien});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKetQua.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvKetQua.EnableHeadersVisualStyles = false;
            this.dgvKetQua.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvKetQua.Location = new System.Drawing.Point(32, 385);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.RowHeadersVisible = false;
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.RowTemplate.Height = 24;
            this.dgvKetQua.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQua.Size = new System.Drawing.Size(1127, 260);
            this.dgvKetQua.TabIndex = 8;
            this.dgvKetQua.Theme = Siticone.UI.WinForms.Enums.DataGridViewPresetThemes.Default;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvKetQua.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvKetQua.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvKetQua.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvKetQua.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvKetQua.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvKetQua.ThemeStyle.ReadOnly = false;
            this.dgvKetQua.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvKetQua.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvKetQua.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvKetQua.ThemeStyle.RowsStyle.Height = 24;
            this.dgvKetQua.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvKetQua.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Ngay
            // 
            this.Ngay.HeaderText = "Ngày";
            this.Ngay.MinimumWidth = 6;
            this.Ngay.Name = "Ngay";
            this.Ngay.ReadOnly = true;
            // 
            // MoTa
            // 
            this.MoTa.HeaderText = "Mô tả";
            this.MoTa.MinimumWidth = 6;
            this.MoTa.Name = "MoTa";
            this.MoTa.ReadOnly = true;
            // 
            // Loai
            // 
            this.Loai.HeaderText = "Loại";
            this.Loai.MinimumWidth = 6;
            this.Loai.Name = "Loai";
            this.Loai.ReadOnly = true;
            // 
            // HangMuc
            // 
            this.HangMuc.HeaderText = "Hạng mục";
            this.HangMuc.MinimumWidth = 6;
            this.HangMuc.Name = "HangMuc";
            this.HangMuc.ReadOnly = true;
            // 
            // SoTien
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SoTien.DefaultCellStyle = dataGridViewCellStyle3;
            this.SoTien.HeaderText = "Số tiền";
            this.SoTien.MinimumWidth = 6;
            this.SoTien.Name = "SoTien";
            this.SoTien.ReadOnly = true;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CheckedState.Parent = this.dtpTuNgay;
            this.dtpTuNgay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.HoveredState.Parent = this.dtpTuNgay;
            this.dtpTuNgay.Location = new System.Drawing.Point(342, 160);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.ShadowDecoration.Parent = this.dtpTuNgay;
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.Value = new System.DateTime(2025, 6, 25, 15, 0, 3, 162);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CheckedState.Parent = this.dtpDenNgay;
            this.dtpDenNgay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.HoveredState.Parent = this.dtpDenNgay;
            this.dtpDenNgay.Location = new System.Drawing.Point(592, 160);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.ShadowDecoration.Parent = this.dtpDenNgay;
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpDenNgay.TabIndex = 2;
            this.dtpDenNgay.Value = new System.DateTime(2025, 6, 25, 15, 0, 16, 613);
            // 
            // cbLoaiGiaoDich
            // 
            this.cbLoaiGiaoDich.BackColor = System.Drawing.Color.Transparent;
            this.cbLoaiGiaoDich.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiGiaoDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiGiaoDich.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLoaiGiaoDich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLoaiGiaoDich.FormattingEnabled = true;
            this.cbLoaiGiaoDich.HoveredState.Parent = this.cbLoaiGiaoDich;
            this.cbLoaiGiaoDich.ItemHeight = 30;
            this.cbLoaiGiaoDich.ItemsAppearance.Parent = this.cbLoaiGiaoDich;
            this.cbLoaiGiaoDich.Location = new System.Drawing.Point(820, 160);
            this.cbLoaiGiaoDich.Name = "cbLoaiGiaoDich";
            this.cbLoaiGiaoDich.ShadowDecoration.Parent = this.cbLoaiGiaoDich;
            this.cbLoaiGiaoDich.Size = new System.Drawing.Size(140, 36);
            this.cbLoaiGiaoDich.TabIndex = 3;
            // 
            // cbTenHangMuc
            // 
            this.cbTenHangMuc.BackColor = System.Drawing.Color.Transparent;
            this.cbTenHangMuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTenHangMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTenHangMuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTenHangMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbTenHangMuc.FormattingEnabled = true;
            this.cbTenHangMuc.HoveredState.Parent = this.cbTenHangMuc;
            this.cbTenHangMuc.ItemHeight = 30;
            this.cbTenHangMuc.ItemsAppearance.Parent = this.cbTenHangMuc;
            this.cbTenHangMuc.Location = new System.Drawing.Point(978, 160);
            this.cbTenHangMuc.Name = "cbTenHangMuc";
            this.cbTenHangMuc.ShadowDecoration.Parent = this.cbTenHangMuc;
            this.cbTenHangMuc.Size = new System.Drawing.Size(140, 36);
            this.cbTenHangMuc.TabIndex = 4;
            // 
            // nudTuSoTien
            // 
            this.nudTuSoTien.BackColor = System.Drawing.Color.Transparent;
            this.nudTuSoTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudTuSoTien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nudTuSoTien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nudTuSoTien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nudTuSoTien.DisabledState.Parent = this.nudTuSoTien;
            this.nudTuSoTien.DisabledState.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            this.nudTuSoTien.DisabledState.UpDownButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.nudTuSoTien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nudTuSoTien.FocusedState.Parent = this.nudTuSoTien;
            this.nudTuSoTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTuSoTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.nudTuSoTien.Location = new System.Drawing.Point(46, 284);
            this.nudTuSoTien.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudTuSoTien.Name = "nudTuSoTien";
            this.nudTuSoTien.ShadowDecoration.Parent = this.nudTuSoTien;
            this.nudTuSoTien.Size = new System.Drawing.Size(100, 36);
            this.nudTuSoTien.TabIndex = 5;
            this.nudTuSoTien.UpDownButtonFillColor = System.Drawing.Color.LightPink;
            // 
            // nudDenSoTien
            // 
            this.nudDenSoTien.BackColor = System.Drawing.Color.Transparent;
            this.nudDenSoTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudDenSoTien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nudDenSoTien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nudDenSoTien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nudDenSoTien.DisabledState.Parent = this.nudDenSoTien;
            this.nudDenSoTien.DisabledState.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            this.nudDenSoTien.DisabledState.UpDownButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.nudDenSoTien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nudDenSoTien.FocusedState.Parent = this.nudDenSoTien;
            this.nudDenSoTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDenSoTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.nudDenSoTien.Location = new System.Drawing.Point(218, 284);
            this.nudDenSoTien.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudDenSoTien.Name = "nudDenSoTien";
            this.nudDenSoTien.ShadowDecoration.Parent = this.nudDenSoTien;
            this.nudDenSoTien.Size = new System.Drawing.Size(100, 36);
            this.nudDenSoTien.TabIndex = 6;
            this.nudDenSoTien.UpDownButtonFillColor = System.Drawing.Color.LightPink;
            // 
            // btnFilter
            // 
            this.btnFilter.BorderRadius = 22;
            this.btnFilter.CheckedState.Parent = this.btnFilter;
            this.btnFilter.CustomImages.Parent = this.btnFilter;
            this.btnFilter.FillColor = System.Drawing.Color.LightPink;
            this.btnFilter.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(114)))));
            this.btnFilter.HoveredState.Parent = this.btnFilter;
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.ImageSize = new System.Drawing.Size(30, 30);
            this.btnFilter.Location = new System.Drawing.Point(362, 275);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.ShadowDecoration.Parent = this.btnFilter;
            this.btnFilter.Size = new System.Drawing.Size(180, 45);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.BorderRadius = 22;
            this.btnClear.CheckedState.Parent = this.btnClear;
            this.btnClear.CustomImages.Parent = this.btnClear;
            this.btnClear.FillColor = System.Drawing.Color.LightPink;
            this.btnClear.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(114)))));
            this.btnClear.HoveredState.Parent = this.btnClear;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageSize = new System.Drawing.Size(30, 30);
            this.btnClear.Location = new System.Drawing.Point(575, 275);
            this.btnClear.Name = "btnClear";
            this.btnClear.ShadowDecoration.Parent = this.btnClear;
            this.btnClear.Size = new System.Drawing.Size(180, 45);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Hủy lọc";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // siticoneLabel1
            // 
            this.siticoneLabel1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel1.Location = new System.Drawing.Point(37, 123);
            this.siticoneLabel1.Name = "siticoneLabel1";
            this.siticoneLabel1.Size = new System.Drawing.Size(122, 47);
            this.siticoneLabel1.TabIndex = 9;
            this.siticoneLabel1.Text = "Từ khóa";
            // 
            // siticoneLabel2
            // 
            this.siticoneLabel2.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel2.Location = new System.Drawing.Point(342, 123);
            this.siticoneLabel2.Name = "siticoneLabel2";
            this.siticoneLabel2.Size = new System.Drawing.Size(129, 47);
            this.siticoneLabel2.TabIndex = 10;
            this.siticoneLabel2.Text = "Từ ngày:";
            // 
            // siticoneLabel3
            // 
            this.siticoneLabel3.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel3.Location = new System.Drawing.Point(592, 123);
            this.siticoneLabel3.Name = "siticoneLabel3";
            this.siticoneLabel3.Size = new System.Drawing.Size(150, 47);
            this.siticoneLabel3.TabIndex = 11;
            this.siticoneLabel3.Text = "Đến ngày:";
            // 
            // siticoneLabel4
            // 
            this.siticoneLabel4.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel4.Location = new System.Drawing.Point(820, 123);
            this.siticoneLabel4.Name = "siticoneLabel4";
            this.siticoneLabel4.Size = new System.Drawing.Size(213, 47);
            this.siticoneLabel4.TabIndex = 12;
            this.siticoneLabel4.Text = "Loại giao dịch:";
            // 
            // siticoneLabel5
            // 
            this.siticoneLabel5.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel5.Location = new System.Drawing.Point(978, 123);
            this.siticoneLabel5.Name = "siticoneLabel5";
            this.siticoneLabel5.Size = new System.Drawing.Size(161, 47);
            this.siticoneLabel5.TabIndex = 13;
            this.siticoneLabel5.Text = "Hạng mục:";
            // 
            // siticoneLabel6
            // 
            this.siticoneLabel6.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel6.Location = new System.Drawing.Point(46, 247);
            this.siticoneLabel6.Name = "siticoneLabel6";
            this.siticoneLabel6.Size = new System.Drawing.Size(156, 47);
            this.siticoneLabel6.TabIndex = 14;
            this.siticoneLabel6.Text = "Từ số tiền:";
            // 
            // siticoneLabel7
            // 
            this.siticoneLabel7.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel7.Location = new System.Drawing.Point(218, 247);
            this.siticoneLabel7.Name = "siticoneLabel7";
            this.siticoneLabel7.Size = new System.Drawing.Size(177, 47);
            this.siticoneLabel7.TabIndex = 15;
            this.siticoneLabel7.Text = "Đến số tiền:";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.CheckedState.Parent = this.btnQuayLai;
            this.btnQuayLai.CustomImages.Parent = this.btnQuayLai;
            this.btnQuayLai.FillColor = System.Drawing.Color.White;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.HoveredState.Parent = this.btnQuayLai;
            this.btnQuayLai.Image = ((System.Drawing.Image)(resources.GetObject("btnQuayLai.Image")));
            this.btnQuayLai.ImageSize = new System.Drawing.Size(70, 70);
            this.btnQuayLai.Location = new System.Drawing.Point(37, 38);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.ShadowDecoration.Parent = this.btnQuayLai;
            this.btnQuayLai.Size = new System.Drawing.Size(60, 45);
            this.btnQuayLai.TabIndex = 19;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // siticoneShadowPanel1
            // 
            this.siticoneShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneShadowPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel8);
            this.siticoneShadowPanel1.Controls.Add(this.siticonePictureBox1);
            this.siticoneShadowPanel1.Controls.Add(this.txtKeyword);
            this.siticoneShadowPanel1.Controls.Add(this.btnQuayLai);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel1);
            this.siticoneShadowPanel1.Controls.Add(this.btnClear);
            this.siticoneShadowPanel1.Controls.Add(this.dtpTuNgay);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel7);
            this.siticoneShadowPanel1.Controls.Add(this.btnFilter);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel2);
            this.siticoneShadowPanel1.Controls.Add(this.nudDenSoTien);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel6);
            this.siticoneShadowPanel1.Controls.Add(this.dtpDenNgay);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel5);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel3);
            this.siticoneShadowPanel1.Controls.Add(this.siticoneLabel4);
            this.siticoneShadowPanel1.Controls.Add(this.nudTuSoTien);
            this.siticoneShadowPanel1.Controls.Add(this.cbLoaiGiaoDich);
            this.siticoneShadowPanel1.Controls.Add(this.cbTenHangMuc);
            this.siticoneShadowPanel1.FillColor = System.Drawing.Color.White;
            this.siticoneShadowPanel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneShadowPanel1.Location = new System.Drawing.Point(12, 12);
            this.siticoneShadowPanel1.Name = "siticoneShadowPanel1";
            this.siticoneShadowPanel1.Radius = 22;
            this.siticoneShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.siticoneShadowPanel1.Size = new System.Drawing.Size(1158, 352);
            this.siticoneShadowPanel1.TabIndex = 20;
            // 
            // siticoneLabel8
            // 
            this.siticoneLabel8.BackColor = System.Drawing.Color.Transparent;
            this.siticoneLabel8.Font = new System.Drawing.Font("Cascadia Code", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(114)))));
            this.siticoneLabel8.Location = new System.Drawing.Point(405, 38);
            this.siticoneLabel8.Name = "siticoneLabel8";
            this.siticoneLabel8.Size = new System.Drawing.Size(54, 8);
            this.siticoneLabel8.TabIndex = 21;
            this.siticoneLabel8.Text = "Tìm kiếm nâng cao";
            // 
            // siticonePictureBox1
            // 
            this.siticonePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("siticonePictureBox1.Image")));
            this.siticonePictureBox1.Location = new System.Drawing.Point(318, 38);
            this.siticonePictureBox1.Name = "siticonePictureBox1";
            this.siticonePictureBox1.ShadowDecoration.Parent = this.siticonePictureBox1;
            this.siticonePictureBox1.Size = new System.Drawing.Size(61, 54);
            this.siticonePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.siticonePictureBox1.TabIndex = 20;
            this.siticonePictureBox1.TabStop = false;
            // 
            // txtKeyword
            // 
            this.txtKeyword.BorderThickness = 2;
            this.txtKeyword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKeyword.DefaultText = "";
            this.txtKeyword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtKeyword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtKeyword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKeyword.DisabledState.Parent = this.txtKeyword;
            this.txtKeyword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKeyword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKeyword.FocusedState.Parent = this.txtKeyword;
            this.txtKeyword.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKeyword.HoveredState.Parent = this.txtKeyword;
            this.txtKeyword.Location = new System.Drawing.Point(37, 160);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PasswordChar = '\0';
            this.txtKeyword.PlaceholderText = "";
            this.txtKeyword.SelectedText = "";
            this.txtKeyword.ShadowDecoration.Parent = this.txtKeyword;
            this.txtKeyword.Size = new System.Drawing.Size(267, 44);
            this.txtKeyword.TabIndex = 17;
            // 
            // frmTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 45F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1197, 788);
            this.Controls.Add(this.siticoneShadowPanel1);
            this.Controls.Add(this.dgvKetQua);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmTimKiem";
            this.Text = "frmTimKiem";
  
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTuSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDenSoTien)).EndInit();
            this.siticoneShadowPanel1.ResumeLayout(false);
            this.siticoneShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Siticone.UI.WinForms.SiticoneDataGridView dgvKetQua;
        private Siticone.UI.WinForms.SiticoneDateTimePicker dtpTuNgay;
        private Siticone.UI.WinForms.SiticoneDateTimePicker dtpDenNgay;
        private Siticone.UI.WinForms.SiticoneComboBox cbLoaiGiaoDich;
        private Siticone.UI.WinForms.SiticoneComboBox cbTenHangMuc;
        private Siticone.UI.WinForms.SiticoneNumericUpDown nudTuSoTien;
        private Siticone.UI.WinForms.SiticoneNumericUpDown nudDenSoTien;
        private Siticone.UI.WinForms.SiticoneButton btnFilter;
        private Siticone.UI.WinForms.SiticoneButton btnClear;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel1;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel2;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel3;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel4;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel5;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel6;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel7;
        private Siticone.UI.WinForms.SiticoneRoundedButton btnQuayLai;
        private Siticone.UI.WinForms.SiticoneShadowPanel siticoneShadowPanel1;
        private Siticone.UI.WinForms.SiticoneTextBox txtKeyword;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loai;
        private System.Windows.Forms.DataGridViewTextBoxColumn HangMuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTien;
        private Siticone.UI.WinForms.SiticoneLabel siticoneLabel8;
        private Siticone.UI.WinForms.SiticonePictureBox siticonePictureBox1;
    }
}