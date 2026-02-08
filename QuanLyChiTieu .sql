CREATE DATABASE QuanLyChiTieu;
GO

USE QuanLyChiTieu;
GO

-- Bảng Tài Khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(256) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    SoDienThoai NVARCHAR(20)
);
GO

-- Bảng Hạng Mục
CREATE TABLE HangMuc (
    MaHangMuc INT IDENTITY PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    TenHangMuc NVARCHAR(100) NOT NULL,
    LoaiHangMuc NVARCHAR(10) NOT NULL, -- Thu / Chi
    BieuTuong NVARCHAR(100) NOT NULL DEFAULT 'default.png',
    CONSTRAINT HM_MaTaiKhoan_FK FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT HM_LoaiHangMuc_CK CHECK (LoaiHangMuc IN ('Thu', 'Chi'))
);
GO

-- Bảng Giao Dịch
CREATE TABLE GiaoDich (
    MaGiaoDich INT IDENTITY PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    MaHangMuc INT NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    LoaiGiaoDich NVARCHAR(10) NOT NULL,
    GhiChu NVARCHAR(200),
    NgayGiaoDich DATETIME NOT NULL,
    TrangThaiDongBo NVARCHAR(20) DEFAULT 'Pending',
    LanSuaCuoi DATETIME DEFAULT GETDATE(),
    PhienBan INT DEFAULT 1,
    MaThietBi NVARCHAR(50),
    MaFirebase NVARCHAR(100), -- Thêm để lưu ID từ Firebase
    CONSTRAINT GD_MaTaiKhoan_FK FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT GD_MaHangMuc_FK FOREIGN KEY (MaHangMuc) REFERENCES HangMuc(MaHangMuc),
    CONSTRAINT GD_LoaiGiaoDich_CK CHECK (LoaiGiaoDich IN ('Thu', 'Chi'))
);
GO

-- Bảng Ngân Sách
CREATE TABLE NganSach (
    MaNganSach INT IDENTITY PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    SoTienNganSach DECIMAL(18,2) NOT NULL,
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NOT NULL,
    CONSTRAINT NS_MaTaiKhoan_FK FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Bảng Đồng Bộ Dữ Liệu
CREATE TABLE DongBoDuLieu (
    MaDongBo INT IDENTITY PRIMARY KEY,
    MaTaiKhoan INT NOT NULL,
    ThoiGianDongBo DATETIME NOT NULL CONSTRAINT DB_ThoiGianDongBo_DF DEFAULT GETDATE(),
    KetQua NVARCHAR(20) NOT NULL,
    ChiTiet NVARCHAR(MAX),
    CONSTRAINT DB_MaTaiKhoan_FK FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT DB_KetQua_CK CHECK (KetQua IN ('ThanhCong', 'ThatBai'))
);
GO

-- INSERT DỮ LIỆU

SET IDENTITY_INSERT TaiKhoan ON;
INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, HoTen, Email, SoDienThoai) VALUES
(1, 'user1', 'matkhau123', N'Nguyễn Văn Hùng', 'dangngoctamnhu2000@gmail.com', '0912375213');
SET IDENTITY_INSERT TaiKhoan OFF;
GO

SET IDENTITY_INSERT HangMuc ON;
INSERT INTO HangMuc (MaHangMuc, MaTaiKhoan, TenHangMuc, LoaiHangMuc, BieuTuong) VALUES
(1, 1, N'Ăn uống', 'Chi', 'anuong.png'),
(2, 1, N'Mua sắm', 'Chi', 'muasam.png'),
(3, 1, N'Di chuyển', 'Chi', 'giaothong.png'),
(4, 1, N'Giải trí', 'Chi', 'giaitri.png'),
(5, 1, N'Hóa đơn', 'Chi', 'hoadon.png'),
(6, 1, N'Sức khỏe', 'Chi', 'suckhoe.png'),
(7, 1, N'Tiết kiệm', 'Chi', 'tietkiem.png'),
(8, 1, N'Lương', 'Thu', 'luong.png'),
(9, 1, N'Thu nhập phụ', 'Thu', 'phucap.png'),
(10, 1, N'Tiền thưởng', 'Thu', 'thuong.png'),
(11, 1, N'Nhà cửa', 'Chi', 'nha.png'),
(12, 1, N'Quà tặng', 'Chi', 'quatang.png'),
(13, 1, N'Điện thoại', 'Chi', 'dienthoai.png'),
(14, 1, N'Sách vở', 'Chi', 'sachvo.png');
SET IDENTITY_INSERT HangMuc OFF;
GO

