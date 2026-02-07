using Firebase.Database;
using QuanLyTaiChinhCaNhan;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database.Query;
namespace QuanLyGiaoDich
{
    public partial class frmQuanLyGiaoDich : Form
    {
        private readonly int _maTaiKhoan;
        private DataTable dtGiaoDichThu;
        private DataTable dtGiaoDichChi;
        private readonly GiaoDichDataHelper dbHelper;
        private readonly FirebaseClient firebaseClient;
        private System.Windows.Forms.Timer syncTimer;

        public frmQuanLyGiaoDich(int maTaiKhoan)
        {
            InitializeComponent();
            _maTaiKhoan = maTaiKhoan;
            dbHelper = new GiaoDichDataHelper(maTaiKhoan);
            InitializeSyncTimer();
        }

        private async void frmQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            InitializeDataTables();
            await LoadData();
            SetupComboBox();
            btnSua.Enabled = false;
            btnSuaChi.Enabled = false;
            dgvGiaoDich.SelectionChanged += (s, ev) => btnSua.Enabled = dgvGiaoDich.SelectedRows.Count > 0;
            dgvGiaoDichChi.SelectionChanged += (s, ev) => btnSuaChi.Enabled = dgvGiaoDichChi.SelectedRows.Count > 0;

            btnXoa.Enabled = false;
            btnXoaChi.Enabled = false;
            dgvGiaoDich.SelectionChanged += (s, ev) => btnXoa.Enabled = dgvGiaoDich.SelectedRows.Count > 0;
            dgvGiaoDichChi.SelectionChanged += (s, ev) => btnXoaChi.Enabled = dgvGiaoDichChi.SelectedRows.Count > 0;
        }

        private void InitializeDataTables()
        {
            dtGiaoDichThu = new DataTable();
            dtGiaoDichChi = new DataTable();
            foreach (var dt in new[] { dtGiaoDichThu, dtGiaoDichChi })
            {
                dt.Columns.Add("Mã Giao Dịch", typeof(int));
                dt.Columns.Add("Hạng Mục", typeof(string));
                dt.Columns.Add("Số Tiền", typeof(decimal));
                dt.Columns.Add("Loại Giao Dịch", typeof(string));
                dt.Columns.Add("Ghi Chú", typeof(string));
                dt.Columns.Add("Ngày Giao Dịch", typeof(DateTime));
                dt.Columns.Add("Trạng Thái Đồng Bộ", typeof(string));
                dt.Columns.Add("Thời Điểm Chỉnh Sửa", typeof(DateTime));
                dt.Columns.Add("Phiên Bản", typeof(int));
                dt.Columns.Add("Thiết Bị", typeof(string));
                dt.Columns.Add("Firebase ID", typeof(string));
            }
        }

        private void SetupComboBox()
        {
            System.Diagnostics.Debug.WriteLine("SetupComboBox được gọi tại: " + DateTime.Now);
            cbGiaoDichThu.Items.AddRange(new[] { "Tất cả", "Tuần này", "Tháng này" });
            cbGiaoDichThu.SelectedIndex = 0;
            cbGiaoDichThu.SelectedIndexChanged -= cbGiaoDichThu_SelectedIndexChanged;
            cbGiaoDichThu.SelectedIndexChanged += cbGiaoDichThu_SelectedIndexChanged;

            cbGiaoDichChi.Items.AddRange(new[] { "Tất cả", "Tuần này", "Tháng này" });
            cbGiaoDichChi.SelectedIndex = 0;
            cbGiaoDichChi.SelectedIndexChanged -= cbGiaoDichChi_SelectedIndexChanged;
            cbGiaoDichChi.SelectedIndexChanged += cbGiaoDichChi_SelectedIndexChanged;
        }

        private void cbGiaoDichThu_SelectedIndexChanged(object sender, EventArgs ev)
        {
            System.Diagnostics.Debug.WriteLine($"cbGiaoDichThu_SelectedIndexChanged called with SelectedItem: {cbGiaoDichThu.SelectedItem}");
            FilterAndUpdate(dtGiaoDichThu, dgvGiaoDich, cbGiaoDichThu, txtTongThu);
        }

        private void cbGiaoDichChi_SelectedIndexChanged(object sender, EventArgs ev)
        {
            System.Diagnostics.Debug.WriteLine($"cbGiaoDichChi_SelectedIndexChanged called with SelectedItem: {cbGiaoDichChi.SelectedItem}");
            FilterAndUpdate(dtGiaoDichChi, dgvGiaoDichChi, cbGiaoDichChi, txtTongChi);
        }

