using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyTaiChinh;
using QuanLyNganSach;
using QuanLyGiaoDich;
using Siticone.UI.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmTrangChu : Form
    {
        private int _maTaiKhoan;
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";

        public frmTrangChu(int maTaiKhoan)
        {
            InitializeComponent();
            _maTaiKhoan = maTaiKhoan;
            ToolTip1.SetToolTip(btnImageRefresh, "Tải lại trang");
            LoadData();
        }

        private void WelcomeUser(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT HoTen FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lblWelcomeUser.Text = "Xin chào, " + result.ToString() + "!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tên người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSummaryData()
        {
            decimal tongThu = 0, tongChi = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MaTaiKhoan = @MaTaiKhoan AND LoaiGiaoDich = @Loai";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        cmd.Parameters.AddWithValue("@Loai", "Thu");
                        object result = cmd.ExecuteScalar();
                        tongThu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                        cmd.Parameters["@Loai"].Value = "Chi";
                        result = cmd.ExecuteScalar();
                        tongChi = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
                lblTongThu.Text = "+ " + tongThu.ToString("N0") + " VND";
                lblTongChi.Text = "- " + tongChi.ToString("N0") + " VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tổng quan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChartData()
        {
            chtThuChi.Series.Clear();
            int currentMonth = DateTime.Now.Month;
            int previousMonth = DateTime.Now.AddMonths(-1).Month;
            int currentYear = DateTime.Now.Year;

            Series seriesCurrentMonthThu = new Series($"Thu - Tháng {currentMonth}/{currentYear}") { ChartType = SeriesChartType.Column };
            Series seriesCurrentMonthChi = new Series($"Chi - Tháng {currentMonth}/{currentYear}") { ChartType = SeriesChartType.Column };
            Series seriesPreviousMonthThu = new Series($"Thu - Tháng {previousMonth}/{currentYear}") { ChartType = SeriesChartType.Column };
            Series seriesPreviousMonthChi = new Series($"Chi - Tháng {previousMonth}/{currentYear}") { ChartType = SeriesChartType.Column };

            int index = 1;
            bool hasData = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MaTaiKhoan = @MaTaiKhoan AND MONTH(NgayGiaoDich) = @Month AND YEAR(NgayGiaoDich) = @Year AND LoaiGiaoDich = @Loai";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        cmd.Parameters.AddWithValue("@Year", currentYear);

                        cmd.Parameters.AddWithValue("@Month", currentMonth);
                        cmd.Parameters.AddWithValue("@Loai", "Thu");
                        decimal thuCurrent = cmd.ExecuteScalar() != DBNull.Value ? Convert.ToDecimal(cmd.ExecuteScalar()) : 0;
                        cmd.Parameters["@Loai"].Value = "Chi";
                        decimal chiCurrent = cmd.ExecuteScalar() != DBNull.Value ? Convert.ToDecimal(cmd.ExecuteScalar()) : 0;

                        cmd.Parameters["@Month"].Value = previousMonth;
                        cmd.Parameters["@Loai"].Value = "Thu";
                        decimal thuPrevious = cmd.ExecuteScalar() != DBNull.Value ? Convert.ToDecimal(cmd.ExecuteScalar()) : 0;
                        cmd.Parameters["@Loai"].Value = "Chi";
                        decimal chiPrevious = cmd.ExecuteScalar() != DBNull.Value ? Convert.ToDecimal(cmd.ExecuteScalar()) : 0;

                        if (thuCurrent > 0 || chiCurrent > 0 || thuPrevious > 0 || chiPrevious > 0) hasData = true;

                        seriesCurrentMonthThu.Points.AddXY(index+1, thuCurrent);
                        seriesCurrentMonthChi.Points.AddXY(index+1, chiCurrent);
                        seriesPreviousMonthThu.Points.AddXY(index, thuPrevious);
                        seriesPreviousMonthChi.Points.AddXY(index, chiPrevious);
                    }
                }
                chtThuChi.Series.Add(seriesCurrentMonthThu);
                chtThuChi.Series.Add(seriesCurrentMonthChi);
                chtThuChi.Series.Add(seriesPreviousMonthThu);
                chtThuChi.Series.Add(seriesPreviousMonthChi);
                chtThuChi.ChartAreas[0].AxisX.Title = "Tháng";
                chtThuChi.ChartAreas[0].AxisY.Title = "Số tiền (VND)";
                chtThuChi.ChartAreas[0].AxisX.CustomLabels.Clear();
                chtThuChi.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(0.5f, 1.5f, "Tháng " + previousMonth, 0, LabelMarkStyle.None));
                chtThuChi.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(1.5f, 2.5f, "Tháng " + currentMonth, 0, LabelMarkStyle.None));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!hasData)
            {
                chtThuChi.Hide();
                lblChartText.Text = "Chưa đủ dữ liệu để hiện biểu đồ";
                lblChartText.Visible = true;
               
            }
            else
            {
                chtThuChi.Show();
                lblChartText.Visible = false;
               
            }
        }

        private void LoadRecentTransactions()
        {
            dgvGiaoDichGanDay.Rows.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = @"
                        SELECT TOP 5
                            gd.NgayGiaoDich,
                            gd.GhiChu,
                            gd.LoaiGiaoDich,
                            hm.TenHangMuc,
                            gd.SoTien
                        FROM 
                            GiaoDich gd
                        INNER JOIN 
                            HangMuc hm ON gd.MaHangMuc = hm.MaHangMuc
                        WHERE 
                            gd.MaTaiKhoan = @MaTaiKhoan
                        ORDER BY 
                            gd.NgayGiaoDich DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvGiaoDichGanDay.Rows.Add(
                                    Convert.ToDateTime(reader["NgayGiaoDich"]).ToString("dd/MM/yyyy"),
                                    reader["GhiChu"].ToString(),
                                    reader["LoaiGiaoDich"].ToString(),
                                    reader["TenHangMuc"].ToString(),
                                    Convert.ToDecimal(reader["SoTien"]).ToString("N0") + " VND"
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBudgetProgress()
        {
            decimal tongChiTieu = 0, nganSachTongQuat = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT SUM(SoTien) FROM GiaoDich WHERE MaTaiKhoan = @MaTaiKhoan AND LoaiGiaoDich = 'Chi' AND MONTH(NgayGiaoDich) = @Month AND YEAR(NgayGiaoDich) = @Year";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        cmd.Parameters.AddWithValue("@Month", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                        object result = cmd.ExecuteScalar();
                        tongChiTieu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }

                    query = "SELECT TOP 1 SoTienNganSach FROM NganSach WHERE MaTaiKhoan = @MaTaiKhoan AND LoaiNganSach = 'TongQuat' AND MONTH(ThoiGianBatDau) = @Month AND YEAR(ThoiGianBatDau) = @Year";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        cmd.Parameters.AddWithValue("@Month", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                        object result = cmd.ExecuteScalar();
                        nganSachTongQuat = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
                decimal phanTram = 0;
                if (nganSachTongQuat == 0)
                {
                    phanTram = 0;
                    lblThongBaoNganSach.Text = "Bạn chưa thiết lập ngân sách tổng quát.";
                }
                else
                {
                    phanTram = (tongChiTieu / nganSachTongQuat) * 100;
                    string thongBao = "";
                    if (phanTram < 50)
                        thongBao = $"Bạn đã tiêu {phanTram:F2}% ngân sách.";
                    else if (phanTram < 80)
                        thongBao = $"Bạn đã tiêu {phanTram:F2}% ngân sách, hãy kiểm soát chi tiêu.";
                    else
                        thongBao = $"Bạn đã tiêu {phanTram:F2}% ngân sách, cần giảm chi tiêu ngay!";
                    lblThongBaoNganSach.Text = thongBao;
                }
                prgNganSach.Maximum = 100;
                prgNganSach.Value = (int)Math.Min(phanTram, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tiến trình ngân sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            WelcomeUser(this, EventArgs.Empty);
            LoadSummaryData();
            LoadChartData();
            LoadRecentTransactions();
            LoadBudgetProgress();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn đăng xuất?", "Đăng xuất?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                frmXacThuc xacThucForm = new frmXacThuc();
                xacThucForm.Show();
                this.Close();
            }
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            frmThongTinCaNhan thongTinForm = new frmThongTinCaNhan(_maTaiKhoan);
            thongTinForm.ShowDialog();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Nhập từ khóa...")
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmTimKiem timKiemForm = new frmTimKiem(_maTaiKhoan, txtSearch.Text.Trim());
            timKiemForm.ShowDialog();
        }

        private void btnHangMuc_Click(object sender, EventArgs e)
        {
            frmQuanLyHangMuc hanhMucForm = new frmQuanLyHangMuc(_maTaiKhoan);
            hanhMucForm.ShowDialog();

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmTaoBaoCao form = new frmTaoBaoCao(_maTaiKhoan); 
            form.ShowDialog();
        }

        private void btnNganSach_Click(object sender, EventArgs e)
        {
            frmNganSach nganSachform = new frmNganSach(_maTaiKhoan);
            nganSachform.ShowDialog();

        }

        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            frmQuanLyGiaoDich giaoDichForm = new frmQuanLyGiaoDich(_maTaiKhoan);
            giaoDichForm.ShowDialog();
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe form = new frmThongKe(_maTaiKhoan);
            form.ShowDialog();
        }

        private void linklblChiTietGiaoDich_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmQuanLyGiaoDich giaoDichForm = new frmQuanLyGiaoDich(_maTaiKhoan);
            giaoDichForm.ShowDialog();

        }

        private void linklblThemGiaoDich_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmThemGiaoDich themGiaoDichForm = new frmThemGiaoDich(_maTaiKhoan);
            themGiaoDichForm.ShowDialog();
        }

        private void linklblChiTietNganSach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNganSach nganSachForm = new frmNganSach(_maTaiKhoan);
            nganSachForm.ShowDialog();
        }

        private void btnImageRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
