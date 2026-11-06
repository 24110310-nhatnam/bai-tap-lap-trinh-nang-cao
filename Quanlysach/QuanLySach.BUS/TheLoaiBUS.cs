using System;
using System.Collections.Generic;
using QuanLySach.DAL;
using QuanLySach.Entity.Entities;

namespace QuanLySach.BUS
{
    public class TheLoaiBUS
    {
        private readonly TheLoaiDAL _theLoaiDAL;

        public TheLoaiBUS()
        {
            _theLoaiDAL = new TheLoaiDAL();
        }

        /// <summary>
        /// Lấy tất cả thể loại
        /// </summary>
        public List<TheLoai> GetAll()
        {
            return _theLoaiDAL.GetAll();
        }

        /// <summary>
        /// Lấy thể loại theo mã
        /// </summary>
        public TheLoai GetById(string maLoai)
        {
            return _theLoaiDAL.GetById(maLoai);
        }

        /// <summary>
        /// Seed dữ liệu mẫu (gọi khi khởi động ứng dụng)
        /// </summary>
        public void SeedData()
        {
            _theLoaiDAL.SeedData();
        }
    }
}
