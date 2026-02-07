using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace QuanLyTaiChinhCaNhan
{

    public partial class frmTaoBaoCao : Form
    {
  
        private readonly NghiepVuPhanTichTraCuu _nghiepVu;

        public frmTaoBaoCao(int maTaiKhoan)
        {
            InitializeComponent();
            _nghiepVu = new NghiepVuPhanTichTraCuu(maTaiKhoan);

       
            cmbBaoCao.Items.Add("Báo cáo thu chi");
            cmbBaoCao.Items.Add("Báo cáo chi tiêu theo hạng mục");
            cmbBaoCao.SelectedIndex = 0;
            rdoImgDay.Checked = true;
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string loaiBaoCao = cmbBaoCao.SelectedItem == null ? null : cmbBaoCao.SelectedItem.ToString();
                if (string.IsNullOrEmpty(loaiBaoCao))
                {
                    MessageBox.Show("Vui lòng chọn loại báo cáo.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                (string thoiGian, string loaiThoiGian) = _nghiepVu.GetThoiGianAndType(
                    rdoImgDay.Checked, rdoImgWeek.Checked, rdoImgMonth.Checked, rdoImgYear.Checked);
                if (string.IsNullOrEmpty(thoiGian) || string.IsNullOrEmpty(loaiThoiGian))
                {
                    MessageBox.Show("Vui lòng chọn loại thời gian báo cáo.", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                switch (loaiBaoCao)
                {
                    case "Báo cáo thu chi":                    
                        frmBaoCaoTongThuChi frmThuChi = new frmBaoCaoTongThuChi(_nghiepVu, thoiGian);
                        frmThuChi.ShowDialog();
                        break;
                    case "Báo cáo chi tiêu theo hạng mục":
                        frmBaoCaoChiTieuTheoHangMuc frmChiTieu = new frmBaoCaoChiTieuTheoHangMuc(_nghiepVu, thoiGian);
                        frmChiTieu.ShowDialog();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTaoBaoCao_Load(object sender, EventArgs e)
        {

        }
    }
}