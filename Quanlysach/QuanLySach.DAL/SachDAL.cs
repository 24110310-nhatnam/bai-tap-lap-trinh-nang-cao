using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using QuanLySach.Entity.Context;
using QuanLySach.Entity.Entities;

namespace QuanLySach.DAL
{
    public class SachDAL
    {
        /// <summary>
        /// Lấy tất cả sách kèm thể loại
        /// </summary>
        public List<Sach> GetAll()
        {
            using (var context = new QuanLySachContext())
            {
                return context.Sachs
                    .Include(s => s.TheLoai)
                    .OrderBy(s => s.MaSach)
                    .ToList();
            }
        }

        /// <summary>
        /// Lấy sách theo mã
        /// </summary>
        public Sach GetById(string maSach)
        {
            if (string.IsNullOrWhiteSpace(maSach))
                return null;

            using (var context = new QuanLySachContext())
            {
                return context.Sachs
                    .Include(s => s.TheLoai)
                    .FirstOrDefault(s => s.MaSach == maSach.Trim());
            }
        }

        /// <summary>
        /// Kiểm tra mã sách đã tồn tại chưa
        /// </summary>
        public bool IsMaSachExists(string maSach)
        {
            if (string.IsNullOrWhiteSpace(maSach))
                return false;

            using (var context = new QuanLySachContext())
            {
                return context.Sachs.Any(s => s.MaSach == maSach.Trim());
            }
        }

        /// <summary>
        /// Thêm sách mới
        /// </summary>
        public bool Add(Sach sach)
        {
            if (sach == null)
                throw new ArgumentNullException("sach");

            using (var context = new QuanLySachContext())
            {
                context.Sachs.Add(sach);
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Cập nhật sách
        /// </summary>
        public bool Update(Sach sach)
        {
            if (sach == null)
                throw new ArgumentNullException("sach");

            using (var context = new QuanLySachContext())
            {
                var existing = context.Sachs.Find(sach.MaSach);
                if (existing == null)
                    return false;

                existing.TenSach = sach.TenSach;
                existing.NgayNhap = sach.NgayNhap;
                existing.SoLuong = sach.SoLuong;
                existing.DonGia = sach.DonGia;
                existing.MaLoai = sach.MaLoai;

                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Xóa sách theo mã
        /// </summary>
        public bool Delete(string maSach)
        {
            if (string.IsNullOrWhiteSpace(maSach))
                return false;

            using (var context = new QuanLySachContext())
            {
                var sach = context.Sachs.Find(maSach.Trim());
                if (sach == null)
                    return false;

                context.Sachs.Remove(sach);
                return context.SaveChanges() > 0;
            }
        }
    }
}
