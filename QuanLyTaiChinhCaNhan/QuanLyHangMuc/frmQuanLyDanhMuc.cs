
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace QuanLyTaiChinh    
{

    public partial class frmQuanLyHangMuc : Form
    {
        private readonly NghiepVuHangMuc _nghiepVu;
        private readonly DatabaseHelper _dbHelper;

        public frmQuanLyHangMuc(int maTaiKhoan)
        {
            InitializeComponent();

            _nghiepVu = new NghiepVuHangMuc(maTaiKhoan);


            listBoxHangMuc.DrawItem += listBoxHangMuc_DrawItem;
            LoadHangMuc();
        }

        private void LoadHangMuc()
        {
            listBoxHangMuc.Items.Clear();
            string error;
            List<HangMuc> HangMucs = _nghiepVu.LoadHangMuc(out error);
            if (string.IsNullOrWhiteSpace(error) == false) 
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var hm in HangMucs)
            {
                listBoxHangMuc.Items.Add(hm);
            }
        }


        private void btnQuanLyThu_Click(object sender, EventArgs e)
        {
            _nghiepVu.ChuyenLoaiVaTaiDuLieu("Thu");
            LoadHangMuc();
        }

        private void btnQuanLyChi_Click(object sender, EventArgs e)
        {
            _nghiepVu.ChuyenLoaiVaTaiDuLieu("Chi");
            LoadHangMuc();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {          
            using (frmThemHangMuc frm = new frmThemHangMuc(_nghiepVu))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string message;                   
                    bool success = _nghiepVu.XuLyThemHangMuc(frm.TenHangMucMoi, frm.TenBieuTuong, out message);
                    HienThiKetQua(success, message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listBoxHangMuc.SelectedItem is HangMuc hm)
            {         
                using (frmThemHangMuc frm = new frmThemHangMuc(_nghiepVu, hm.TenHangMuc, hm.BieuTuong))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        string message;                       
                        bool success = _nghiepVu.XuLyCapNhatHangMuc(hm, frm.TenHangMucMoi, frm.TenBieuTuong, out message);
                        HienThiKetQua(success, message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listBoxHangMuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var hm = listBoxHangMuc.SelectedItem as HangMuc;
                string message;
                bool success = _nghiepVu.XuLyXoaHangMuc(hm, out message);
                HienThiKetQua(success, message);
            }
        
        }

        private void HienThiKetQua(bool success, string message)
        {
            if (success)
            {
                LoadHangMuc();
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listBoxHangMuc_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            e.DrawFocusRectangle();

            HangMuc hm = listBoxHangMuc.Items[e.Index] as HangMuc;
            if (hm == null) return;


            _nghiepVu.DrawItemIcon(e.Graphics, e.Bounds, hm.BieuTuong);

            using (Brush brush = new SolidBrush(e.ForeColor))
            {
                float textY = e.Bounds.Top + (e.Bounds.Height - e.Font.Height) / 2;
                e.Graphics.DrawString(hm.TenHangMuc, e.Font, brush, e.Bounds.Left + 40, textY);
            }
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}