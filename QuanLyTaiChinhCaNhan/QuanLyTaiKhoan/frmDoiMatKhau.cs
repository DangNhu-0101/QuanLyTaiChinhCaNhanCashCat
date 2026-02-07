using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmDoiMatKhau : Form
    {
        private int _maTaiKhoan;
        private string currentPassword;
        public string NewPassword { get; private set; }
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";

        public frmDoiMatKhau(int maTaiKhoan, string currentPassword)
        {
            InitializeComponent();
            _maTaiKhoan = maTaiKhoan;
            this.currentPassword = currentPassword;
        }
        private void rdoImgPass_MouseEnter(object sender, EventArgs e)
        {
            txtMatKhauCu.UseSystemPasswordChar = false; 
        }

        private void rdoImgPass_MouseLeave(object sender, EventArgs e)
        {
            txtMatKhauCu.UseSystemPasswordChar = true; 
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

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.Text != currentPassword)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhauMoi.Text) || string.IsNullOrEmpty(txtNhapLaiMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMatKhauMoi.Text != txtNhapLaiMatKhauMoi.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE MaTaiKhoan = @MaTaiKhoan AND MatKhau = @MatKhauCu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        cmd.Parameters.AddWithValue("@MatKhauCu", currentPassword);
                        cmd.Parameters.AddWithValue("@MatKhauMoi", txtMatKhauMoi.Text);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            NewPassword = txtMatKhauMoi.Text;
                            this.DialogResult = DialogResult.OK;
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDoiMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No) e.Cancel = true;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}