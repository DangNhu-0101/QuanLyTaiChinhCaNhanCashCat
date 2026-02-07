using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyTaiChinh
{
    public class DatabaseHelper
    {
        private string chuoiketnoi = "Server = .\\SQLEXPRESS; Database = QuanLyChiTieu; Integrated Security = True";

   
        public List<string> GetHangMucList(int maTaiKhoan, string loaiHangMuc)
        {
            var result = new List<string>();
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "SELECT TenHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan";
                if (!string.IsNullOrEmpty(loaiHangMuc))
                    query += " AND LoaiHangMuc = @LoaiHangMuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    if (!string.IsNullOrEmpty(loaiHangMuc))
                        cmd.Parameters.AddWithValue("@LoaiHangMuc", loaiHangMuc);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader["TenHangMuc"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        public int? GetMaHangMuc(string tenHangMuc, int maTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "SELECT MaHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND TenHangMuc = @TenHangMuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.Parameters.AddWithValue("@TenHangMuc", tenHangMuc);
                    var result = cmd.ExecuteScalar();
                    return result != null ? (int?)Convert.ToInt32(result) : null;
                }
            }
        }

        public string GetTenHangMuc(int maHangMuc, int maTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "SELECT TenHangMuc FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND MaHangMuc = @MaHangMuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.Parameters.AddWithValue("@MaHangMuc", maHangMuc);
                    var result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }


        public bool KiemTraMaHangMuc(int maHangMuc, int maTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM HangMuc WHERE MaTaiKhoan = @MaTaiKhoan AND MaHangMuc = @MaHangMuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.Parameters.AddWithValue("@MaHangMuc", maHangMuc);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}