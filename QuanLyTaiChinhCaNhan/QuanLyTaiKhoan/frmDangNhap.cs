using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmDangNhap : Form
    {
        private string chuoiketnoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyChiTieu;Integrated Security=True";
        public int MaTaiKhoan { get; private set; }

        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void rdoImgPass_MouseEnter(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = false; 
        }

        private void rdoImgPass_MouseLeave(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true; 
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT MaTaiKhoan FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MaTaiKhoan = Convert.ToInt32(result);
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linklblKhoiPhucMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmKhoiPhucMatKhau frmKhoiPhuc = new frmKhoiPhucMatKhau();
            frmKhoiPhuc.ShowDialog();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linklblDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy frmDangKy = new frmDangKy();
            frmDangKy.ShowDialog();
        }

       
    }
}