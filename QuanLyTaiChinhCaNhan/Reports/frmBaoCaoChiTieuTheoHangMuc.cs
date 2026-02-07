using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmBaoCaoChiTieuTheoHangMuc : Form
    {

        private readonly NghiepVuPhanTichTraCuu _nghiepVu;

        private DateTime thoiGian;
        private string loaiThoiGian;

        public frmBaoCaoChiTieuTheoHangMuc(NghiepVuPhanTichTraCuu nghiepVu, string thoiGianString)
        {
            InitializeComponent();
            _nghiepVu = nghiepVu;

            (this.thoiGian, this.loaiThoiGian) = _nghiepVu.ParseThoiGianToDateTime(thoiGianString);

            Console.WriteLine($"[frmBaoCaoChiTieuTheoHangMuc] Parsed thoiGian: {this.thoiGian:yyyy-MM-dd HH:mm:ss}");
        }

        
        private void frmBaoCaoChiTieuTheoHangMuc_Load(object sender, EventArgs e)
        {
            try
            {
                if (crvCategory == null) return;
                ReportDocument reportDocument = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "Reports", "rptChiTieuTheoHangMuc.rpt");
                if (File.Exists(reportPath) == false)
                {
                    MessageBox.Show($"Không tìm thấy file báo cáo: {reportPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportDocument.Load(reportPath);
                DataSet ds = _nghiepVu.GetReportData_ChiTieuTheoHangMuc(thoiGian, loaiThoiGian);
                if (ds.Tables.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu, vui lòng tạo giao dịch mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu, vui lòng tạo giao dịch mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                reportDocument.SetDataSource(ds.Tables[0]);
                reportDocument.SetParameterValue("@ThoiGian", thoiGian.ToString("MM/dd/yyyy"));
                reportDocument.SetParameterValue("@LoaiThoiGian", loaiThoiGian);

                crvCategory.ReportSource = reportDocument;
                crvCategory.RefreshReport();
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
                MessageBox.Show($"Lỗi chi tiết: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
    
