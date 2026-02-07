
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace QuanLyTaiChinh
{
    public partial class frmThemHangMuc : Form
    {

        private readonly NghiepVuHangMuc _nghiepVu;
        private string _tenBieuTuong = string.Empty;
        public string TenHangMucMoi { get; private set; }
        public string TenBieuTuong => _tenBieuTuong;
        public frmThemHangMuc(NghiepVuHangMuc nghiepVu)
        {
            InitializeComponent();
            this._nghiepVu = nghiepVu;

            this.Text = $"Thêm danh mục ({_nghiepVu.LoaiDangChon})";

            _nghiepVu.LoadIconsToFlowLayout(flowLayoutPanelIcons, Pb_Click, _tenBieuTuong);
            if (flowLayoutPanelIcons.Controls.Count > 0)
            {
                Pb_Click(flowLayoutPanelIcons.Controls[0], EventArgs.Empty);
            }

            this.FormClosing += frmThemHangMuc_FormClosing;
        }


        public frmThemHangMuc(NghiepVuHangMuc nghiepVu, string tenCu, string iconCu) : this(nghiepVu)
        {
            txtTen.Text = tenCu;
            _tenBieuTuong = iconCu;
            this.Text = "Sửa danh mục";

            _nghiepVu.SelectIconAndPreview(_tenBieuTuong, pictureBoxPreview, flowLayoutPanelIcons);
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                _tenBieuTuong = pb.Tag.ToString();

                _nghiepVu.SelectIconAndPreview(_tenBieuTuong, pictureBoxPreview, flowLayoutPanelIcons);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TenBieuTuong))
            {
                MessageBox.Show("Vui lòng chọn biểu tượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TenHangMucMoi = txtTen.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmThemHangMuc_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
            }
            foreach (PictureBox pb in flowLayoutPanelIcons.Controls)
            {
                if (pb.Image != null)
                {
                    pb.Image.Dispose();
                }
            }
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
