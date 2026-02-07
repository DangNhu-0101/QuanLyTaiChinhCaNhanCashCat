using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmDangKy : Form
    {
        public string TenDangNhap => txtTenDangNhap.Text.Trim();
        public string MatKhau => txtMatKhau.Text.Trim();
        public string HoTen => txtHoTen.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string SoDienThoai => txtSoDienThoai.Text.Trim();

        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenDangNhap) || string.IsNullOrEmpty(MatKhau) ||
                string.IsNullOrEmpty(HoTen) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(SoDienThoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Email không hợp lệ! Định dạng: example@domain.com", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidPhone(SoDienThoai))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Định dạng: 0[6-9][0-9]{8,9}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("Server = LAPTOP-O8Q0L0EF\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True"))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                   

                    string insertQuery = "INSERT INTO TaiKhoan ( TenDangNhap, MatKhau, HoTen, Email, SoDienThoai) VALUES ( @TenDangNhap, @MatKhau, @HoTen, @Email, @SoDienThoai)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", MatKhau);
                        cmd.Parameters.AddWithValue("@HoTen", HoTen);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                        cmd.ExecuteNonQuery();
                    }
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;
            string phonePattern = @"^0[6-9][0-9]{8,9}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        private void rdoImgPass_MouseEnter(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = false; 
        }

        private void rdoImgPass_MouseLeave(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true; 
        }
        private void rdoImgCFPass_MouseEnter(object sender, EventArgs e)
        {
            txtNhapLaiMatKhau.UseSystemPasswordChar = false; 
        }

        private void rdoImgCFPass_MouseLeave(object sender, EventArgs e)
        {
            txtNhapLaiMatKhau.UseSystemPasswordChar = true; 
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}