        private async Task LoadData()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Bắt đầu tải dữ liệu cho MaTaiKhoan: {_maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                var tempThu = dbHelper.GetGiaoDich(_maTaiKhoan, "Thu");
                var tempChi = dbHelper.GetGiaoDich(_maTaiKhoan, "Chi");

                if (!tempThu.Columns.Contains("Mã Giao Dịch") || !tempChi.Columns.Contains("Mã Giao Dịch"))
                {
                    string thuColumns = string.Join(", ", tempThu.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    string chiColumns = string.Join(", ", tempChi.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    MessageBox.Show($"Dữ liệu từ cơ sở dữ liệu không chứa cột 'Mã Giao Dịch'.\n" +
                                    $"Cột trong tempThu: {thuColumns}\n" +
                                    $"Cột trong tempChi: {chiColumns}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dtGiaoDichThu.Clear();
                if (tempThu.Rows.Count > 0)
                {
                    foreach (DataRow row in tempThu.Rows)
                    {
                        dtGiaoDichThu.Rows.Add(row.ItemArray);
                    }
                }

                dtGiaoDichChi.Clear();
                if (tempChi.Rows.Count > 0)
                {
                    foreach (DataRow row in tempChi.Rows)
                    {
                        dtGiaoDichChi.Rows.Add(row.ItemArray);
                    }
                }

                dgvGiaoDich.DataSource = dtGiaoDichThu;
                dgvGiaoDichChi.DataSource = dtGiaoDichChi;

                if (dgvGiaoDich.Columns.Contains("Mã Giao Dịch")) dgvGiaoDich.Columns["Mã Giao Dịch"].Visible = false;
                if (dgvGiaoDichChi.Columns.Contains("Mã Giao Dịch")) dgvGiaoDichChi.Columns["Mã Giao Dịch"].Visible = false;

                FilterAndUpdate(dtGiaoDichThu, dgvGiaoDich, cbGiaoDichThu, txtTongThu);
                FilterAndUpdate(dtGiaoDichChi, dgvGiaoDichChi, cbGiaoDichChi, txtTongChi);
                System.Diagnostics.Debug.WriteLine($"Tải dữ liệu hoàn tất: {dtGiaoDichThu.Rows.Count} giao dịch Thu, {dtGiaoDichChi.Rows.Count} giao dịch Chi");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải dữ liệu: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterAndUpdate(DataTable dt, DataGridView dgv, ComboBox cbFilter, TextBox txtTong)
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime start = DateTime.MinValue, end = DateTime.MaxValue;

                if (cbFilter.SelectedItem?.ToString() == "Tuần này")
                {
                    int d = (int)now.DayOfWeek;
                    start = now.Date.AddDays(-d);
                    end = start.AddDays(7);
                }
                else if (cbFilter.SelectedItem?.ToString() == "Tháng này")
                {
                    start = new DateTime(now.Year, now.Month, 1);
                    end = start.AddMonths(1);
                }

                var dv = new DataView(dt);
                if (cbFilter.SelectedItem?.ToString() != "Tất cả")
                    dv.RowFilter = $"[Ngày Giao Dịch]>=#{start:MM/dd/yyyy}# AND [Ngày Giao Dịch]<#{end:MM/dd/yyyy}#";

                dgv.DataSource = dv;
                decimal tong = dv.ToTable().AsEnumerable()
                    .Where(row => row["Số Tiền"] != DBNull.Value)
                    .Sum(row => Convert.ToDecimal(row["Số Tiền"]));
                txtTong.Text = tong.ToString("N0");

                if (dgv.Columns.Contains("Mã Giao Dịch")) dgv.Columns["Mã Giao Dịch"].Visible = false;
                System.Diagnostics.Debug.WriteLine($"Lọc dữ liệu cho {cbFilter.Name}: Total = {tong:N0}, Rows = {dv.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lọc dữ liệu: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void HandleGiaoDichAction(string loai, DataRow row, Action refreshAction)
        {
            try
            {
                syncTimer.Stop();
                System.Diagnostics.Debug.WriteLine($"Bắt đầu xử lý hành động {loai} tại: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

                frmThemGiaoDich frm = row == null
                    ? new frmThemGiaoDich(_maTaiKhoan, loai)
                    : new frmThemGiaoDich(_maTaiKhoan, loai,
                        row["Hạng Mục"]?.ToString() ?? "",
                        row["Số Tiền"] != DBNull.Value ? Convert.ToDecimal(row["Số Tiền"]) : 0,
                        row["Ghi Chú"]?.ToString() ?? "",
                        row["Ngày Giao Dịch"] != DBNull.Value ? Convert.ToDateTime(row["Ngày Giao Dịch"]) : DateTime.Now);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(frm.HangMuc) || frm.SoTien <= 0 || (loai != "Chi" && loai != "Thu"))
                    {
                        System.Diagnostics.Debug.WriteLine($"Dữ liệu không hợp lệ: HangMuc={frm.HangMuc}, SoTien={frm.SoTien}, Loai={loai}");
                        MessageBox.Show("Dữ liệu không hợp lệ: Vui lòng kiểm tra hạng mục, số tiền hoặc loại giao dịch.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (row == null)
                    {
                        int maGiaoDich = dbHelper.InsertGiaoDich(_maTaiKhoan, frm.HangMuc, frm.SoTien, loai, frm.GhiChu, frm.NgayGD);
                        System.Diagnostics.Debug.WriteLine($"Đã thêm giao dịch mới local: MaGiaoDich={maGiaoDich}, TenHangMuc={frm.HangMuc}, SoTien={frm.SoTien}");
                        MessageBox.Show("Dữ liệu đã được lưu local. Đang chờ đồng bộ hoặc nhấn 'Đồng bộ' để đồng bộ tức thì.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int maGiaoDich = Convert.ToInt32(row["Mã Giao Dịch"]);
                        dbHelper.UpdateGiaoDich(maGiaoDich, _maTaiKhoan, frm.HangMuc, frm.SoTien, loai, frm.GhiChu, frm.NgayGD);
                        System.Diagnostics.Debug.WriteLine($"Đã sửa giao dịch local: MaGiaoDich={maGiaoDich}, TenHangMuc={frm.HangMuc}, SoTien={frm.SoTien}");
                        MessageBox.Show("Dữ liệu đã được cập nhật local. Đang chờ đồng bộ hoặc nhấn 'Đồng bộ' để đồng bộ tức thì.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    refreshAction();
                    System.Diagnostics.Debug.WriteLine($"Giao dịch {loai} đã lưu, chờ đồng bộ lúc {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xử lý giao dịch {loai}: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi xử lý giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                syncTimer.Start();
                System.Diagnostics.Debug.WriteLine("Khôi phục syncTimer.");
            }
        }

        private void InitializeSyncTimer()
        {
            syncTimer = new System.Windows.Forms.Timer();
            syncTimer.Interval = 300000; // 5 phút

            syncTimer.Tick += async (s, ev) =>
            {
                System.Diagnostics.Debug.WriteLine($"[TIMER TICK] {DateTime.Now:HH:mm:ss}");
                await CheckAndSync();
            };

            syncTimer.Start();
            System.Diagnostics.Debug.WriteLine("Khởi tạo syncTimer");
        }

        private async Task CheckAndSync()
        {
            try
            {
                if (!dbHelper.IsInternetAvailable())
                {
                    System.Diagnostics.Debug.WriteLine("Không có kết nối Internet. Hủy đồng bộ tự động.");
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Bắt đầu đồng bộ tự động cho MaTaiKhoan: {_maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                bool success = await dbHelper.SyncGiaoDichAsync(_maTaiKhoan);
                await LoadData();
                if (success)
                {
                    System.Diagnostics.Debug.WriteLine($"Hoàn tất đồng bộ tự động tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Đồng bộ tự động không thành công tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đồng bộ tự động: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi đồng bộ tự động: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                syncTimer.Stop();
                System.Diagnostics.Debug.WriteLine($"Bắt đầu đồng bộ thủ công cho MaTaiKhoan: {_maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                if (!dbHelper.IsInternetAvailable())
                {
                    MessageBox.Show("Không có kết nối Internet. Vui lòng kiểm tra kết nối và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await dbHelper.SyncGiaoDichAsync(_maTaiKhoan);
                await LoadData();
                if (success)
                {
                    MessageBox.Show("Đồng bộ thủ công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Debug.WriteLine($"Hoàn tất đồng bộ thủ công tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
                else
                {
                    MessageBox.Show("Đồng bộ thủ công thất bại. Vui lòng kiểm tra log để biết thêm chi tiết.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine($"Đồng bộ thủ công không thành công tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đồng bộ thủ công: {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi đồng bộ thủ công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                syncTimer.Start();
                System.Diagnostics.Debug.WriteLine("Khôi phục syncTimer sau đồng bộ thủ công.");
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            HandleGiaoDichAction("Thu", null, async () => await LoadData());
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.CurrentRow != null)
            {
                DataRow row = ((DataRowView)dgvGiaoDich.CurrentRow.DataBoundItem).Row;
                HandleGiaoDichAction("Thu", row, async () => await LoadData());
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDich.CurrentRow != null)
            {
                DataRow row = ((DataRowView)dgvGiaoDich.CurrentRow.DataBoundItem).Row;
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        syncTimer.Stop();

                        int maGiaoDich = Convert.ToInt32(row["Mã Giao Dịch"]);

             
                        string firebaseId = row.Table.Columns.Contains("FirebaseId") && row["FirebaseId"] != DBNull.Value
                            ? row["FirebaseId"].ToString()
                            : null;

                        if (!string.IsNullOrEmpty(firebaseId))
                        {
                            try
                            {
                                await firebaseClient.Child("GiaoDich").Child(firebaseId).DeleteAsync();
                                System.Diagnostics.Debug.WriteLine($"Đã xóa giao dịch trên Firebase: FirebaseId={firebaseId}");
                            }
                            catch (Exception exFirebase)
                            {
                                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa giao dịch trên Firebase: {exFirebase.Message}");
                            }
                        }

                        dbHelper.DeleteGiaoDich(maGiaoDich);
                        System.Diagnostics.Debug.WriteLine($"Đã đánh dấu xóa giao dịch local: MaGiaoDich={maGiaoDich}");

                        await LoadData();

                        MessageBox.Show("Giao dịch đã được đánh dấu xóa local. Đang chờ đồng bộ hoặc nhấn 'Đồng bộ' để đồng bộ tức thì.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Debug.WriteLine($"Giao dịch đã xóa, chờ đồng bộ lúc {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa giao dịch: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        MessageBox.Show($"Lỗi khi xóa giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        syncTimer.Start();
                    }
                }
            }
        }

        private async void btnThemChi_Click(object sender, EventArgs e)
        {
            HandleGiaoDichAction("Chi", null, async () => await LoadData());
        }

        private async void btnSuaChi_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDichChi.CurrentRow != null)
            {
                DataRow row = ((DataRowView)dgvGiaoDichChi.CurrentRow.DataBoundItem).Row;
                HandleGiaoDichAction("Chi", row, async () => await LoadData());
            }
        }

        private async void btnXoaChi_Click(object sender, EventArgs e)
        {
            if (dgvGiaoDichChi.CurrentRow != null)
            {
                DataRow row = ((DataRowView)dgvGiaoDichChi.CurrentRow.DataBoundItem).Row;
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa giao dịch này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        syncTimer.Stop();

                        int maGiaoDich = Convert.ToInt32(row["Mã Giao Dịch"]);

  
                        string firebaseId = row.Table.Columns.Contains("FirebaseId") && row["FirebaseId"] != DBNull.Value
                            ? row["FirebaseId"].ToString()
                            : null;

                        if (!string.IsNullOrEmpty(firebaseId))
                        {
                            try
                            {
                                await firebaseClient.Child("GiaoDich").Child(firebaseId).DeleteAsync();
                                System.Diagnostics.Debug.WriteLine($"Đã xóa giao dịch trên Firebase: FirebaseId={firebaseId}");
                            }
                            catch (Exception exFirebase)
                            {
                                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa giao dịch trên Firebase: {exFirebase.Message}");
                            }
                        }

                        dbHelper.DeleteGiaoDich(maGiaoDich);
                        System.Diagnostics.Debug.WriteLine($"Đã đánh dấu xóa giao dịch local: MaGiaoDich={maGiaoDich}");

                        await LoadData();

                        MessageBox.Show("Giao dịch đã được đánh dấu xóa local. Đang chờ đồng bộ hoặc nhấn 'Đồng bộ' để đồng bộ tức thì.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Debug.WriteLine($"Giao dịch đã xóa, chờ đồng bộ lúc {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa giao dịch: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        MessageBox.Show($"Lỗi khi xóa giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        syncTimer.Start();
                    }
                }
            }
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}