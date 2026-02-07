
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmThongKe : Form
    {
        private readonly NghiepVuPhanTichTraCuu _nghiepVu;
        public frmThongKe(int maTaiKhoan)
        {
            InitializeComponent();
            _nghiepVu = new NghiepVuPhanTichTraCuu(maTaiKhoan);
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            KhoiTaoComboBox();
            object selectedThang = cboThang.SelectedItem;
            object selectedNam = cboNam.SelectedItem;
            if (selectedThang != null && selectedNam != null)
            {
                string thangString = selectedThang.ToString();
                string namString = selectedNam.ToString();

                if (int.TryParse(thangString, out int thang) &&
                    int.TryParse(namString, out int nam))
                {
                    ThongKe(thang, nam);
                }
            }
        }

    
        private void KhoiTaoComboBox()
        {
   
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++) { cboThang.Items.Add(i.ToString("D2")); }
            cboThang.SelectedItem = DateTime.Now.Month.ToString("D2");

            cboNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 5; i++) { cboNam.Items.Add(i.ToString()); }
            cboNam.SelectedItem = currentYear.ToString();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (int.TryParse(cboThang.SelectedItem?.ToString(), out int thang) &&
                int.TryParse(cboNam.SelectedItem?.ToString(), out int nam))
            {
                ThongKe(thang, nam);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tháng và năm hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ThongKe(int thang, int nam)
        {
            try
            {
                (decimal tongThu, decimal tongChi) = _nghiepVu.GetTongThuChi(thang, nam);
                lblTongThu.Text = $"Tổng thu: {tongThu:N0} VNĐ";
                lblTongChi.Text = $"Tổng chi: {tongChi:N0} VNĐ";
                lblSoDu.Text = $"Số dư: {(tongThu - tongChi):N0} VNĐ";

                DataTable dt = _nghiepVu.GetGiaoDichThang(thang, nam);
                dgvGiaoDich.DataSource = dt;

                dgvGiaoDich.Columns["NgayGiaoDich"].HeaderText = "Ngày";
                dgvGiaoDich.Columns["LoaiGiaoDich"].HeaderText = "Loại";
                dgvGiaoDich.Columns["TenHangMuc"].HeaderText = "Hạng mục";
                dgvGiaoDich.Columns["SoTien"].HeaderText = "Số tiền";
                dgvGiaoDich.Columns["GhiChu"].HeaderText = "Mô tả";
                dgvGiaoDich.Columns["SoTien"].DefaultCellStyle.Format = "N0";

                int daysInMonth;
                List<decimal> soDuTichLuyList = _nghiepVu.CalculateAccumulativeBalance(thang, nam, out daysInMonth);

                SetupChartAppearance(thang, nam, daysInMonth);

                for (int i = 0; i < daysInMonth; i++)
                {
                    decimal soDu = soDuTichLuyList.ElementAtOrDefault(i);
                    chartChiTieu.Series["SoDu"].Points.AddXY(i + 1, soDu);
                    chartChiTieu.Series["SoDu"].Points[i].ToolTip = $"Ngày {i + 1}: {soDu:N0} VNĐ";
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show($"Lỗi: {errorMessage}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  
        private void SetupChartAppearance(int thang, int nam, int daysInMonth)
        {
            chartChiTieu.Series.Clear();
            chartChiTieu.Series.Add("SoDu");
            chartChiTieu.Series["SoDu"].ChartType = SeriesChartType.Area;
            chartChiTieu.Series["SoDu"].Color = Color.FromArgb(120, 255, 182, 193);
            chartChiTieu.Series["SoDu"].BorderColor = Color.HotPink;
            chartChiTieu.Series["SoDu"].BorderWidth = 2;

            chartChiTieu.Legends.Clear();
            chartChiTieu.Legends.Add(new Legend("LegendSoDu"));
            chartChiTieu.Series["SoDu"].Legend = "LegendSoDu";
            chartChiTieu.Series["SoDu"].LegendText = "Số dư tích lũy";

            chartChiTieu.Titles.Clear();
            chartChiTieu.Titles.Add($"Số dư tích lũy tháng {thang}/{nam}");
            chartChiTieu.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
            chartChiTieu.ChartAreas[0].AxisX.Title = "Ngày";
            chartChiTieu.ChartAreas[0].AxisY.Title = "Số dư (VNĐ)";
            chartChiTieu.ChartAreas[0].AxisX.Interval = 1;
            chartChiTieu.ChartAreas[0].AxisX.Minimum = 1;
            chartChiTieu.ChartAreas[0].AxisX.Maximum = daysInMonth;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}