SET IDENTITY_INSERT GiaoDich ON;
INSERT INTO GiaoDich (MaGiaoDich, MaTaiKhoan, MaHangMuc, SoTien, LoaiGiaoDich, GhiChu, NgayGiaoDich, LanSuaCuoi, PhienBan, MaThietBi) VALUES
-- THÁNG 10/2025
(1, 1, 8, 8000000, 'Thu', N'Lương tháng 10', '2025-10-01', '2025-10-01 08:30:00', 1, 'ThietBi1'),
(2, 1, 1, 500000, 'Chi', N'Đi ăn với bạn bè', '2025-10-02', '2025-10-02 19:45:00', 1, 'ThietBi1'),
(3, 1, 2, 600000, 'Chi', N'Mua áo sơ mi', '2025-10-03', '2025-10-03 10:15:00', 1, 'ThietBi1'),
(4, 1, 9, 2000000, 'Thu', N'Freelance thiết kế', '2025-10-05', '2025-10-05 14:20:00', 1, 'ThietBi1'),
(5, 1, 5, 1200000, 'Chi', N'Tiền điện tháng 9', '2025-10-07', '2025-10-07 09:00:00', 1, 'ThietBi1'),
(6, 1, 3, 300000, 'Chi', N'Xăng xe', '2025-10-08', '2025-10-08 18:00:00', 1, 'ThietBi1'),
(7, 1, 4, 200000, 'Chi', N'Xem phim Joker', '2025-10-10', '2025-10-10 21:30:00', 1, 'ThietBi1'),
(8, 1, 6, 400000, 'Chi', N'Mua thuốc bổ', '2025-10-12', '2025-10-12 11:00:00', 1, 'ThietBi1'),
(9, 1, 7, 1000000, 'Chi', N'Gửi tiết kiệm', '2025-10-15', '2025-10-15 08:00:00', 1, 'ThietBi1'),
(10, 1, 1, 600000, 'Chi', N'Ăn trưa văn phòng', '2025-10-18', '2025-10-18 12:30:00', 1, 'ThietBi1'),
(11, 1, 2, 700000, 'Chi', N'Mua giày chạy bộ', '2025-10-20', '2025-10-20 16:45:00', 1, 'ThietBi1'),
(12, 1, 3, 250000, 'Chi', N'Grab đi tiệc', '2025-10-22', '2025-10-22 22:15:00', 1, 'ThietBi1'),
(13, 1, 10, 1500000, 'Thu', N'Thưởng dự án Quý 3', '2025-10-25', '2025-10-25 15:00:00', 1, 'ThietBi1'),
(14, 1, 4, 200000, 'Chi', N'Cafe cuối tuần', '2025-10-26', '2025-10-26 09:30:00', 1, 'ThietBi1'),
(15, 1, 5, 1300000, 'Chi', N'Tiền nước & Internet', '2025-10-28', '2025-10-28 10:00:00', 1, 'ThietBi1'),
(16, 1, 7, 1000000, 'Chi', N'Tích lũy cuối tháng', '2025-10-30', '2025-10-30 20:00:00', 1, 'ThietBi1'),
-- THÁNG 11/2025
(17, 1, 8, 8500000, 'Thu', N'Lương tháng 11', '2025-11-01', '2025-11-01 08:00:00', 1, 'ThietBi1'),
(18, 1, 11, 1500000, 'Chi', N'Sửa ống nước', '2025-11-02', '2025-11-02 14:30:00', 1, 'ThietBi1'),
(19, 1, 1, 400000, 'Chi', N'Ăn sáng', '2025-11-03', '2025-11-03 07:30:00', 1, 'ThietBi1'),
(20, 1, 12, 500000, 'Chi', N'Quà 20/11 sớm', '2025-11-04', '2025-11-04 19:00:00', 1, 'ThietBi1'),
(21, 1, 9, 2500000, 'Thu', N'Viết content', '2025-11-05', '2025-11-05 16:00:00', 1, 'ThietBi1'),
(22, 1, 13, 500000, 'Chi', N'Card điện thoại', '2025-11-05', '2025-11-05 20:00:00', 1, 'ThietBi1'),
(23, 1, 14, 300000, 'Chi', N'Mua sách IT', '2025-11-06', '2025-11-06 11:15:00', 1, 'ThietBi1'),
(24, 1, 2, 850000, 'Chi', N'Mua áo khoác mùa đông', '2025-11-07', '2025-11-07 18:30:00', 1, 'ThietBi1'),
(25, 1, 3, 200000, 'Chi', N'Bảo dưỡng xe máy', '2025-11-08', '2025-11-08 09:45:00', 1, 'ThietBi1'),
(26, 1, 4, 250000, 'Chi', N'Vé xem ca nhạc', '2025-11-09', '2025-11-09 20:00:00', 1, 'ThietBi1'),
(27, 1, 5, 1250000, 'Chi', N'Tiền điện tháng 10', '2025-11-10', '2025-11-10 08:15:00', 1, 'ThietBi1'),
(28, 1, 6, 300000, 'Chi', N'Vitamin tổng hợp', '2025-11-11', '2025-11-11 12:00:00', 1, 'ThietBi1'),
(29, 1, 7, 2000000, 'Chi', N'Tiết kiệm tháng 11', '2025-11-12', '2025-11-12 13:30:00', 1, 'ThietBi1'),
(30, 1, 1, 450000, 'Chi', N'Lẩu cuối tuần', '2025-11-14', '2025-11-14 19:30:00', 1, 'ThietBi1'),
(31, 1, 3, 200000, 'Chi', N'Grab đi làm mưa', '2025-11-15', '2025-11-15 08:45:00', 1, 'ThietBi1'),
(32, 1, 4, 300000, 'Chi', N'Netflix & Spotify', '2025-11-16', '2025-11-16 21:00:00', 1, 'ThietBi1'),
(33, 1, 2, 900000, 'Chi', N'Mua quần Jean', '2025-11-18', '2025-11-18 15:20:00', 1, 'ThietBi1'),
(34, 1, 5, 1100000, 'Chi', N'Internet 6 tháng', '2025-11-19', '2025-11-19 10:00:00', 1, 'ThietBi1'),
(35, 1, 6, 350000, 'Chi', N'Khám răng', '2025-11-20', '2025-11-20 14:00:00', 1, 'ThietBi1'),
(36, 1, 7, 1000000, 'Chi', N'Quỹ dự phòng', '2025-11-21', '2025-11-21 09:00:00', 1, 'ThietBi1'),
(37, 1, 9, 2200000, 'Thu', N'Dự án ngoài giờ', '2025-11-22', '2025-11-22 23:00:00', 1, 'ThietBi1'),
(38, 1, 1, 500000, 'Chi', N'Cafe làm việc', '2025-11-23', '2025-11-23 14:30:00', 1, 'ThietBi1'),
(39, 1, 11, 200000, 'Chi', N'Bóng đèn mới', '2025-11-24', '2025-11-24 17:00:00', 1, 'ThietBi1'),
(40, 1, 1, 150000, 'Chi', N'Ăn sáng vội', '2025-11-24', '2025-11-24 07:45:00', 1, 'ThietBi1'),
-- THÁNG 12/2025
(41, 1, 8, 9000000, 'Thu', N'Lương tháng 12', '2025-12-01', '2025-12-01 08:10:00', 1, 'ThietBi1'),
(42, 1, 1, 450000, 'Chi', N'Ăn sáng đầu tháng', '2025-12-02', '2025-12-02 07:40:00', 1, 'ThietBi1'),
(43, 1, 2, 750000, 'Chi', N'Mua áo len mùa đông', '2025-12-03', '2025-12-03 18:20:00', 1, 'ThietBi1'),
(44, 1, 9, 2100000, 'Thu', N'Freelance thiết kế banner', '2025-12-04', '2025-12-04 15:30:00', 1, 'ThietBi1'),
(45, 1, 5, 1300000, 'Chi', N'Tiền điện tháng 11', '2025-12-05', '2025-12-05 09:10:00', 1, 'ThietBi1'),
(46, 1, 3, 250000, 'Chi', N'Xăng xe', '2025-12-06', '2025-12-06 19:00:00', 1, 'ThietBi1'),
(47, 1, 4, 180000, 'Chi', N'Cafe cuối tuần', '2025-12-07', '2025-12-07 10:00:00', 1, 'ThietBi1'),
(48, 1, 6, 320000, 'Chi', N'Mua vitamin C', '2025-12-08', '2025-12-08 11:30:00', 1, 'ThietBi1');
SET IDENTITY_INSERT GiaoDich OFF;
GO

