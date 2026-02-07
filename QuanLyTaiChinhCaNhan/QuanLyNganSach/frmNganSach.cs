using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNganSach
{

    public partial class frmNganSach : Form
    {
        private readonly DatabaseConnection db;
        private int _maTaiKhoan;
        public frmNganSach(int maTaiKhoan)
        {
            InitializeComponent();
            _maTaiKhoan = maTaiKhoan;
            db = new DatabaseConnection();
            cboNganSachHangMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            InitializeDataGridView();
            LoadNganSachHangMuc();
            LoadNganSach();
            dgvNganSach.DataError += dgvNganSach_DataError;
            dgvNganSach.SelectionChanged += dgvNganSach_SelectionChanged;
            
        }

        private void InitializeDataGridView()
        {
            try
            {

                dgvNganSach.Columns.Clear();
                dgvNganSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvNganSach.AllowUserToAddRows = false;
                dgvNganSach.ReadOnly = true;

                var colMaNganSach = new DataGridViewTextBoxColumn
                {
                    Name = "MaNganSach",
                    HeaderText = "Mã Ngân Sách",
                    DataPropertyName = "MaNganSach",
                    FillWeight = 5 
                };
                var colTenHangMuc = new DataGridViewTextBoxColumn
                {
                    Name = "TenHangMuc",
                    HeaderText = "Tên Hạng Mục",
                    DataPropertyName = "TenHangMuc",
                    FillWeight = 20 
                };
                colTenHangMuc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                var colSoTienNganSach = new DataGridViewTextBoxColumn
                {
                    Name = "SoTienNganSach",
                    HeaderText = "Số Tiền",
                    DataPropertyName = "SoTienNganSach",
                    FillWeight = 20 
                };
                var colThoiGianBatDau = new DataGridViewTextBoxColumn
                {
                    Name = "ThoiGianBatDau",
                    HeaderText = "Ngày Bắt Đầu",
                    DataPropertyName = "ThoiGianBatDau",
                    FillWeight = 20 
                };
                var colThoiGianKetThuc = new DataGridViewTextBoxColumn
                {
                    Name = "ThoiGianKetThuc",
                    HeaderText = "Ngày Kết Thúc",
                    DataPropertyName = "ThoiGianKetThuc",
                    FillWeight = 20 
                };

                var colTienDo = new DataGridViewProgressColumn
                {
                    Name = "TienDo",
                    HeaderText = "Tiến Độ",
                    DataPropertyName = "TienDo",
                    FillWeight = 15 
                };
                colTienDo.MinimumWidth = 120;
                colTienDo.Width = 180;

                dgvNganSach.Columns.AddRange(new DataGridViewColumn[] { colMaNganSach, colTenHangMuc, colSoTienNganSach, colThoiGianBatDau, colThoiGianKetThuc,colTienDo });

                dgvChiTiet.Columns.Clear();
                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvChiTiet.ReadOnly = true;
                dgvChiTiet.AllowUserToAddRows = false;


                var colChiTietMaGiaoDich = new DataGridViewTextBoxColumn
                {
                    Name = "MaGiaoDich",
                    HeaderText = "Mã Giao Dịch",
                    DataPropertyName = "MaGiaoDich",
                    FillWeight = 5 
                };
                var colChiTietTenHangMuc = new DataGridViewTextBoxColumn
                {
                    Name = "TenHangMuc",
                    HeaderText = "Hạng Mục",
                    DataPropertyName = "TenHangMuc",
                    FillWeight = 20
                };
                colChiTietTenHangMuc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                var colSoTienChi = new DataGridViewTextBoxColumn
                {
                    Name = "SoTienChi",
                    HeaderText = "Số Tiền Chi",
                    DataPropertyName = "SoTienChi",
                    FillWeight = 20
                };
                var colChiTietTienDo = new DataGridViewProgressColumn
                {
                    Name = "TienDo",
                    HeaderText = "Tiến Độ (%)",
                    DataPropertyName = "TienDo",
                    FillWeight = 20 
                };
                colChiTietTienDo.MinimumWidth = 120;
                colChiTietTienDo.Width = 180;

                var colNgayGiaoDich = new DataGridViewTextBoxColumn
                {
                    Name = "NgayGiaoDich",
                    HeaderText = "Ngày Giao Dịch",
                    DataPropertyName = "NgayGiaoDich",
                    FillWeight = 15 
                };
                var colGhiChu = new DataGridViewTextBoxColumn
                {
                    Name = "GhiChu",
                    HeaderText = "Ghi Chú",
                    DataPropertyName = "GhiChu",
                    FillWeight = 20 
                };
                colGhiChu.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { colChiTietMaGiaoDich, colChiTietTenHangMuc, colSoTienChi, colChiTietTienDo, colNgayGiaoDich, colGhiChu });

                dgvNganSach.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvChiTiet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo DataGridView: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNganSachHangMuc()
        {
           

            try
            {
                cboNganSachHangMuc.Items.Clear();
                cboNganSachHangMuc.Items.Add(new ComboBoxItem("Tổng Quát", "TongQuat", true));

                string query = "SELECT MaHangMuc, TenHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan";
                SqlParameter[] parameters = { new SqlParameter("@MaTaiKhoan", _maTaiKhoan) };
                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    cboNganSachHangMuc.Items.Add(new ComboBoxItem(
                        row["TenHangMuc"].ToString(),
                        row["MaHangMuc"].ToString(),
                        false
                    ));
                }

                cboNganSachHangMuc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hạng mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNganSach()
        {
            try
            {
                string query = @"
                    SELECT 
                        ns.MaNganSach,
                        ISNULL(hm.TenHangMuc, N'Tổng Quát') AS TenHangMuc,
                        ns.SoTienNganSach,
                        ns.ThoiGianBatDau,
                        ns.ThoiGianKetThuc,
                        ISNULL(CAST((
                            SELECT SUM(gd.SoTien) * 100 / NULLIF(ns.SoTienNganSach, 0)
                            FROM GiaoDich gd
                            WHERE (ns.MaHangMuc IS NULL OR gd.MaHangMuc = ns.MaHangMuc)
                            AND gd.NgayGiaoDich BETWEEN ns.ThoiGianBatDau AND ns.ThoiGianKetThuc
                            AND gd.LoaiGiaoDich = 'Chi'
                        ) AS INT), 0) AS TienDo
                    FROM NganSach ns
                    LEFT JOIN HangMuc hm ON ns.MaHangMuc = hm.MaHangMuc
                    WHERE ns.MaTaiKhoan = @MaTaiKhoan";

                SqlParameter[] parameters = { new SqlParameter("@MaTaiKhoan", _maTaiKhoan) };
                DataTable dt = db.ExecuteQuery(query, parameters);

                foreach (DataRow row in dt.Rows)
                {

                    if (row["TienDo"] == DBNull.Value)
                        row["TienDo"] = 0;
                    else
                        row["TienDo"] = Convert.ToInt32(row["TienDo"]);
                }

                dgvNganSach.DataSource = dt;
                if (dgvNganSach.Columns.Contains("TienDo"))
                {
                    dgvNganSach.InvalidateColumn(dgvNganSach.Columns["TienDo"].Index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietNganSachTongQuat(int maNganSach, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, decimal soTienNganSach)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("TenHangMuc", typeof(string));
                dt.Columns.Add("SoTienChi", typeof(decimal));
                dt.Columns.Add("TienDo", typeof(int));
                dt.Columns.Add("MaGiaoDich", typeof(int));
                dt.Columns.Add("NgayGiaoDich", typeof(DateTime));
                dt.Columns.Add("GhiChu", typeof(string));

                string queryGiaoDich = @"
                    SELECT 
                        hm.TenHangMuc,
                        gd.SoTien AS SoTienChi,
                        gd.MaGiaoDich,
                        gd.NgayGiaoDich,
                        gd.GhiChu
                    FROM GiaoDich gd
                    INNER JOIN HangMuc hm ON gd.MaHangMuc = hm.MaHangMuc
                    WHERE gd.MaTaiKhoan = @MaTaiKhoan
                    AND gd.LoaiGiaoDich = 'Chi'
                    AND gd.NgayGiaoDich BETWEEN @ThoiGianBatDau AND @ThoiGianKetThuc
                    ORDER BY hm.TenHangMuc, gd.NgayGiaoDich";

                SqlParameter[] parametersGiaoDich = {
                    new SqlParameter("@MaTaiKhoan", _maTaiKhoan),
                    new SqlParameter("@ThoiGianBatDau", thoiGianBatDau),
                    new SqlParameter("@ThoiGianKetThuc", thoiGianKetThuc)
                };

                DataTable dtGiaoDich = db.ExecuteQuery(queryGiaoDich, parametersGiaoDich);

                string queryTongChi = @"
                    SELECT 
                        hm.TenHangMuc,
                        ISNULL(SUM(gd.SoTien), 0) AS SoTienChi,
                        ISNULL(CAST(SUM(gd.SoTien) * 100 / NULLIF(@SoTienNganSach, 0) AS INT), 0) AS TienDo
                    FROM GiaoDich gd
                    INNER JOIN HangMuc hm ON gd.MaHangMuc = hm.MaHangMuc
                    WHERE gd.MaTaiKhoan = @MaTaiKhoan
                    AND gd.LoaiGiaoDich = 'Chi'
                    AND gd.NgayGiaoDich BETWEEN @ThoiGianBatDau AND @ThoiGianKetThuc
                    GROUP BY hm.TenHangMuc
                    ORDER BY hm.TenHangMuc";

                SqlParameter[] parametersTongChi = {
                    new SqlParameter("@MaTaiKhoan", _maTaiKhoan),
                    new SqlParameter("@ThoiGianBatDau", thoiGianBatDau),
                    new SqlParameter("@ThoiGianKetThuc", thoiGianKetThuc),
                    new SqlParameter("@SoTienNganSach", soTienNganSach)
                };

                DataTable dtTongChi = db.ExecuteQuery(queryTongChi, parametersTongChi);

                foreach (DataRow rowGiaoDich in dtGiaoDich.Rows)
                {
                    string tenHangMuc = rowGiaoDich["TenHangMuc"].ToString();
                    DataRow newRow = dt.NewRow();
                    newRow["TenHangMuc"] = tenHangMuc;
                    newRow["SoTienChi"] = rowGiaoDich["SoTienChi"];
                    newRow["MaGiaoDich"] = rowGiaoDich["MaGiaoDich"];
                    newRow["NgayGiaoDich"] = rowGiaoDich["NgayGiaoDich"];
                    newRow["GhiChu"] = rowGiaoDich["GhiChu"];

                    foreach (DataRow rowTongChi in dtTongChi.Rows)
                    {
                        if (rowTongChi["TenHangMuc"].ToString() == tenHangMuc)
                        {
                            newRow["TienDo"] = rowTongChi["TienDo"];
                            break;
                        }
                    }

                    dt.Rows.Add(newRow);
                }

                dgvChiTiet.DataSource = dt;
                if (dgvChiTiet.Columns.Contains("TienDo"))
                {
                    dgvChiTiet.InvalidateColumn(dgvChiTiet.Columns["TienDo"].Index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết ngân sách Tổng Quát: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvChiTiet.DataSource = null;
            }
        }

        private bool CheckDuplicateNganSach(ComboBoxItem selectedItem, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, int? maNganSach = null)
        {
            try
            {
                string query;
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@MaTaiKhoan", _maTaiKhoan),
                    new SqlParameter("@ThoiGianBatDau", thoiGianBatDau),
                    new SqlParameter("@ThoiGianKetThuc", thoiGianKetThuc)
                };

                if (selectedItem.IsTongQuat)
                {
                    query = @"
                        SELECT COUNT(*) 
                        FROM NganSach 
                        WHERE MaTaiKhoan = @MaTaiKhoan 
                        AND (ThoiGianBatDau <= @ThoiGianKetThuc AND ThoiGianKetThuc >= @ThoiGianBatDau)";
                }
                else
                {
                    query = @"
                        SELECT COUNT(*) 
                        FROM NganSach 
                        WHERE MaTaiKhoan = @MaTaiKhoan 
                        AND (
                            MaHangMuc = @MaHangMuc 
                            OR MaHangMuc IS NULL
                        )
                        AND (ThoiGianBatDau <= @ThoiGianKetThuc AND ThoiGianKetThuc >= @ThoiGianBatDau)";
                    parameters.Add(new SqlParameter("@MaHangMuc", selectedItem.Value));
                }

                if (maNganSach.HasValue)
                {
                    query += " AND MaNganSach != @MaNganSach";
                    parameters.Add(new SqlParameter("@MaNganSach", maNganSach.Value));
                }

                DataTable dt = db.ExecuteQuery(query, parameters.ToArray());
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra trùng lặp ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; 
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                ComboBoxItem selectedItem = (ComboBoxItem)cboNganSachHangMuc.SelectedItem;

                if (CheckDuplicateNganSach(selectedItem, dtpThoiGianBatDau.Value, dtpThoiGianKetThuc.Value))
                {
                    string message = selectedItem.IsTongQuat
                        ? "Đã tồn tại ngân sách trong khoảng thời gian này! Không thể thêm ngân sách Tổng Quát."
                        : $"Đã tồn tại ngân sách Tổng Quát hoặc ngân sách cho hạng mục {selectedItem.DisplayText} trong khoảng thời gian này!";
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
                    INSERT INTO NganSach (MaTaiKhoan, MaHangMuc, SoTienNganSach, LoaiNganSach, ThoiGianBatDau, ThoiGianKetThuc)
                    VALUES (@MaTaiKhoan, @MaHangMuc, @SoTien, @LoaiNganSach, @ThoiGianBatDau, @ThoiGianKetThuc)";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaTaiKhoan", _maTaiKhoan),
                    new SqlParameter("@MaHangMuc", selectedItem.IsTongQuat ? DBNull.Value : (object)selectedItem.Value),
                    new SqlParameter("@SoTien", decimal.Parse(txtSoTien.Text)),
                    new SqlParameter("@LoaiNganSach", selectedItem.IsTongQuat ? "TongQuat" : "HangMuc"),
                    new SqlParameter("@ThoiGianBatDau", dtpThoiGianBatDau.Value),
                    new SqlParameter("@ThoiGianKetThuc", dtpThoiGianKetThuc.Value),
                };

                db.ExecuteNonQuery(query, parameters);
                LoadNganSach();
                MessageBox.Show("Thêm ngân sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNganSach.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một ngân sách để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                int maNganSach = Convert.ToInt32(dgvNganSach.SelectedRows[0].Cells["MaNganSach"].Value);
                ComboBoxItem selectedItem = (ComboBoxItem)cboNganSachHangMuc.SelectedItem;

                if (CheckDuplicateNganSach(selectedItem, dtpThoiGianBatDau.Value, dtpThoiGianKetThuc.Value, maNganSach))
                {
                    string message = selectedItem.IsTongQuat
                        ? "Đã tồn tại ngân sách trong khoảng thời gian này! Không thể sửa thành ngân sách Tổng Quát."
                        : $"Đã tồn tại ngân sách Tổng Quát hoặc ngân sách cho hạng mục {selectedItem.DisplayText} trong khoảng thời gian này!";
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
                    UPDATE NganSach
                    SET MaHangMuc = @MaHangMuc, SoTienNganSach = @SoTien, LoaiNganSach = @LoaiNganSach,
                        ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc
                    WHERE MaNganSach = @MaNganSach";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaNganSach", maNganSach),
                    new SqlParameter("@MaHangMuc", selectedItem.IsTongQuat ? DBNull.Value : (object)selectedItem.Value),
                    new SqlParameter("@SoTien", decimal.Parse(txtSoTien.Text)),
                    new SqlParameter("@LoaiNganSach", selectedItem.IsTongQuat ? "TongQuat" : "HangMuc"),
                    new SqlParameter("@ThoiGianBatDau", dtpThoiGianBatDau.Value),
                    new SqlParameter("@ThoiGianKetThuc", dtpThoiGianKetThuc.Value),
                };

                db.ExecuteNonQuery(query, parameters);
                LoadNganSach();
                MessageBox.Show("Sửa ngân sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNganSach.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một ngân sách để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa ngân sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int maNganSach = Convert.ToInt32(dgvNganSach.SelectedRows[0].Cells["MaNganSach"].Value);
                    string query = "DELETE FROM NganSach WHERE MaNganSach = @MaNganSach";
                    SqlParameter[] parameters = { new SqlParameter("@MaNganSach", maNganSach) };
                    db.ExecuteNonQuery(query, parameters);
                    LoadNganSach();
                    MessageBox.Show("Xóa ngân sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNganSach_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvNganSach.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgvNganSach.SelectedRows[0];
                    string tenHangMuc = row.Cells["TenHangMuc"].Value.ToString();
                    txtSoTien.Text = row.Cells["SoTienNganSach"].Value.ToString();
                    dtpThoiGianBatDau.Value = Convert.ToDateTime(row.Cells["ThoiGianBatDau"].Value);
                    dtpThoiGianKetThuc.Value = Convert.ToDateTime(row.Cells["ThoiGianKetThuc"].Value);

                    foreach (ComboBoxItem item in cboNganSachHangMuc.Items)
                    {
                        if (item.DisplayText == tenHangMuc)
                        {
                            cboNganSachHangMuc.SelectedItem = item;
                            break;
                        }
                    }

                    if (tenHangMuc == "Tổng Quát")
                    {
                        int maNganSach = Convert.ToInt32(row.Cells["MaNganSach"].Value);
                        decimal soTienNganSach = Convert.ToDecimal(row.Cells["SoTienNganSach"].Value);
                        LoadChiTietNganSachTongQuat(maNganSach, dtpThoiGianBatDau.Value, dtpThoiGianKetThuc.Value, soTienNganSach);
                    }
                    else
                    {
                        dgvChiTiet.DataSource = null; 
                    }
                }
                else
                {
                    dgvChiTiet.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn ngân sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvChiTiet.DataSource = null;
            }
        }

        private void dgvNganSach_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show($"Lỗi dữ liệu tại cột {dgvNganSach.Columns[e.ColumnIndex].HeaderText}, dòng {e.RowIndex + 1}: {e.Exception.Message}", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSoTien.Text) || !decimal.TryParse(txtSoTien.Text, out decimal soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpThoiGianBatDau.Value > dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboNganSachHangMuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại ngân sách hoặc hạng mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}