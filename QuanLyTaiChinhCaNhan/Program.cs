using log4net;
using log4net.Config;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace QuanLyTaiChinhCaNhan
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            log.Info("--- UNG DUNG BAT DAU---");

            
            bool KetNoiThanhCong = false;
            string ThongBaoLoi = "";
            try
            {
                ConnectionStringSettings CaiDat = ConfigurationManager.ConnectionStrings["QuanLyChiTieuConnectionString"];

                if (CaiDat == null)
                {
                    throw new FileNotFoundException("Khong tim thay chuoi ket noi trong App.config!\nTen: QuanLyChiTieuConnectionString");

                }
                string ChuoiKetNoi = CaiDat.ConnectionString;

                SqlConnection KetNoi = new SqlConnection(ChuoiKetNoi);


                KetNoi.Open();

                SqlCommand Len = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan", KetNoi);


                object KetQua = Len.ExecuteScalar();


                if (KetQua == null)
                {
                    throw new InvalidOperationException("Khong the doc du lieu tu bang tai khoan");

                }
                Len.Dispose();
                KetNoi.Close();
                KetNoi.Dispose();
                KetNoiThanhCong = true;
            }
            catch (FileNotFoundException ex)
            {
                ThongBaoLoi = ex.Message;

            }
            catch (InvalidOperationException ex)
            {
                ThongBaoLoi = ex.Message;
            }
            catch (SqlException ex)
            {
                ThongBaoLoi = "Loi ket noi CSDL (Ma: " + ex.Number + ")\n" + ex.Message;
            }
            catch (Exception ex)
            {
                ThongBaoLoi = "Loi khac: " + ex.Message;
            }
            finally
            {
                if (KetNoiThanhCong==true)
                {
                    MessageBox.Show(
                        "Ket noi thanh cong!\nUng dung san sang",
                        "Thanh cong",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Khong the khoi dong!\n\n" + ThongBaoLoi,
                        "Loi CSDL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            if (KetNoiThanhCong==true)
            {
                Application.Run(new frmXacThuc());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}