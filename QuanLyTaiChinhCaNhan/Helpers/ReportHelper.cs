// NghiepVuPhanTichTraCuu.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyTaiChinhCaNhan
{
    // Ghi chú Refactoring: [Extract Class] Lớp nghiệp vụ tổng hợp cho Tra cứu/Báo cáo/Thống kê
    public class NghiepVuPhanTichTraCuu
    {
        // Ghi chú Refactoring: [Replace Magic Constant] 1 lần. Chuyển chuỗi kết nối ra khỏi Form
        private readonly string _connectionString = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";
        private readonly int _maTaiKhoan;

        public NghiepVuPhanTichTraCuu(int maTaiKhoan)
        {
            _maTaiKhoan = maTaiKhoan;
        }

        #region Logic Tạo Báo Cáo (frmTaoBaoCao) - (Move Method: 1 lần)

        // Ghi chú Refactoring: [Move Method] 1 lần. Di chuyển logic tính toán thời gian báo cáo
        public (string ThoiGian, string LoaiThoiGian) GetThoiGianBaoCao(
            bool rdoDayChecked, bool rdoWeekChecked, bool rdoMonthChecked, bool rdoYearChecked)
        {
            // Ghi chú Refactoring: [Extract Method] 1 lần. Tách logic tính toán
            DateTime now = DateTime.Now;
            try
            {
                if (rdoDayChecked) return (now.ToString("MM/dd/yyyy"), "Day");
                if (rdoWeekChecked) return ($"Tuần {System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)} - {now.Year}", "Week");
                if (rdoMonthChecked) return ($"{now.Month}/{now.Year}", "Month");
                if (rdoYearChecked) return (now.Year.ToString(), "Year");

                return (null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetThoiGianBaoCao: {ex.Message}");
                return (null, null);
            }
        }

        #endregion

        #region Logic Thống Kê (frmThongKe) - (Move Method: 3 lần, Extract Method: 3 lần)

        // Ghi chú Refactoring: [Extract Method/Move Method] 1 lần. Tách logic xử lý tổng thu/chi
        public (decimal TongThu, decimal TongChi) GetTongThuChi(int thang, int nam)
        {
            decimal tongThu = 0;
            decimal tongChi = 0;
            string queryTong = @"SELECT SUM(CASE WHEN g.LoaiGiaoDich = 'Thu' THEN g.SoTien ELSE 0 END) as TongThu,
                                        SUM(CASE WHEN g.LoaiGiaoDich = 'Chi' THEN g.SoTien ELSE 0 END) as TongChi
                                 FROM GiaoDich g
                                 WHERE MONTH(g.NgayGiaoDich) = @Thang AND YEAR(g.NgayGiaoDich) = @Nam AND g.MaTaiKhoan = @MaTaiKhoan";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(queryTong, conn))
            {
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {                         
                            if (reader["TongThu"] != DBNull.Value)
                            {
                                tongThu = Convert.ToDecimal(reader["TongThu"]);
                            }
                            else
                            {
                                tongThu = 0;
                            }

                            if (reader["TongChi"] != DBNull.Value)
                            {
                                tongChi = Convert.ToDecimal(reader["TongChi"]);
                            }
                            else
                            {
                                tongChi = 0; 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tính tổng thu chi.", ex);
                }
            }
            return (tongThu, tongChi);
        }

        // Ghi chú Refactoring: [Extract Method/Move Method] 1 lần. Tách logic lấy danh sách giao dịch cho DataGridView
        public DataTable GetGiaoDichThang(int thang, int nam)
        {
            string queryGiaoDich = @"SELECT g.NgayGiaoDich, g.LoaiGiaoDich, h.TenHangMuc, g.GhiChu, g.SoTien
                                     FROM GiaoDich g INNER JOIN HangMuc h ON g.MaHangMuc = h.MaHangMuc
                                     WHERE MONTH(g.NgayGiaoDich) = @Thang AND YEAR(g.NgayGiaoDich) = @Nam AND g.MaTaiKhoan = @MaTaiKhoan
                                     ORDER BY g.NgayGiaoDich DESC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(queryGiaoDich, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);

                try
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tải danh sách giao dịch.", ex);
                }
            }
        }

        // Ghi chú Refactoring: [Extract Method/Move Method] 1 lần. Tách logic tính toán và chuẩn bị dữ liệu biểu đồ
        public List<decimal> CalculateAccumulativeBalance(int thang, int nam, out int daysInMonth)
        {
            // Ghi chú: Logic SQL, tính tích lũy, và xử lý mảng đã được di chuyển
            daysInMonth = DateTime.DaysInMonth(nam, thang);
            decimal[] soDuTheoNgay = new decimal[daysInMonth + 1];
            decimal soDuTichLuy = 0;

            string querySoDu = @"SELECT DAY(g.NgayGiaoDich) as Ngay,
                                        SUM(CASE WHEN g.LoaiGiaoDich = 'Thu' THEN g.SoTien ELSE 0 END) -
                                        SUM(CASE WHEN g.LoaiGiaoDich = 'Chi' THEN g.SoTien ELSE 0 END) as SoDuNgay
                                 FROM GiaoDich g
                                 WHERE MONTH(g.NgayGiaoDich) = @Thang AND YEAR(g.NgayGiaoDich) = @Nam AND g.MaTaiKhoan = @MaTaiKhoan
                                 GROUP BY DAY(g.NgayGiaoDich) ORDER BY DAY(g.NgayGiaoDich)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(querySoDu, conn))
            {
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ngay = Convert.ToInt32(reader["Ngay"]);
                            decimal soDuNgay = Convert.ToDecimal(reader["SoDuNgay"]);
                            soDuTichLuy += soDuNgay;
                            soDuTheoNgay[ngay] = soDuTichLuy;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tính số dư tích lũy.", ex);
                }
            }

            // Điền số dư cho tất cả các ngày trong tháng (Logic tính toán tích lũy)
            for (int i = 1; i <= daysInMonth; i++)
            {
                if (i > 1 && soDuTheoNgay[i] == 0)
                {
                    soDuTheoNgay[i] = soDuTheoNgay[i - 1];
                }
            }
            return soDuTheoNgay.Skip(1).ToList();
        }

        #endregion

        #region Logic Tìm Kiếm (frmTimKiem) - (Move Method: 4 lần, Extract Method: 2 lần)

        public DataTable LoadOptions(string entityName)
        {
            string column;
            string table;

            if (entityName == "LoaiGiaoDich")
            {
                column = "LoaiGiaoDich";
                table = "GiaoDich";
            }
            else
            {
                column = "TenHangMuc";
                table = "HangMuc";
            }
            string query = $"SELECT DISTINCT {column} FROM {table} WHERE MaTaiKhoan = @MaTaiKhoan";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                try
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tải {entityName}.", ex);
                }
            }
        }



        public (string Query, List<SqlParameter> Parameters) BuildFilterQuery(
            string initialKeyword, string keyword,
            DateTime tuNgay, DateTime denNgay,
            string loaiGiaoDich, string tenHangMuc,
            decimal tuSoTien, decimal denSoTien,
            bool isInitialLoad)
        {
            string query = @"SELECT gd.NgayGiaoDich, gd.GhiChu, gd.LoaiGiaoDich, hm.TenHangMuc, gd.SoTien
                     FROM GiaoDich gd INNER JOIN HangMuc hm ON gd.MaHangMuc = hm.MaHangMuc
                     WHERE gd.MaTaiKhoan = @MaTaiKhoan";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@MaTaiKhoan", _maTaiKhoan)
    };

            string currentKeyword = isInitialLoad ? initialKeyword : keyword;
            string currentKeywordParameterName = isInitialLoad ? "@InitialKeyword" : "@FilterKeyword";

            if (string.IsNullOrEmpty(currentKeyword) == false)
            {
                query += $@" AND (
            (gd.GhiChu LIKE '%' + {currentKeywordParameterName} + '%')
            OR (gd.LoaiGiaoDich LIKE '%' + {currentKeywordParameterName} + '%')
            OR (hm.TenHangMuc LIKE '%' + {currentKeywordParameterName} + '%')
            OR (CAST(gd.SoTien AS NVARCHAR) LIKE '%' + {currentKeywordParameterName} + '%')
            OR (CAST(gd.NgayGiaoDich AS NVARCHAR) LIKE '%' + {currentKeywordParameterName} + '%')
        )";
                parameters.Add(new SqlParameter(currentKeywordParameterName, currentKeyword));
            }
            if (tuNgay != DateTime.MinValue)
            {
                query += " AND gd.NgayGiaoDich >= @TuNgay";
                parameters.Add(new SqlParameter("@TuNgay", tuNgay.Date));
            }
            if (denNgay != DateTime.MinValue)
            {
                query += " AND gd.NgayGiaoDich < @DenNgay";
                parameters.Add(new SqlParameter("@DenNgay", denNgay.Date.AddDays(1)));
            }
            if (string.IsNullOrEmpty(loaiGiaoDich) == false)
            {
                query += " AND gd.LoaiGiaoDich = @LoaiGiaoDich";
                parameters.Add(new SqlParameter("@LoaiGiaoDich", loaiGiaoDich.Trim()));
            }
            if (string.IsNullOrEmpty(tenHangMuc) == false)
            {
                query += " AND hm.TenHangMuc = @TenHangMuc";
                parameters.Add(new SqlParameter("@TenHangMuc", tenHangMuc.Trim()));
            }
            if (tuSoTien > 0)
            {
                query += " AND gd.SoTien >= @TuSoTien";
                parameters.Add(new SqlParameter("@TuSoTien", tuSoTien));
            }
            if (denSoTien < 1000000000)
            {
                query += " AND gd.SoTien <= @DenSoTien";
                parameters.Add(new SqlParameter("@DenSoTien", denSoTien));
            }
            query += " ORDER BY gd.NgayGiaoDich DESC";

            return (query, parameters);
        }

        public DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                try
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                   
                    throw new Exception("Lỗi khi thực thi truy vấn cơ sở dữ liệu.", ex);
                }
            }
        }
        public (string ThoiGian, string LoaiThoiGian) GetThoiGianAndType(
            bool rdoDayChecked, bool rdoWeekChecked, bool rdoMonthChecked, bool rdoYearChecked)
        {
            DateTime now = DateTime.Now;
            try
            {
                if (rdoDayChecked) 
                    return (now.ToString("MM/dd/yyyy"), "Day");
                if (rdoWeekChecked) return ($"Tuần {System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)} " +
                        $"- {now.Year}", "Week");
                if (rdoMonthChecked) return ($"{now.Month}/{now.Year}", "Month");
                if (rdoYearChecked) return (now.Year.ToString(), "Year");
                return (null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetThoiGianAndType: {ex.Message}");
                return (null, null);
            }
        }


        public (DateTime ParsedThoiGian, string LoaiThoiGian) ParseThoiGianToDateTime(string thoiGian)
        {
            if (string.IsNullOrWhiteSpace(thoiGian)) throw new ArgumentException("Chuỗi thời gian rỗng hoặc không hợp lệ.");
            try
            {
                if (thoiGian.Contains("/"))
                {
                    if (thoiGian.Split('/').Length == 2 && !thoiGian.Contains("Tuần")) return (new DateTime(
                        int.Parse(thoiGian.Split('/')[1].Trim()), int.Parse(thoiGian.Split('/')[0].Trim()), 1).Date, "Month");
                    else return (DateTime.ParseExact(thoiGian.Trim(), "MM/dd/yyyy", null).Date, "Day");
                }
                else if (thoiGian.Contains("Tuần"))
                {
                    var match = Regex.Match(thoiGian.Trim(), @"Tuần\s+(\d+)\s*-\s*(\d{4})");
                    if (!match.Success) 
                        throw new ArgumentException($"Định dạng tuần không hợp lệ: {thoiGian}");
                    int weekNum = int.Parse(match.Groups[1].Value);
                    int year = int.Parse(match.Groups[2].Value);
                    DateTime jan1 = new DateTime(year, 1, 1);
                    int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
                    if (daysOffset > 0) daysOffset -= 7;
                    DateTime firstMonday = jan1.AddDays(daysOffset);
                    return (firstMonday.AddDays((weekNum - 1) * 7).Date, "Week");
                }
                else
                {
                    int year = int.Parse(thoiGian.Trim());
                    return (new DateTime(year, 1, 1).Date, "Year");
                }
            }
            catch (Exception ex) { throw new Exception("Lỗi khi chuyển đổi chuỗi thời gian.", ex); }
        }

    
        public DataSet GetReportData_TongThuChi(DateTime thoiGian, string loaiThoiGian)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_TongThuChi", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGian.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@LoaiThoiGian", loaiThoiGian);
                try
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0) ds.Tables[0].TableName = "TongThuChi";
                    return ds;
                }
                catch (Exception ex) { throw new Exception("Lỗi khi truy vấn dữ liệu báo cáo Tổng Thu Chi.", ex); }
            }
        }


        public DataSet GetReportData_ChiTieuTheoHangMuc(DateTime thoiGian, string loaiThoiGian)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ChiTieuTheoHangMuc", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", _maTaiKhoan);
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGian.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@LoaiThoiGian", loaiThoiGian);

                try
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0) ds.Tables[0].TableName = "ChiTieuTheoHangMuc";
                    return ds;
                }
                catch (Exception ex) { throw new Exception("Lỗi khi truy vấn dữ liệu báo cáo Chi Tiêu Hạng Mục.", ex); }
            }
        }
        #endregion
    }
}