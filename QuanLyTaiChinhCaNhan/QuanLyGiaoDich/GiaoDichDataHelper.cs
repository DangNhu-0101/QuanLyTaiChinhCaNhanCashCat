using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace QuanLyTaiChinhCaNhan
{
    public class GiaoDichDataHelper
    {
        private readonly string connectionString = "Server=LAPTOP-O8Q0L0EF\\SQLEXPRESS;Database=QuanLyChiTieu;Integrated Security=True";
        private readonly string firebaseUrl = "https://quanlychitieu-73780-default-rtdb.asia-southeast1.firebasedatabase.app/";
        private readonly string firebaseSecret = "XyZKM2o4UyJZeFfLm9THRNjMGqSFCbLqLmYww7QG";
        public readonly string deviceId = Guid.NewGuid().ToString();
        private DateTime lastSyncTime = DateTime.MinValue;
        private readonly FirebaseClient firebaseClient;
        private readonly TimeSpan localTimeZoneOffset = TimeSpan.FromHours(7); 

        public GiaoDichDataHelper(int maTaiKhoan)
        {
            firebaseClient = new FirebaseClient(firebaseUrl,
                new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(firebaseSecret) });
            LoadLastSyncTime(maTaiKhoan);
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private void LoadLastSyncTime(int maTaiKhoan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAX(ThoiGianDongBo) FROM DongBoDuLieu WHERE MaTaiKhoan = @MaTaiKhoan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        var result = cmd.ExecuteScalar();
                        lastSyncTime = result != DBNull.Value ? (DateTime)result : DateTime.MinValue;
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Loaded lastSyncTime (+07:00): {lastSyncTime:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải thời gian đồng bộ cuối: {ex.Message}");
            }
        }

        public DataTable GetGiaoDich(int maTaiKhoan, string loaiGiaoDich)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT g.MaGiaoDich AS [Mã Giao Dịch], h.TenHangMuc AS [Danh Mục], g.SoTien AS [Số Tiền], 
                               g.LoaiGiaoDich AS [Loại Giao Dịch], g.GhiChu AS [Ghi Chú], 
                               g.NgayGiaoDich AS [Ngày Giao Dịch], g.SyncStatus AS [Trạng Thái Đồng Bộ],
                               g.LastModified AS [Thời Điểm Chỉnh Sửa], g.Version AS [Phiên Bản], g.DeviceId AS [Thiết Bị], g.FirebaseId AS [Firebase ID]
                        FROM GiaoDich g
                        INNER JOIN HangMuc h ON g.MaHangMuc = h.MaHangMuc
                        WHERE g.MaTaiKhoan = @MaTaiKhoan AND g.LoaiGiaoDich = @LoaiGiaoDich AND g.SyncStatus != 'Deleted'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@LoaiGiaoDich", loaiGiaoDich);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Loaded {dt.Rows.Count} giao dịch for MaTaiKhoan: {maTaiKhoan}, LoaiGiaoDich: {loaiGiaoDich}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải giao dịch {loaiGiaoDich}: {ex.Message}");
                throw new Exception($"Lỗi khi tải giao dịch: {ex.Message}");
            }
            return dt;
        }

        public List<string> GetHangMuc(int maTaiKhoan, string loaiHangMuc)
        {
            List<string> hangMucList = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TenHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND LoaiHangMuc = @LoaiHangMuc";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@LoaiHangMuc", loaiHangMuc);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hangMucList.Add(reader["TenHangMuc"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải danh sách hạng mục: {ex.Message}");
                throw new Exception($" WeezyLỗi khi tải danh sách hạng mục: {ex.Message}");
            }
            return hangMucList;
        }

        public bool KiemTraHangMucTonTai(int maTaiKhoan, string tenHangMuc, string loaiHangMuc)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc AND LoaiHangMuc = @LoaiHangMuc";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@TenHangMuc", tenHangMuc);
                        cmd.Parameters.AddWithValue("@LoaiHangMuc", loaiHangMuc);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi kiểm tra hạng mục {tenHangMuc}: {ex.Message}");
                return false;
            }
        }

        public void EnsureHangMucExists(int maTaiKhoan, string tenHangMuc, string loaiHangMuc)
        {
            try
            {
                if (string.IsNullOrEmpty(tenHangMuc) || string.IsNullOrEmpty(loaiHangMuc))
                {
                    System.Diagnostics.Debug.WriteLine($"TenHangMuc hoặc LoaiHangMuc rỗng: {tenHangMuc}, {loaiHangMuc}");
                    throw new ArgumentException("Tên hạng mục hoặc loại hạng mục không được rỗng.");
                }
                if (!KiemTraHangMucTonTai(maTaiKhoan, tenHangMuc, loaiHangMuc))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO HangMuc (MaTaiKhoan, TenHangMuc, LoaiHangMuc) VALUES (@MaTaiKhoan, @TenHangMuc, @LoaiHangMuc)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                            cmd.Parameters.AddWithValue("@TenHangMuc", tenHangMuc);
                            cmd.Parameters.AddWithValue("@LoaiHangMuc", loaiHangMuc);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Đã tạo hạng mục mới: {tenHangMuc} (LoaiHangMuc: {loaiHangMuc})");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tạo hạng mục {tenHangMuc}: {ex.Message}");
                throw new Exception($"Lỗi khi tạo hạng mục: {ex.Message}");
            }
        }

        public int InsertGiaoDich(int maTaiKhoan, string tenHangMuc, decimal soTien, string loaiGiaoDich, string ghiChu, DateTime ngayGiaoDich)
        {
            try
            {
                if (string.IsNullOrEmpty(tenHangMuc) || string.IsNullOrEmpty(loaiGiaoDich) || soTien <= 0)
                    throw new ArgumentException("Dữ liệu không hợp lệ: Tên hạng mục, loại giao dịch hoặc số tiền không hợp lệ.");
                EnsureHangMucExists(maTaiKhoan, tenHangMuc, loaiGiaoDich);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO GiaoDich (MaTaiKhoan, MaHangMuc, SoTien, LoaiGiaoDich, GhiChu, NgayGiaoDich, SyncStatus, LastModified, Version, DeviceId, FirebaseId)
                        OUTPUT INSERTED.MaGiaoDich
                        VALUES (@MaTaiKhoan, 
                                (SELECT TOP 1 MaHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc AND LoaiHangMuc = @LoaiGiaoDich), 
                                @SoTien, @LoaiGiaoDich, @GhiChu, @NgayGiaoDich, 'Pending', @LastModified, 1, @DeviceId, NULL)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@TenHangMuc", tenHangMuc);
                        cmd.Parameters.AddWithValue("@LoaiGiaoDich", loaiGiaoDich);
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayGiaoDich", ngayGiaoDich);
                        cmd.Parameters.AddWithValue("@LastModified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                        var result = cmd.ExecuteScalar();
                        if (result == null)
                            throw new Exception("Không thể thêm giao dịch: MaHangMuc không tìm thấy.");
                        System.Diagnostics.Debug.WriteLine($"Đã thêm giao dịch MaGiaoDich: {(int)result}, LastModified: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                        return (int)result;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi thêm giao dịch: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void UpdateGiaoDich(int maGiaoDich, int maTaiKhoan, string tenHangMuc, decimal soTien, string loaiGiaoDich, string ghiChu, DateTime ngayGiaoDich)
        {
            try
            {
                if (string.IsNullOrEmpty(tenHangMuc) || string.IsNullOrEmpty(loaiGiaoDich) || soTien <= 0)
                    throw new ArgumentException("Dữ liệu không hợp lệ: Tên hạng mục, loại giao dịch hoặc số tiền không hợp lệ.");
                EnsureHangMucExists(maTaiKhoan, tenHangMuc, loaiGiaoDich);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT LastModified FROM GiaoDich WHERE MaGiaoDich = @MaGiaoDich AND MaTaiKhoan = @MaTaiKhoan";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                        checkCmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        var oldLastModified = checkCmd.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Trước khi cập nhật - MaGiaoDich: {maGiaoDich}, LastModified cũ: {oldLastModified}");
                    }

                    string query = @"
                        UPDATE GiaoDich 
                        SET MaHangMuc = (SELECT TOP 1 MaHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc AND LoaiHangMuc = @LoaiGiaoDich),
                            SoTien = @SoTien, 
                            LoaiGiaoDich = @LoaiGiaoDich, 
                            GhiChu = @GhiChu, 
                            NgayGiaoDich = @NgayGiaoDich, 
                            SyncStatus = 'Pending', 
                            LastModified = @LastModified, 
                            Version = Version + 1, 
                            DeviceId = @DeviceId
                        WHERE MaGiaoDich = @MaGiaoDich AND MaTaiKhoan = @MaTaiKhoan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@TenHangMuc", tenHangMuc);
                        cmd.Parameters.AddWithValue("@LoaiGiaoDich", loaiGiaoDich);
                        cmd.Parameters.AddWithValue("@SoTien", soTien);
                        cmd.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayGiaoDich", ngayGiaoDich);
                        cmd.Parameters.AddWithValue("@LastModified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception($"Không tìm thấy giao dịch với MaGiaoDich: {maGiaoDich} và MaTaiKhoan: {maTaiKhoan}");
                        System.Diagnostics.Debug.WriteLine($"Đã cập nhật giao dịch MaGiaoDich: {maGiaoDich}, LastModified mới: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi sửa giao dịch MaGiaoDich: {maGiaoDich}, Lỗi: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public void DeleteGiaoDich(int maGiaoDich)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE GiaoDich SET SyncStatus = 'Deleted', LastModified = @LastModified, Version = Version + 1 WHERE MaGiaoDich = @MaGiaoDich";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                        cmd.Parameters.AddWithValue("@LastModified", DateTime.Now);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception($"Không tìm thấy giao dịch với MaGiaoDich: {maGiaoDich}");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Đã đánh dấu xóa giao dịch MaGiaoDich: {maGiaoDich}, LastModified: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi đánh dấu xóa giao dịch MaGiaoDich: {maGiaoDich}, Lỗi: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> SyncGiaoDichAsync(int maTaiKhoan)
        {
            if (!IsInternetAvailable())
            {
                string loi = "Không có kết nối Internet.";
                System.Diagnostics.Debug.WriteLine(loi);
                LogSync(maTaiKhoan, "ThatBai", loi);
                return false;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"Bắt đầu đồng bộ tất cả cho MaTaiKhoan: {maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                bool sqlToFirebaseSuccess = await SyncFromSqlToFirebaseAsync(maTaiKhoan);
                bool firebaseToSqlSuccess = await SyncFromFirebaseToSqlAsync(maTaiKhoan);

                if (sqlToFirebaseSuccess && firebaseToSqlSuccess)
                {
                    lastSyncTime = DateTime.Now;
                    SaveLastSyncTime();
                    System.Diagnostics.Debug.WriteLine($"Hoàn tất đồng bộ tất cả, lastSyncTime cập nhật (+07:00): {lastSyncTime:yyyy-MM-dd HH:mm:ss}");
                    LogSync(maTaiKhoan, "ThanhCong");
                    return true;
                }
                string loi = "Đồng bộ không hoàn tất: Một hoặc cả hai quá trình đồng bộ thất bại.";
                System.Diagnostics.Debug.WriteLine(loi);
                LogSync(maTaiKhoan, "ThatBai", loi);
                return false;
            }
            catch (Exception ex)
            {
                string loi = $"Lỗi đồng bộ tất cả: {ex.Message}\nStackTrace: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(loi);
                LogSync(maTaiKhoan, "ThatBai", loi);
                return false;
            }
        }

        private async Task<bool> SyncFromSqlToFirebaseAsync(int maTaiKhoan)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Bắt đầu đồng bộ từ SQL Server lên Firebase cho MaTaiKhoan: {maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string uploadQuery = @"
                        SELECT g.MaGiaoDich, g.MaTaiKhoan, h.TenHangMuc, g.SoTien, g.LoaiGiaoDich, g.GhiChu, g.NgayGiaoDich,
                               g.SyncStatus, g.LastModified, g.Version, g.DeviceId, g.FirebaseId
                        FROM GiaoDich g
                        INNER JOIN HangMuc h ON g.MaHangMuc = h.MaHangMuc
                        WHERE g.SyncStatus IN ('Pending', 'Deleted') AND g.MaTaiKhoan = @MaTaiKhoan AND g.LastModified > @LastSyncTime";
                    using (SqlCommand cmd = new SqlCommand(uploadQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@LastSyncTime", lastSyncTime);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var localGiaoDich = new GiaoDich
                                {
                                    MaGiaoDich = reader.GetInt32(0),
                                    MaTaiKhoan = reader.GetInt32(1),
                                    TenHangMuc = reader.GetString(2),
                                    SoTien = reader.GetDecimal(3),
                                    LoaiGiaoDich = reader.GetString(4),
                                    GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    NgayGiaoDich = reader.GetDateTime(6),
                                    SyncStatus = reader.GetString(7),
                                    LastModified = reader.GetDateTime(8),
                                    Version = reader.GetInt32(9),
                                    DeviceId = reader.GetString(10),
                                    FirebaseId = reader.IsDBNull(11) ? null : reader.GetString(11)
                                };

                                var firebaseKey = localGiaoDich.FirebaseId ?? Guid.NewGuid().ToString();

                                if (localGiaoDich.SyncStatus == "Deleted")
                                {
                                    if (localGiaoDich.FirebaseId != null)
                                    {
                                        int retryCount = 0;
                                        const int maxRetries = 3;
                                        while (retryCount < maxRetries)
                                        {
                                            try
                                            {
                                                await firebaseClient.Child($"GiaoDich/{firebaseKey}").DeleteAsync();
                                                System.Diagnostics.Debug.WriteLine($"Đã xóa giao dịch trên Firebase với key: {firebaseKey}, MaGiaoDich: {localGiaoDich.MaGiaoDich}");
                                                break;
                                            }
                                            catch (Exception ex)
                                            {
                                                retryCount++;
                                                string loi = $"Lỗi khi xóa trên Firebase (MaGiaoDich: {localGiaoDich.MaGiaoDich}, lần {retryCount}): {ex.Message}";
                                                System.Diagnostics.Debug.WriteLine(loi);
                                                if (retryCount == maxRetries)
                                                {
                                                    UpdateSyncStatus(localGiaoDich.MaGiaoDich, "Failed");
                                                    LogSync(maTaiKhoan, "ThatBai", loi);
                                                    System.Diagnostics.Debug.WriteLine($"Đồng bộ xóa thất bại sau {maxRetries} lần thử: MaGiaoDich={localGiaoDich.MaGiaoDich}");
                                                    continue;
                                                }
                                                await Task.Delay(1000);
                                            }
                                        }
                                    }
                                    using (SqlConnection deleteConn = new SqlConnection(connectionString))
                                    {
                                        deleteConn.Open();
                                        string deleteQuery = "DELETE FROM GiaoDich WHERE MaGiaoDich = @MaGiaoDich";
                                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, deleteConn))
                                        {
                                            deleteCmd.Parameters.AddWithValue("@MaGiaoDich", localGiaoDich.MaGiaoDich);
                                            deleteCmd.ExecuteNonQuery();
                                        }
                                    }
                                    System.Diagnostics.Debug.WriteLine($"Đã xóa giao dịch cục bộ với MaGiaoDich: {localGiaoDich.MaGiaoDich}");
                                    LogSync(maTaiKhoan, "ThanhCong");
                                    continue;
                                }

                                var firebaseData = new Dictionary<string, object>
                                {
                                    { "MaTaiKhoan", localGiaoDich.MaTaiKhoan },
                                    { "TenHangMuc", localGiaoDich.TenHangMuc },
                                    { "SoTien", localGiaoDich.SoTien },
                                    { "LoaiGiaoDich", localGiaoDich.LoaiGiaoDich },
                                    { "GhiChu", localGiaoDich.GhiChu ?? "" },
                                    { "NgayGiaoDich", localGiaoDich.NgayGiaoDich.ToString("yyyy-MM-ddTHH:mm:ss+07:00") },
                                    { "LastModified", localGiaoDich.LastModified.ToString("yyyy-MM-ddTHH:mm:ss+07:00") },
                                    { "Version", localGiaoDich.Version },
                                    { "DeviceId", localGiaoDich.DeviceId }
                                };

                                int retryCountPush = 0;
                                const int maxRetriesPush = 3;
                                while (retryCountPush < maxRetriesPush)
                                {
                                    try
                                    {
                                        var firebaseItem = await firebaseClient.Child($"GiaoDich/{firebaseKey}").OnceSingleAsync<Dictionary<string, object>>();
                                        if (firebaseItem != null && firebaseItem.ContainsKey("LastModified"))
                                        {
                                            DateTime firebaseLastModified = DateTimeOffset.Parse(firebaseItem["LastModified"].ToString()).DateTime;
                                            if (firebaseLastModified >= localGiaoDich.LastModified)
                                            {
                                                System.Diagnostics.Debug.WriteLine($"Bỏ qua đẩy lên Firebase (MaGiaoDich: {localGiaoDich.MaGiaoDich}, FirebaseId: {firebaseKey}) vì Firebase LastModified ({firebaseLastModified:yyyy-MM-dd HH:mm:ss}) mới hơn hoặc bằng local ({localGiaoDich.LastModified:yyyy-MM-dd HH:mm:ss})");
                                                UpdateSyncStatus(localGiaoDich.MaGiaoDich, "Completed");
                                                continue;
                                            }
                                        }

                                        await firebaseClient.Child($"GiaoDich/{firebaseKey}").PutAsync(firebaseData);
                                        System.Diagnostics.Debug.WriteLine($"Đã đồng bộ lên Firebase với key: {firebaseKey}, MaGiaoDich: {localGiaoDich.MaGiaoDich}, GhiChu: {localGiaoDich.GhiChu}");
                                        UpdateFirebaseId(localGiaoDich.MaGiaoDich, firebaseKey);
                                        UpdateSyncStatus(localGiaoDich.MaGiaoDich, "Completed");
                                        LogSync(maTaiKhoan, "ThanhCong");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        retryCountPush++;
                                        string loi = $"Lỗi khi đẩy lên Firebase (MaGiaoDich: {localGiaoDich.MaGiaoDich}, lần {retryCountPush}): {ex.Message}";
                                        System.Diagnostics.Debug.WriteLine(loi);
                                        if (retryCountPush == maxRetriesPush)
                                        {
                                            UpdateSyncStatus(localGiaoDich.MaGiaoDich, "Failed");
                                            LogSync(maTaiKhoan, "ThatBai", loi);
                                            System.Diagnostics.Debug.WriteLine($"Đồng bộ thất bại sau {maxRetriesPush} lần thử: MaGiaoDich={localGiaoDich.MaGiaoDich}");
                                        }
                                        await Task.Delay(1000);
                                    }
                                }
                            }
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("Hoàn tất đồng bộ từ SQL Server lên Firebase.");
                LogSync(maTaiKhoan, "ThanhCong");
                return true;
            }
            catch (Exception ex)
            {
                string loi = $"Lỗi đồng bộ từ SQL Server lên Firebase: {ex.Message}\nStackTrace: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(loi);
                LogSync(maTaiKhoan, "ThatBai", loi);
                return false;
            }
        }

        private async Task<bool> SyncFromFirebaseToSqlAsync(int maTaiKhoan)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Bắt đầu đồng bộ từ Firebase xuống SQL Server cho MaTaiKhoan: {maTaiKhoan} tại {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var firebaseItems = await firebaseClient.Child("GiaoDich").OnceAsync<Dictionary<string, object>>();
                    var firebaseIds = new HashSet<string>();

                    foreach (var item in firebaseItems)
                    {
                        var data = item.Object;
                        var firebaseId = item.Key;
                        System.Diagnostics.Debug.WriteLine($"Đã lấy giao dịch FirebaseId: {firebaseId}, Data: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}");

                        if (data == null || !data.ContainsKey("MaTaiKhoan"))
                        {
                            string loi = $"Dữ liệu Firebase không hợp lệ hoặc thiếu MaTaiKhoan (FirebaseId: {firebaseId})";
                            System.Diagnostics.Debug.WriteLine(loi);
                            LogSync(maTaiKhoan, "ThatBai", loi);
                            continue;
                        }

                        firebaseIds.Add(firebaseId);

                        if (Convert.ToInt32(data["MaTaiKhoan"]) != maTaiKhoan)
                        {
                            System.Diagnostics.Debug.WriteLine($"Bỏ qua giao dịch FirebaseId: {firebaseId} vì MaTaiKhoan ({data["MaTaiKhoan"]}) không khớp với {maTaiKhoan}");
                            continue;
                        }

                        DateTime firebaseLastModified;
                        try
                        {
                            firebaseLastModified = DateTimeOffset.Parse(data["LastModified"].ToString()).DateTime;
                            System.Diagnostics.Debug.WriteLine($"FirebaseId: {firebaseId}, Firebase LastModified: {firebaseLastModified:yyyy-MM-dd HH:mm:ss}");
                        }
                        catch (Exception ex)
                        {
                            string loi = $"Lỗi khi parse LastModified từ Firebase (FirebaseId: {firebaseId}): {ex.Message}";
                            System.Diagnostics.Debug.WriteLine(loi);
                            LogSync(maTaiKhoan, "ThatBai", loi);
                            continue;
                        }

                        if (firebaseLastModified <= lastSyncTime)
                        {
                            System.Diagnostics.Debug.WriteLine($"Bỏ qua giao dịch FirebaseId: {firebaseId} vì LastModified ({firebaseLastModified:yyyy-MM-dd HH:mm:ss}) không mới hơn lastSyncTime ({lastSyncTime:yyyy-MM-dd HH:mm:ss})");
                            continue;
                        }

                        var localGiaoDich = GetLocalGiaoDichByFirebaseId(firebaseId, conn);
                        System.Diagnostics.Debug.WriteLine($"FirebaseId: {firebaseId}, Local LastModified: {(localGiaoDich != null ? localGiaoDich.LastModified.ToString("yyyy-MM-dd HH:mm:ss") : "null")}, Local SyncStatus: {(localGiaoDich != null ? localGiaoDich.SyncStatus : "null")}");
                        if (localGiaoDich == null || (firebaseLastModified > localGiaoDich.LastModified && localGiaoDich.SyncStatus != "Pending"))
                        {
                            var giaoDich = new GiaoDich
                            {
                                MaTaiKhoan = Convert.ToInt32(data["MaTaiKhoan"]),
                                TenHangMuc = data.ContainsKey("TenHangMuc") ? data["TenHangMuc"].ToString() : "Khác",
                                SoTien = data.ContainsKey("SoTien") ? Convert.ToDecimal(data["SoTien"]) : 0,
                                LoaiGiaoDich = data.ContainsKey("LoaiGiaoDich") ? data["LoaiGiaoDich"].ToString() : "Chi",
                                GhiChu = data.ContainsKey("GhiChu") ? data["GhiChu"].ToString() : "",
                                NgayGiaoDich = data.ContainsKey("NgayGiaoDich") ? DateTimeOffset.Parse(data["NgayGiaoDich"].ToString()).DateTime : DateTime.Now,
                                LastModified = firebaseLastModified,
                                Version = data.ContainsKey("Version") ? Convert.ToInt32(data["Version"]) : 0,
                                DeviceId = data.ContainsKey("DeviceId") ? data["DeviceId"].ToString() : "",
                                FirebaseId = firebaseId
                            };

                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Bắt đầu ghi giao dịch từ Firebase vào SQL Server, FirebaseId: {firebaseId}, GhiChu: {giaoDich.GhiChu}");
                                UpdateOrInsertGiaoDichFromFirebase(conn, giaoDich);
                                System.Diagnostics.Debug.WriteLine($"Đã đồng bộ từ Firebase xuống SQL Server với key: {firebaseId}, GhiChu: {giaoDich.GhiChu}");
                                LogSync(maTaiKhoan, "ThanhCong");
                                lastSyncTime = firebaseLastModified > lastSyncTime ? firebaseLastModified : lastSyncTime;
                            }
                            catch (Exception ex)
                            {
                                string loi = $"Lỗi khi đồng bộ từ Firebase (FirebaseId: {firebaseId}): {ex.Message}";
                                System.Diagnostics.Debug.WriteLine(loi);
                                LogSync(maTaiKhoan, "ThatBai", loi);
                                continue;
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Bỏ qua đồng bộ từ Firebase (FirebaseId: {firebaseId}) vì local LastModified ({localGiaoDich?.LastModified:yyyy-MM-dd HH:mm:ss}) mới hơn hoặc bằng Firebase ({firebaseLastModified:yyyy-MM-dd HH:mm:ss}) hoặc SyncStatus là Pending");
                        }
                    }

                    string localGiaoDichQuery = "SELECT MaGiaoDich, FirebaseId, SyncStatus, LastModified FROM GiaoDich WHERE MaTaiKhoan = @MaTaiKhoan AND FirebaseId IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(localGiaoDichQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int maGiaoDich = reader.GetInt32(0);
                                string firebaseId = reader.GetString(1);
                                string syncStatus = reader.GetString(2);
                                DateTime localLastModified = reader.GetDateTime(3);
                                if (!firebaseIds.Contains(firebaseId) && syncStatus != "Pending")
                                {
                                    var firebaseItem = await firebaseClient.Child($"GiaoDich/{firebaseId}").OnceSingleAsync<Dictionary<string, object>>();
                                    if (firebaseItem != null && firebaseItem.ContainsKey("LastModified"))
                                    {
                                        DateTime firebaseLastModified = DateTimeOffset.Parse(firebaseItem["LastModified"].ToString()).DateTime;
                                        if (firebaseLastModified >= localLastModified)
                                        {
                                            System.Diagnostics.Debug.WriteLine($"Bỏ qua xóa cục bộ (MaGiaoDich: {maGiaoDich}, FirebaseId: {firebaseId}) vì Firebase LastModified ({firebaseLastModified:yyyy-MM-dd HH:mm:ss}) mới hơn hoặc bằng local ({localLastModified:yyyy-MM-dd HH:mm:ss})");
                                            continue;
                                        }
                                    }

                                    using (SqlConnection deleteConn = new SqlConnection(connectionString))
                                    {
                                        deleteConn.Open();
                                        string deleteQuery = "DELETE FROM GiaoDich WHERE MaGiaoDich = @MaGiaoDich";
                                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, deleteConn))
                                        {
                                            deleteCmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                                            deleteCmd.ExecuteNonQuery();
                                        }
                                    }
                                    System.Diagnostics.Debug.WriteLine($"Đã xóa giao dịch cục bộ với MaGiaoDich: {maGiaoDich} vì không tồn tại trên Firebase");
                                    LogSync(maTaiKhoan, "ThanhCong");
                                }
                            }
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("Hoàn tất đồng bộ từ Firebase xuống SQL Server.");
                LogSync(maTaiKhoan, "ThanhCong");
                return true;
            }
            catch (Exception ex)
            {
                string loi = $"Lỗi đồng bộ từ Firebase xuống SQL Server: {ex.Message}\nStackTrace: {ex.StackTrace}";
                System.Diagnostics.Debug.WriteLine(loi);
                LogSync(maTaiKhoan, "ThatBai", loi);
                return false;
            }
        }

        private GiaoDich GetLocalGiaoDichByFirebaseId(string firebaseId, SqlConnection conn)
        {
            try
            {
                string query = "SELECT MaGiaoDich, MaTaiKhoan, SyncStatus, LastModified, Version, DeviceId, FirebaseId FROM GiaoDich WHERE FirebaseId = @FirebaseId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GiaoDich
                            {
                                MaGiaoDich = reader.GetInt32(0),
                                MaTaiKhoan = reader.GetInt32(1),
                                SyncStatus = reader.GetString(2),
                                LastModified = reader.GetDateTime(3),
                                Version = reader.GetInt32(4),
                                DeviceId = reader.GetString(5),
                                FirebaseId = reader.IsDBNull(6) ? null : reader.GetString(6)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy giao dịch local theo FirebaseId: {ex.Message}");
            }
            return null;
        }

        private void UpdateOrInsertGiaoDichFromFirebase(SqlConnection conn, GiaoDich giaoDich)
        {
            try
            {
                EnsureHangMucExists(giaoDich.MaTaiKhoan, giaoDich.TenHangMuc, giaoDich.LoaiGiaoDich);
                string query = @"
                    IF EXISTS (SELECT 1 FROM GiaoDich WHERE FirebaseId = @FirebaseId)
                        UPDATE GiaoDich 
                        SET MaHangMuc = (SELECT TOP 1 MaHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc AND LoaiHangMuc = @LoaiGiaoDich),
                            SoTien = @SoTien, 
                            LoaiGiaoDich = @LoaiGiaoDich, 
                            GhiChu = @GhiChu, 
                            NgayGiaoDich = @NgayGiaoDich,
                            SyncStatus = 'Completed', 
                            LastModified = @LastModified, 
                            Version = @Version, 
                            DeviceId = @DeviceId
                        WHERE FirebaseId = @FirebaseId
                    ELSE
                        INSERT INTO GiaoDich (MaTaiKhoan, MaHangMuc, SoTien, LoaiGiaoDich, GhiChu, NgayGiaoDich, SyncStatus, LastModified, Version, DeviceId, FirebaseId)
                        VALUES (@MaTaiKhoan, 
                                (SELECT TOP 1 MaHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc AND LoaiHangMuc = @LoaiGiaoDich),
                                @SoTien, @LoaiGiaoDich, @GhiChu, @NgayGiaoDich, 'Completed', @LastModified, @Version, @DeviceId, @FirebaseId)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", giaoDich.MaTaiKhoan);
                    cmd.Parameters.AddWithValue("@TenHangMuc", giaoDich.TenHangMuc);
                    cmd.Parameters.AddWithValue("@LoaiGiaoDich", giaoDich.LoaiGiaoDich);
                    cmd.Parameters.AddWithValue("@SoTien", giaoDich.SoTien);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)giaoDich.GhiChu ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayGiaoDich", giaoDich.NgayGiaoDich);
                    cmd.Parameters.AddWithValue("@LastModified", giaoDich.LastModified);
                    cmd.Parameters.AddWithValue("@Version", giaoDich.Version);
                    cmd.Parameters.AddWithValue("@DeviceId", giaoDich.DeviceId);
                    cmd.Parameters.AddWithValue("@FirebaseId", giaoDich.FirebaseId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($"Ghi giao dịch vào SQL Server, FirebaseId: {giaoDich.FirebaseId}, RowsAffected: {rowsAffected}");
                    if (rowsAffected == 0)
                        throw new Exception($"Không thể cập nhật/thêm giao dịch từ Firebase: MaHangMuc không tìm thấy cho TenHangMuc: {giaoDich.TenHangMuc}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi cập nhật/đăng ký từ Firebase: {ex.Message}");
                throw;
            }
        }

        private void UpdateFirebaseId(int maGiaoDich, string firebaseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE GiaoDich SET FirebaseId = @FirebaseId WHERE MaGiaoDich = @MaGiaoDich";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirebaseId", firebaseId);
                        cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi cập nhật FirebaseId cho MaGiaoDich: {maGiaoDich}, Lỗi: {ex.Message}");
                throw;
            }
        }

        public void UpdateSyncStatus(int maGiaoDich, string syncStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE GiaoDich SET SyncStatus = @SyncStatus WHERE MaGiaoDich = @MaGiaoDich";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SyncStatus", syncStatus);
                        cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi cập nhật SyncStatus cho MaGiaoDich: {maGiaoDich}, Lỗi: {ex.Message}");
                throw;
            }
        }

        private void SaveLastSyncTime()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO DongBoDuLieu (MaTaiKhoan, ThoiGianDongBo, LoaiDongBo, KetQua)
                        VALUES (@MaTaiKhoan, @ThoiGianDongBo, 'TuDong', 'ThanhCong')";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", 1);
                        cmd.Parameters.AddWithValue("@ThoiGianDongBo", lastSyncTime);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu thời gian đồng bộ: {ex.Message}");
            }
        }

        private void LogSync(int maTaiKhoan, string ketQua, string chiTietLoi = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string logQuery = @"
                        INSERT INTO DongBoDuLieu (MaTaiKhoan, ThoiGianDongBo, LoaiDongBo, KetQua, ChiTietLoi)
                        VALUES (@MaTaiKhoan, @ThoiGianDongBo, 'TuDong', @KetQua, @ChiTietLoi)";
                    using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        logCmd.Parameters.AddWithValue("@ThoiGianDongBo", DateTime.Now);
                        logCmd.Parameters.AddWithValue("@KetQua", ketQua);
                        logCmd.Parameters.AddWithValue("@ChiTietLoi", (object)chiTietLoi ?? DBNull.Value);
                        logCmd.ExecuteNonQuery();
                    }
                    System.Diagnostics.Debug.WriteLine($"Ghi log đồng bộ: MaTaiKhoan={maTaiKhoan}, KetQua={ketQua}, ChiTietLoi={chiTietLoi ?? "Không có"}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi ghi log đồng bộ: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }

    public class GiaoDich
    {
        public int MaGiaoDich { get; set; }
        public int MaTaiKhoan { get; set; }
        public string TenHangMuc { get; set; }
        public decimal SoTien { get; set; }
        public string LoaiGiaoDich { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public string SyncStatus { get; set; }
        public DateTime LastModified { get; set; }
        public int Version { get; set; }
        public string DeviceId { get; set; }
        public string FirebaseId { get; set; }
    }
}