using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySach.Entity.Context;
using QuanLySach.Entity.Entities;

namespace QuanLySach.DAL
{
    public class TheLoaiDAL
    {
        /// <summary>
        /// Lấy tất cả thể loại, sắp xếp theo mã
        /// </summary>
        public List<TheLoai> GetAll()
        {
            using (var context = new QuanLySachContext())
            {
                return context.TheLoais
                    .OrderBy(t => t.MaLoai)
                    .ToList();
            }
        }

        /// <summary>
        /// Lấy thể loại theo mã
        /// </summary>
        public TheLoai GetById(string maLoai)
        {
            if (string.IsNullOrWhiteSpace(maLoai))
                return null;

            using (var context = new QuanLySachContext())
            {
                return context.TheLoais.Find(maLoai.Trim());
            }
        }

        /// <summary>
        /// Seed dữ liệu mẫu nếu chưa có
        /// </summary>
        public void SeedData()
        {
            using (var context = new QuanLySachContext())
            {
                // Seed thể loại
                var seedTheLoai = new List<TheLoai>
                {
                    new TheLoai { MaLoai = "TL001", TenLoai = "Văn học" },
                    new TheLoai { MaLoai = "TL002", TenLoai = "Khoa học" },
                    new TheLoai { MaLoai = "TL003", TenLoai = "Kỹ thuật" }
                };

                foreach (var tl in seedTheLoai)
                {
                    if (!context.TheLoais.Any(t => t.MaLoai == tl.MaLoai))
                        context.TheLoais.Add(tl);
                }

                context.SaveChanges();

                // Seed sách
                var seedSach = new List<Sach>
                {
                    new Sach
                    {
                        MaSach = "S001",
                        TenSach = "Truyện Kiều",
                        NgayNhap = new DateTime(2024, 1, 15),
                        SoLuong = 50,
                        DonGia = 85000m,
                        MaLoai = "TL001"
                    },
                    new Sach
                    {
                        MaSach = "S002",
                        TenSach = "Số đỏ",
                        NgayNhap = new DateTime(2024, 2, 20),
                        SoLuong = 30,
                        DonGia = 75000m,
                        MaLoai = "TL001"
                    },
                    new Sach
                    {
                        MaSach = "S003",
                        TenSach = "Vật lý đại cương",
                        NgayNhap = new DateTime(2024, 3, 10),
                        SoLuong = 100,
                        DonGia = 120000m,
                        MaLoai = "TL002"
                    },
                    new Sach
                    {
                        MaSach = "S004",
                        TenSach = "Lập trình C#",
                        NgayNhap = new DateTime(2024, 4, 5),
                        SoLuong = 75,
                        DonGia = 150000m,
                        MaLoai = "TL003"
                    },
                    new Sach
                    {
                        MaSach = "S005",
                        TenSach = "Cơ sở dữ liệu",
                        NgayNhap = new DateTime(2024, 5, 1),
                        SoLuong = 60,
                        DonGia = 130000m,
                        MaLoai = "TL003"
                    }
                };

                foreach (var s in seedSach)
                {
                    if (!context.Sachs.Any(x => x.MaSach == s.MaSach))
                        context.Sachs.Add(s);
                }

                context.SaveChanges();
            }
        }
    }
}
