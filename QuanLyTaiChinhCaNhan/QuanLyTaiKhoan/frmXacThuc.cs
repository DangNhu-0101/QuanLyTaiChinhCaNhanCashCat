using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmXacThuc : Form
    {
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";

        public frmXacThuc()
        {
            InitializeComponent();
        }
    


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            frmDangNhap dn = new frmDangNhap();
            if (dn.ShowDialog() == DialogResult.OK)
            {
                int maTaiKhoan = dn.MaTaiKhoan;
                frmTrangChu trangChuForm = new frmTrangChu(maTaiKhoan);
                trangChuForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đã hủy đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            frmDangKy dk = new frmDangKy();
            if (dk.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã hủy đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmXacThuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No) e.Cancel = true;

        }

      
    }
}