SET IDENTITY_INSERT NganSach ON;
INSERT INTO NganSach (MaNganSach, MaTaiKhoan, SoTienNganSach, ThoiGianBatDau, ThoiGianKetThuc) VALUES
(1, 1, 9000000, '2025-10-01', '2025-10-31'),
(2, 1, 9500000, '2025-11-01', '2025-11-30'),
(3, 1, 10000000, '2025-12-01', '2025-12-31');
SET IDENTITY_INSERT NganSach OFF;
GO

-- Tạo Index hỗ trợ tìm kiếm chi tiêu
CREATE NONCLUSTERED INDEX [IX_GiaoDich_TimKiemChiTieu]
ON [dbo].[GiaoDich] ([MaTaiKhoan], [LoaiGiaoDich], [NgayGiaoDich])
INCLUDE ([SoTien], [MaHangMuc]);
GO

-- Tạo Index hỗ trợ thống kê tổng thu chi
CREATE NONCLUSTERED INDEX [IX_GiaoDich_TongThuChi]
ON [dbo].[GiaoDich] ([MaTaiKhoan], [NgayGiaoDich])
INCLUDE ([LoaiGiaoDich], [SoTien], [MaHangMuc]);
GO

-- SP: Chi tiêu theo hạng mục
CREATE PROCEDURE sp_ChiTieuTheoHangMuc
    @MaTaiKhoan INT,
    @ThoiGian DATETIME,
    @LoaiThoiGian NVARCHAR(10) -- 'Day', 'Week', 'Month', 'Year'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NgayBatDau DATE, @NgayKetThuc DATE;

    -- Xử lý thời gian bắt đầu và kết thúc dựa trên @LoaiThoiGian
    IF @LoaiThoiGian = 'Day'
    BEGIN
        SET @NgayBatDau = CAST(@ThoiGian AS DATE);
        SET @NgayKetThuc = CAST(@ThoiGian AS DATE);
    END
    ELSE IF @LoaiThoiGian = 'Week'
    BEGIN
        SET @NgayBatDau = DATEADD(week, DATEDIFF(week, 0, @ThoiGian), 0);
        SET @NgayKetThuc = DATEADD(day, 6, @NgayBatDau);
    END
    ELSE IF @LoaiThoiGian = 'Month'
    BEGIN
        SET @NgayBatDau = DATEFROMPARTS(YEAR(@ThoiGian), MONTH(@ThoiGian), 1);
        SET @NgayKetThuc = EOMONTH(@ThoiGian);
    END
    ELSE IF @LoaiThoiGian = 'Year'
    BEGIN
        SET @NgayBatDau = DATEFROMPARTS(YEAR(@ThoiGian), 1, 1);
        SET @NgayKetThuc = DATEFROMPARTS(YEAR(@ThoiGian), 12, 31);
    END

    -- CTE gom nhóm dữ liệu chi tiêu
    ;WITH DataGomNhom AS (
        SELECT
            gd.MaHangMuc,
            SUM(gd.SoTien) AS TongChiTieuTheoHangMuc,
            COUNT(gd.MaGiaoDich) AS SoLuongGiaoDich
        FROM GiaoDich gd
        WHERE gd.MaTaiKhoan = @MaTaiKhoan
          AND gd.LoaiGiaoDich = N'Chi'
          AND gd.NgayGiaoDich >= @NgayBatDau
          AND gd.NgayGiaoDich <= @NgayKetThuc
        GROUP BY gd.MaHangMuc
    )
    -- Select kết quả cuối cùng và tính phần trăm
    SELECT
        (SELECT HoTen FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan) AS TenNguoiDung,
        ISNULL(hm.TenHangMuc, N'Chưa phân loại') AS TenHangMuc,
        cte.SoLuongGiaoDich,
        cte.TongChiTieuTheoHangMuc AS TongChiTieu,
        CAST(
            CASE
                WHEN SUM(cte.TongChiTieuTheoHangMuc) OVER() = 0 THEN 0
                ELSE cte.TongChiTieuTheoHangMuc * 100.0 / SUM(cte.TongChiTieuTheoHangMuc) OVER()
            END
        AS DECIMAL(5, 2)) AS PhanTramTrongTongChi
    FROM DataGomNhom cte
    LEFT JOIN HangMuc hm ON cte.MaHangMuc = hm.MaHangMuc
    ORDER BY cte.TongChiTieuTheoHangMuc DESC;
