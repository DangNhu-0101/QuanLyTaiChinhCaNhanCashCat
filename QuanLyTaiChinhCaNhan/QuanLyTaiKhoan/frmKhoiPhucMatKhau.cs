using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmKhoiPhucMatKhau : Form
    {
        private int currentStep = 1;
        private string otpCode;
        private string emailOrPhone;
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";

        public frmKhoiPhucMatKhau()
        {
            InitializeComponent();
            ShowStep(currentStep);
        }

        private void ShowStep(int step)
        {
            currentStep = step;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Location = new Point(79, 87);
            panel2.Location = new Point(79, 87);
            panel3.Location = new Point(79, 87);
            switch (step)
            {
                case 1: panel1.Visible = true; break;
                case 2: panel2.Visible = true; break;
                case 3: panel3.Visible = true; break;
            }
        }

        private void btnXacNhan1_Click(object sender, EventArgs e)
        {
            emailOrPhone = txtEmailPhone.Text.Trim();
            if (IsValidEmailOrPhone(emailOrPhone))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @EmailOrPhone OR SoDienThoai = @EmailOrPhone";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@EmailOrPhone", emailOrPhone);
                            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                            {
                                Random rnd = new Random();
                                otpCode = rnd.Next(100000, 999999).ToString();
                                MessageBox.Show($"(Demo) Mã OTP của bạn là: {otpCode}", "Thông báo");
                                ShowStep(2);
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy tài khoản với email hoặc số điện thoại này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xác nhận email/số điện thoại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập Email hoặc Số điện thoại hợp lệ!\n- Email: Định dạng example@domain.com\n- Số điện thoại: 0[6-9][0-9]{8,9}");
            }
        }

        private void btnXacNhan2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOTP.Text) && txtOTP.Text == otpCode)
            {
                ShowStep(3);
            }
            else
            {
                MessageBox.Show("Mã OTP không đúng hoặc để trống!");
            }
        }

        private void btnXacNhan3_Click(object sender, EventArgs e)
        {
            if (IsValidPassword(txtMatKhauMoi.Text) && txtMatKhauMoi.Text == txtNhapLaiMatKhauMoi.Text)
            {
                string newPassword = txtMatKhauMoi.Text;
                try
                {
                    using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                    {
                        conn.Open();
                        string query = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE Email = @EmailOrPhone OR SoDienThoai = @EmailOrPhone";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MatKhau", newPassword);
                            cmd.Parameters.AddWithValue("@EmailOrPhone", emailOrPhone);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Mật khẩu đã được khôi phục thành công!");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Không thể cập nhật mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi khôi phục mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu mới không khớp hoặc không đáp ứng yêu cầu!\nYêu cầu: Ít nhất 8 ký tự, có chữ hoa, chữ thường và số.");
            }
        }
        private void rdoImgPassMoi_MouseEnter(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = false; 
        }
        private void rdoImgPassMoi_MouseLeave(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = true; 
        }
        private void rdoImgNhapLaiPassMoi_MouseEnter(object sender, EventArgs e)
        {
            txtNhapLaiMatKhauMoi.UseSystemPasswordChar = false;
        }
        private void rdoImgNhapLaiPassMoi_MouseLeave(object sender, EventArgs e)
        {
            txtNhapLaiMatKhauMoi.UseSystemPasswordChar = true; 
        }
        private bool IsValidEmailOrPhone(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(input, emailPattern)) return true;
            string phonePattern = @"^0[6-9][0-9]{8,9}$";
            return Regex.IsMatch(input, phonePattern);
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8) return false;
            return Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[0-9]");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}