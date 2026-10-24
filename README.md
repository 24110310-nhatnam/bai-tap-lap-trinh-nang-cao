# QLTV - Quản lý thư viện

Ứng dụng ASP.NET Core MVC dùng Entity Framework Core Code First và SQL Server.

## Chức năng
- Quản lý sách: xem danh sách, xem chi tiết, thêm, sửa, xóa.
- Quản lý tác giả: xem danh sách, xem chi tiết, thêm, sửa, xóa.
- Quan hệ một-nhiều: một tác giả có thể có nhiều sách.
- Entity Framework Core tạo bảng từ các lớp C# (`Sach`, `TacGia`).

## Công nghệ
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server

## Yêu cầu
- .NET 8 SDK
- SQL Server và (khuyến nghị) SQL Server Management Studio
- Công cụ EF CLI: `dotnet tool install --global dotnet-ef`

## Cấu hình database
Mở `appsettings.json` và sửa `ConnectionStrings:QLTVConnection` cho đúng máy của bạn.

Ví dụ SQL Server mặc định:
`Server=.;Database=QLTVDB;Trusted_Connection=True;TrustServerCertificate=True;`

Nếu dùng SQL Server Express, có thể cần:
`Server=localhost\\SQLEXPRESS;Database=QLTVDB;Trusted_Connection=True;TrustServerCertificate=True;`

## Chạy dự án và tạo database

Mở Terminal tại thư mục có `QLTV.csproj`, chạy:

```bash
dotnet restore
dotnet build
dotnet ef migrations add InitialLibraryDB --context QLTVDBContext
dotnet ef database update --context QLTVDBContext
dotnet run
```

Mở URL được hiển thị trong Terminal.

Nếu thư mục `Migrations` đã có migration được commit trong GitHub, không tạo lại migration `InitialLibraryDB`; chỉ chạy:

```bash
dotnet ef database update --context QLTVDBContext
```

## Cấu trúc thư mục

```text
QLTV/
├── Controllers/
│   ├── SachController.cs
│   └── TacGiaController.cs
├── Data/
│   └── QLTVDBContext.cs
├── Models/
│   ├── Sach.cs
│   └── TacGia.cs
├── Views/
│   ├── Sach/
│   ├── TacGia/
│   └── Shared/
├── wwwroot/css/site.css
├── appsettings.json
├── Program.cs
└── QLTV.csproj
```

## Đẩy lên GitHub

Tạo repository mới trên GitHub, sau đó chạy tại thư mục dự án:

```bash
git init
git add .
git commit -m "Initial commit: QLTV Code First"
git branch -M main
git remote add origin https://github.com/USERNAME/REPOSITORY.git
git push -u origin main
```

Thay URL `origin` bằng URL repository của bạn.

## Ghi chú
- Không đưa mật khẩu database hoặc thông tin bí mật vào GitHub.
- Nếu SQL Server không dùng Windows Authentication, hãy thay connection string phù hợp và không commit thông tin đăng nhập.
