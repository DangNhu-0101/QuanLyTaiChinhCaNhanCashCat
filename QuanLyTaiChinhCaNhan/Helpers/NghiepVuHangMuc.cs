// NghiepVuHangMuc.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyTaiChinh
{
    
    public class NghiepVuHangMuc
    {
      
        private readonly int _maTaiKhoan;
        private string _loaiDangChon = "Thu";

   
        private static readonly string[] IconFiles = {
            "anuong.png", "giaothong.png", "luong.png", "muasam.png",
            "phucap.png", "thuong.png", "giaitri.png", "hoadon.png",
            "suckhoe.png", "quatang.png", "dienthoai.png", "sachvo.png","tietkiem.png","nha.png"
        };

        public NghiepVuHangMuc(int maTaiKhoan)
        {
            _maTaiKhoan = maTaiKhoan;
        }

        public string LoaiDangChon => _loaiDangChon;

        #region Logic Icon và Hỗ Trợ UI (Sử dụng Extract Method)

        
        private string GetIconPath(string iconFileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", iconFileName);
        }

      
        public void SetupIconPictureBox(PictureBox pb, string iconFileName)
        {
            string iconPath = GetIconPath(iconFileName);
            if (pb.Image != null) { pb.Image.Dispose(); pb.Image = null; }

            if (File.Exists(iconPath))
            {
                pb.Image = Image.FromFile(iconPath);
            }
            else
            {
                pb.Image = null;
                Console.WriteLine($"Không tìm thấy file biểu tượng: {iconPath}");
            }
        }

    
        public void LoadIconsToFlowLayout(FlowLayoutPanel flowLayoutPanelIcons, EventHandler pbClickHandler, string currentIcon)
        {
            flowLayoutPanelIcons.Controls.Clear();
            string[] iconFiles = IconFiles;

            foreach (string icon in iconFiles)
            {
                PictureBox pb = new PictureBox();
                SetupIconPictureBox(pb, icon);

                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Width = pb.Height = 40;
                pb.Margin = new Padding(5);
                pb.Tag = icon;
                pb.Cursor = Cursors.Hand;
                pb.Click += pbClickHandler;
                flowLayoutPanelIcons.Controls.Add(pb);

                if (icon == currentIcon) { pb.BorderStyle = BorderStyle.FixedSingle; }
            }
        }

     
        public void SelectIconAndPreview(string tenBieuTuong, PictureBox pictureBoxPreview, FlowLayoutPanel flowLayoutPanelIcons)
        {
            foreach (PictureBox item in flowLayoutPanelIcons.Controls) 
            { 
                item.BorderStyle = BorderStyle.None; 
            }

            foreach (PictureBox pb in flowLayoutPanelIcons.Controls)
            {
                if (pb.Tag != null && pb.Tag.ToString() == tenBieuTuong) 
                { 
                    pb.BorderStyle = BorderStyle.FixedSingle; 
                }
            }

            SetupIconPictureBox(pictureBoxPreview, tenBieuTuong);
        }

     
        public void DrawItemIcon(Graphics g, Rectangle bounds, string bieuTuong)
        {
            string iconPath = GetIconPath(bieuTuong);
            if (File.Exists(iconPath))
            {
                using (Image img = Image.FromFile(iconPath))
                {
                    int iconY = bounds.Top + (bounds.Height - 30) / 2;
                    g.DrawImage(img, bounds.Left + 5, iconY, 30, 30);
                }
            }
        }

        #endregion

        #region Logic Nghiệp Vụ (CRUD)

     
        public List<HangMuc> LoadHangMuc(out string error)
        {
            error = string.Empty;
            try
            {
                return HangMucService.LayHangMucTheoLoai(_maTaiKhoan, _loaiDangChon);
            }
            catch (Exception ex)
            {
                error = $"Lỗi tải danh mục: {ex.Message}";
                return new List<HangMuc>();
            }
        }

  
        public void ChuyenLoaiVaTaiDuLieu(string loaiMoi)
        {
            _loaiDangChon = loaiMoi;
        }

     
        public bool XuLyThemHangMuc(string tenHangMucMoi, string tenBieuTuong, out string message)
        {
            var hm = new HangMuc
            {
                MaTaiKhoan = _maTaiKhoan,
                TenHangMuc = tenHangMucMoi,
                LoaiHangMuc = _loaiDangChon,
                BieuTuong = tenBieuTuong
            };

            try
            {
                HangMucService.ThemHangMuc(hm);
                message = "Thêm danh mục thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Lỗi khi thêm danh mục: {ex.Message}";
                return false;
            }
        }

        public bool XuLyCapNhatHangMuc(HangMuc hangMucCu, string tenMoi, string bieuTuongMoi, out string message)
        {
            hangMucCu.TenHangMuc = tenMoi;
            hangMucCu.BieuTuong = bieuTuongMoi;

            try
            {
                HangMucService.CapNhatHangMuc(hangMucCu);
                message = "Cập nhật danh mục thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Lỗi khi cập nhật danh mục: {ex.Message}";
                return false;
            }
        }

  
        public bool XuLyXoaHangMuc(HangMuc hm, out string message)
        {
            try
            {
                HangMucService.XoaHangMuc(hm.MaHangMuc, _maTaiKhoan);
                message = "Xóa danh mục thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Lỗi khi xóa danh mục: {ex.Message}";
                return false;
            }
        }

        #endregion
    }
}