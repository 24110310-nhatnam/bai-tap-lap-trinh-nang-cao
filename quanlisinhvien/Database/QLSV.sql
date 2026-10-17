-- ============================================================
-- Script tạo cơ sở dữ liệu QLSV cho bài Quản lý Sinh viên (ADO.NET)
-- Chạy bằng SQL Server Management Studio (SSMS).
-- LƯU Ý: Script sẽ XÓA và TẠO LẠI 2 bảng SinhVien, LopHoc trong CSDL QLSV.
-- ============================================================

IF DB_ID(N'QLSV') IS NULL
BEGIN
    CREATE DATABASE QLSV;
END
GO

USE QLSV;
GO

-- Xóa bảng cũ (xóa SinhVien trước vì có khóa ngoại tham chiếu LopHoc)
IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NOT NULL DROP TABLE dbo.SinhVien;
GO
IF OBJECT_ID(N'dbo.LopHoc', N'U') IS NOT NULL DROP TABLE dbo.LopHoc;
GO

-- Bảng Lớp học
CREATE TABLE dbo.LopHoc
(
    MaLop  NVARCHAR(10) NOT NULL,
    TenLop NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_LopHoc PRIMARY KEY (MaLop)
);
GO

-- Bảng Sinh viên
CREATE TABLE dbo.SinhVien
(
    MaSV        NVARCHAR(10) NOT NULL,
    HoTen       NVARCHAR(50) NOT NULL,
    GioiTinh    NVARCHAR(5)  NULL,
    NgaySinh    DATE         NULL,
    Diem        FLOAT        NULL,
    SoDienThoai NVARCHAR(15) NULL,
    Email       NVARCHAR(50) NULL,
    MaLop       NVARCHAR(10) NULL,
    CONSTRAINT PK_SinhVien PRIMARY KEY (MaSV),
    CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (MaLop) REFERENCES dbo.LopHoc (MaLop)
);
GO

-- Dữ liệu mẫu
INSERT INTO dbo.LopHoc (MaLop, TenLop) VALUES
    (N'CNTT01', N'Công nghệ thông tin 1'),
    (N'CNTT02', N'Công nghệ thông tin 2'),
    (N'KT01',   N'Kế toán 1');
GO

INSERT INTO dbo.SinhVien (MaSV, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email, MaLop) VALUES
    (N'SV001', N'Nguyễn Văn An',  N'Nam', '2003-01-15', 8.5, N'0901234567', N'an.nv@example.com',   N'CNTT01'),
    (N'SV002', N'Trần Thị Bình',  N'Nữ',  '2003-03-22', 7.0, N'0912345678', N'binh.tt@example.com', N'CNTT01'),
    (N'SV003', N'Lê Minh Châu',   N'Nam', '2002-12-05', 9.0, N'0923456789', N'chau.lm@example.com', N'CNTT02'),
    (N'SV004', N'Phạm Thu Dung',  N'Nữ',  '2003-07-30', 6.5, N'0934567890', N'dung.pt@example.com', N'KT01');
GO

-- Kiểm tra kết quả
SELECT * FROM dbo.LopHoc;
SELECT * FROM dbo.SinhVien;
GO
