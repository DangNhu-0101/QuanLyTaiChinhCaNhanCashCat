using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmThongTinCaNhan : Form
    {
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";
        private int _maTaiKhoan;
        private string _hoTenOriginal;
        private string _tenDangNhapOriginal;
        private string _emailOriginal;
        private string _soDienThoaiOriginal;

        public frmThongTinCaNhan(int maTaiKhoan)
        {
            InitializeComponent();
            _maTaiKhoan = maTaiKhoan;
            LoadDuLieu();
            SetControlsReadOnly(true);
            btnHuy.Visible = false; 
        }

        private void LoadDuLieu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "SELECT TenDangNhap, HoTen, MatKhau, Email, SoDienThoai FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _tenDangNhapOriginal = reader["TenDangNhap"].ToString();
                                txtTenDangNhap.Text = _tenDangNhapOriginal;
                                _hoTenOriginal = reader["HoTen"].ToString();
                                txtHoTen.Text = _hoTenOriginal;
                                txtMatKhau.Text = reader["MatKhau"].ToString();
                                _emailOriginal = reader["Email"].ToString();
                                txtEmail.Text = _emailOriginal;
                                _soDienThoaiOriginal = reader["SoDienThoai"].ToString();
                                txtSoDienThoai.Text = _soDienThoaiOriginal;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin cá nhân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetControlsReadOnly(bool readOnly)
        {
            txtHoTen.ReadOnly = readOnly;
            txtTenDangNhap.ReadOnly = readOnly;
            txtMatKhau.ReadOnly = true;
            txtEmail.ReadOnly = readOnly;
            txtSoDienThoai.ReadOnly = readOnly;
            btnUpdate.Text = readOnly ? "Cập nhật" : "Lưu";
        }

        private bool isEditing = false;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true;
                SetControlsReadOnly(false);
                btnHuy.Visible = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                    {
                        conn.Open();
                        string query = "UPDATE TaiKhoan SET HoTen = @HoTen, TenDangNhap = @TenDangNhap, Email = @Email, SoDienThoai = @SoDienThoai WHERE MaTaiKhoan = @MaTaiKhoan";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                            cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                            cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    _hoTenOriginal = txtHoTen.Text;
                    _tenDangNhapOriginal = txtTenDangNhap.Text;
                    _emailOriginal = txtEmail.Text;
                    _soDienThoaiOriginal = txtSoDienThoai.Text;
                    MessageBox.Show("Thông tin đã được cập nhật!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isEditing = false;
                btnHuy.Visible = false;
                SetControlsReadOnly(true);
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhauForm = new frmDoiMatKhau(_maTaiKhoan, txtMatKhau.Text);
            if (doiMatKhauForm.ShowDialog() == DialogResult.OK)
            {
                txtMatKhau.Text = doiMatKhauForm.NewPassword;
                MessageBox.Show("Mật khẩu đã được đổi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void rdoImgPass_MouseEnter(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = false;
        }

        private void rdoImgPass_MouseLeave(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                txtHoTen.Text = _hoTenOriginal;
                txtTenDangNhap.Text = _tenDangNhapOriginal;
                txtEmail.Text = _emailOriginal;
                txtSoDienThoai.Text = _soDienThoaiOriginal;
                isEditing = false;
                btnHuy.Visible = false;
                SetControlsReadOnly(true);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa tài khoản này? Hành động này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                    {
                        conn.Open();
                        // Thực hiện xóa tài khoản
                        string query = "DELETE FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Tài khoản đã được xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                System.Diagnostics.Process.Start(Application.ExecutablePath);
                                Environment.Exit(0); 



                            }
                            else
                            {
                                MessageBox.Show("Không thể xóa tài khoản. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}