# Quản lý Chi tiêu Cá nhân (CashCat)

Ứng dụng Desktop giúp theo dõi, phân tích và quản lý tài chính cá nhân hiệu quả, được xây dựng trên nền tảng .NET Framework (Windows Forms).

## Giới thiệu
Dự án được phát triển nhằm giải quyết vấn đề quản lý tài chính cá nhân, giúp người dùng dễ dàng theo dõi dòng tiền, thiết lập ngân sách và xem báo cáo thu chi trực quan.

## Tính năng nổi bật

### 1. Hệ thống Xác thực & Bảo mật (Authentication)
* Đăng ký & Đăng nhập: Kiểm tra tính hợp lệ của dữ liệu.
* Quên mật khẩu (OTP): Tích hợp gửi mã OTP xác thực qua Email 
* Quản lý tài khoản: Cập nhật thông tin và đổi mật khẩu.

### 2. Quản lý Giao dịch (Core Features)
* Dashboard: Hiển thị nhanh số dư, tổng thu/chi.
* CRUD Giao dịch: Thêm, sửa, xóa khoản thu chi.
* Tìm kiếm: Tra cứu giao dịch theo từ khóa, thời gian, số tiền.

### 3. Quản lý Ngân sách & Hạng mục
* Hạng mục: Tùy chỉnh danh mục Thu/Chi.
* Ngân sách: Đặt hạn mức chi tiêu và cảnh báo vượt mức.

### 4. Báo cáo & Thống kê
* Biểu đồ: Hiển thị biến động số dư.
* Xuất báo cáo (Crystal Reports): Tạo báo cáo chi tiết để in ấn.

---

## Công nghệ sử dụng
* Ngôn ngữ: C# (.NET Framework)
* Cơ sở dữ liệu: SQL Server
* Lưu trữ đám mây: Firebase
* Thư viện: Crystal Reports, System.Net.Mail, Chart Controls.

---

## Hình ảnh Demo Chi tiết

### 1. Màn hình Chào mừng & Xác thực
(Quy trình đăng ký, đăng nhập và xác thực người dùng)

![Màn hình Chào mừng](xac-thuc.png)
![Đăng nhập](dang-nhap.png)
![Đăng ký tài khoản mới](dang-ky.png)

### 2. Quy trình Khôi phục Mật khẩu (OTP)
(Hệ thống gửi mã OTP qua Email để lấy lại mật khẩu)

![Gửi yêu cầu quên mật khẩu](khoi-phuc-mk1.png)
![Nhập mã xác thực](khoi-phuc-mk2.png)
![Đặt lại mật khẩu mới](khoi-phuc-mk3.png)

### 3. Thông tin Cá nhân & Đổi Mật khẩu
![Thông tin cá nhân](ttcn.png)
![Đổi mật khẩu](doi-mk.png)

### 4. Giao diện Chính (Dashboard)
![Trang chủ tổng quan](trang-chu.png)

### 5. Quản lý Giao dịch & Tìm kiếm
![Danh sách giao dịch](giao-dich.png)
![Thêm giao dịch mới](them-gd.png)
![Tìm kiếm nâng cao](tim-kiem.png)

### 6. Quản lý Hạng mục & Ngân sách
![Quản lý hạng mục](hang-muc.png)
![Thêm hạng mục mới](them-hm.png)
![Thiết lập ngân sách chi tiêu](ngan-sach.png)

### 7. Báo cáo & Thống kê
![Biểu đồ thống kê](thong-ke.png)
![Tạo và xuất báo cáo](tao-bao-cao.png)

---

## Hướng dẫn Cài đặt
1. Clone dự án:
git clone https://github.com/DangNhu-0101/QuanLyTaiChinhCaNhanCashCat.git
2. Mở file solution (.sln) bằng Visual Studio.
3. Cấu hình chuỗi kết nối (Connection String) trong file App.config.
4. Chạy Script SQL trong thư mục Database.
5. Nhấn Start để chạy ứng dụng.

---
Liên hệ: dangngoctamnhu2020@gmail.com
