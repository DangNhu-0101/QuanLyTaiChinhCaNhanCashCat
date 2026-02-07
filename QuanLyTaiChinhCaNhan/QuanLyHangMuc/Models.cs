using System.ComponentModel;

namespace QuanLyTaiChinh
{

    public class HangMuc
    {

        public int MaHangMuc { get; set; }


        public int MaTaiKhoan { get; set; }

        [DisplayName("Tên hạng mục")]
        public string TenHangMuc { get; set; }

        [DisplayName("Loại hạng mục")]
        public string LoaiHangMuc { get; set; }


        [DisplayName("Biểu tượng")]
        public string BieuTuong { get; set; }


        public override string ToString()
        {
            return TenHangMuc;
        }
    }
}