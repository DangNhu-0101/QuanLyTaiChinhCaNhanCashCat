
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiChinhCaNhan
{
    public partial class frmTimKiem : Form
    {

        private readonly NghiepVuPhanTichTraCuu _nghiepVu;
        private readonly string _initialKeyword;

        public frmTimKiem(int maTaiKhoan, string initialKeyword)
        {
            InitializeComponent();
            _nghiepVu = new NghiepVuPhanTichTraCuu(maTaiKhoan);
            _initialKeyword = initialKeyword ?? "";
            LoadLoaiGiaoDich();
            LoadHangMuc();
            SetInitialControl(); 
            LoadInitialData();
        }
 
        private void LoadLoaiGiaoDich()
        {
            try
            {
                DataTable dt = _nghiepVu.LoadOptions("LoaiGiaoDich");
                cbLoaiGiaoDich.Items.Clear();
                cbLoaiGiaoDich.Items.Add("");
                foreach (DataRow row in dt.Rows)
                {
                    cbLoaiGiaoDich.Items.Add(row["LoaiGiaoDich"].ToString());
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }
                MessageBox.Show("Lỗi khi tải loại giao dịch: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void LoadHangMuc()
        {
            try
            {
                DataTable dt = _nghiepVu.LoadOptions("HangMuc");
                cbTenHangMuc.Items.Clear();
                cbTenHangMuc.Items.Add("");
                foreach (DataRow row in dt.Rows)
                {
                    cbTenHangMuc.Items.Add(row["TenHangMuc"].ToString());
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show("Lỗi khi tải hạng mục: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void PopulateDataGrid(DataTable dt)
        {
            dgvKetQua.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvKetQua.Rows.Add(
                    row["NgayGiaoDich"].ToString(),
                    row["GhiChu"].ToString(),
                    row["LoaiGiaoDich"].ToString(),
                    row["TenHangMuc"].ToString(),
                    Convert.ToDecimal(row["SoTien"]).ToString("N0") + " VND"
                );
            }
        }


        private void LoadInitialData()
        {
            try
            {
                var (query, parameters) = _nghiepVu.BuildFilterQuery(
                    _initialKeyword, keyword: "",
                    tuNgay: DateTime.MinValue, denNgay: DateTime.MinValue,
                    loaiGiaoDich: "", tenHangMuc: "",
                    tuSoTien: 0, denSoTien: 1000000000,
                    isInitialLoad: true);
                DataTable dt = _nghiepVu.ExecuteQuery(query, parameters);
                PopulateDataGrid(dt);

                if (dgvKetQua.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy giao dịch nào với từ khóa: " + _initialKeyword,
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show("Lỗi khi tải dữ liệu giao dịch: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
        private void btnFilter_Click(object sender, EventArgs e)
        {
  
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FilterData();
        }
        private void FilterData()
        {
            try
            {
                var (query, parameters) = _nghiepVu.BuildFilterQuery(
                   initialKeyword: "", keyword: txtKeyword.Text,
                   tuNgay: dtpTuNgay.Value, denNgay: dtpDenNgay.Value,
                   loaiGiaoDich: cbLoaiGiaoDich.Text, tenHangMuc: cbTenHangMuc.Text,
                   tuSoTien: nudTuSoTien.Value, denSoTien: nudDenSoTien.Value,
                   isInitialLoad: false);
                DataTable dt = _nghiepVu.ExecuteQuery(query, parameters);
                PopulateDataGrid(dt);
                if (dgvKetQua.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy giao dịch nào theo bộ lọc hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string errorMessage;
                if (ex.InnerException != null)
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                MessageBox.Show("Lỗi khi lọc dữ liệu: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInitialControl()
        {
            txtKeyword.Text = "";
            dtpTuNgay.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDenNgay.Value = DateTime.Today;
            cbLoaiGiaoDich.SelectedIndex = 0;
            cbTenHangMuc.SelectedIndex = 0;
            nudTuSoTien.Value = 0;
            nudDenSoTien.Value = 1000000000;
        }


        
        private void btnClear_Click(object sender, EventArgs e)
        {

            LoadInitialData();
            SetInitialControl();
        }

        
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}