END;
GO

-- SP: Tổng Thu Chi
CREATE PROCEDURE sp_TongThuChi
    @MaTaiKhoan INT,
    @ThoiGian DATETIME,
    @LoaiThoiGian NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NgayBatDau DATE, @NgayKetThuc DATE;
    
    -- Xử lý thời gian
    IF @LoaiThoiGian = 'Day' 
    BEGIN 
        SET @NgayBatDau = CAST(@ThoiGian AS DATE); 
        SET @NgayKetThuc = CAST(@ThoiGian AS DATE); 
    END
    ELSE IF @LoaiThoiGian = 'Week' 
    BEGIN 
        SET @NgayBatDau = DATEADD(week, DATEDIFF(week, 0, @ThoiGian), 0); 
        SET @NgayKetThuc = DATEADD(day, 6, @NgayBatDau); 
    END
    ELSE IF @LoaiThoiGian = 'Month' 
    BEGIN 
        SET @NgayBatDau = DATEFROMPARTS(YEAR(@ThoiGian), MONTH(@ThoiGian), 1); 
        SET @NgayKetThuc = EOMONTH(@ThoiGian); 
    END
    ELSE IF @LoaiThoiGian = 'Year' 
    BEGIN 
        SET @NgayBatDau = DATEFROMPARTS(YEAR(@ThoiGian), 1, 1); 
        SET @NgayKetThuc = DATEFROMPARTS(YEAR(@ThoiGian), 12, 31); 
    END

    DECLARE @TenNguoiDung NVARCHAR(100) = (SELECT HoTen FROM TaiKhoan WITH(NOLOCK) WHERE MaTaiKhoan = @MaTaiKhoan);

    IF @LoaiThoiGian = 'Year'
    BEGIN
        ;WITH MonthlyData AS (
            SELECT
                MONTH(NgayGiaoDich) AS Thang,
                SUM(CASE WHEN LoaiGiaoDich = N'Thu' THEN SoTien ELSE 0 END) AS SoTienThu,
                SUM(CASE WHEN LoaiGiaoDich = N'Chi' THEN SoTien ELSE 0 END) AS SoTienChi
            FROM GiaoDich WITH(NOLOCK)
            WHERE MaTaiKhoan = @MaTaiKhoan AND NgayGiaoDich BETWEEN @NgayBatDau AND @NgayKetThuc
            GROUP BY MONTH(NgayGiaoDich)
        )
        SELECT
            @TenNguoiDung AS TenNguoiDung,
            Thang, SoTienThu, SoTienChi,
            SUM(SoTienThu) OVER() AS TongThu,
            SUM(SoTienChi) OVER() AS TongChi,
            SUM(SoTienThu) OVER() - SUM(SoTienChi) OVER() AS ChenhLech,
            CAST(0 AS DECIMAL(18,2)) AS ChenhLechChiTiet,
            NULL AS MaGiaoDich, NULL AS NgayGiaoDich, NULL AS LoaiGiaoDich,
            CAST(0 AS DECIMAL(18,2)) AS SoTien,
            NULL AS GhiChu, NULL AS TenHangMuc,
            CAST(0 AS DECIMAL(18,2)) AS TongThuChiTiet,
            CAST(0 AS DECIMAL(18,2)) AS TongChiChiTiet
        FROM MonthlyData
        ORDER BY Thang;
    END
    ELSE
    BEGIN
        SELECT
            @TenNguoiDung AS TenNguoiDung,
            NULL AS Thang, NULL AS SoTienThu, NULL AS SoTienChi,
            NULL AS TongThu, NULL AS TongChi, NULL AS ChenhLech,
            SUM(CASE WHEN gd.LoaiGiaoDich = N'Thu' THEN gd.SoTien ELSE 0 END) OVER () - SUM(CASE WHEN gd.LoaiGiaoDich = N'Chi' THEN gd.SoTien ELSE 0 END) OVER () AS ChenhLechChiTiet,
            gd.MaGiaoDich, gd.NgayGiaoDich, gd.LoaiGiaoDich, gd.SoTien, gd.GhiChu,
            ISNULL(hm.TenHangMuc, N'Khác') AS TenHangMuc,
            SUM(CASE WHEN gd.LoaiGiaoDich = N'Thu' THEN gd.SoTien ELSE 0 END) OVER () AS TongThuChiTiet,
            SUM(CASE WHEN gd.LoaiGiaoDich = N'Chi' THEN gd.SoTien ELSE 0 END) OVER () AS TongChiChiTiet
        FROM GiaoDich gd WITH(NOLOCK)
        LEFT JOIN HangMuc hm WITH(NOLOCK) ON gd.MaHangMuc = hm.MaHangMuc
        WHERE gd.MaTaiKhoan = @MaTaiKhoan AND NgayGiaoDich BETWEEN @NgayBatDau AND @NgayKetThuc
        ORDER BY gd.NgayGiaoDich DESC;
    END
END;
GO