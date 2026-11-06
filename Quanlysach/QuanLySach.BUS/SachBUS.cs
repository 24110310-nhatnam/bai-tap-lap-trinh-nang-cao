using System;
using System.Collections.Generic;
using QuanLySach.DAL;
using QuanLySach.Entity.Entities;

namespace QuanLySach.BUS
{
    public class SachBUS
    {
        private readonly SachDAL _sachDAL;

        public SachBUS()
        {
            _sachDAL = new SachDAL();
        }

        /// <summary>
        /// Lấy tất cả sách
        /// </summary>
        public List<Sach> GetAll()
        {
            return _sachDAL.GetAll();
        }

        /// <summary>
        /// Lấy sách theo mã
        /// </summary>
        public Sach GetById(string maSach)
        {
            return _sachDAL.GetById(maSach);
        }

        /// <summary>
        /// Validate dữ liệu sách
        /// </summary>
        /// <param name="maSach">Mã sách</param>
        /// <param name="tenSach">Tên sách</param>
        /// <param name="maLoai">Mã thể loại</param>
        /// <param name="ngayNhap">Ngày nhập</param>
        /// <param name="soLuongStr">Số lượng (chuỗi từ TextBox)</param>
        /// <param name="donGiaStr">Đơn giá (chuỗi từ TextBox)</param>
        /// <param name="isAdd">true = thêm mới, false = sửa</param>
        /// <param name="errorMessage">Thông báo lỗi nếu có</param>
        /// <returns>true nếu hợp lệ</returns>
        public bool Validate(
            string maSach,
            string tenSach,
            string maLoai,
            DateTime ngayNhap,
            string soLuongStr,
            string donGiaStr,
            bool isAdd,
            out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate Mã sách
            if (string.IsNullOrWhiteSpace(maSach))
            {
                errorMessage = "Mã sách không được để trống.";
                return false;
            }
            if (maSach.Trim().Length > 20)
            {
                errorMessage = "Mã sách tối đa 20 ký tự.";
                return false;
            }

            // Validate Tên sách
            if (string.IsNullOrWhiteSpace(tenSach))
            {
                errorMessage = "Tên sách không được để trống.";
                return false;
            }
            if (tenSach.Trim().Length > 150)
            {
                errorMessage = "Tên sách tối đa 150 ký tự.";
                return false;
            }

            // Validate Thể loại
            if (string.IsNullOrWhiteSpace(maLoai))
            {
                errorMessage = "Vui lòng chọn thể loại.";
                return false;
            }

            // Validate Ngày nhập
            if (ngayNhap.Date > DateTime.Now.Date)
            {
                errorMessage = "Ngày nhập không được lớn hơn ngày hiện tại.";
                return false;
            }

            // Validate Số lượng
            int soLuong;
            if (!int.TryParse(soLuongStr, out soLuong))
            {
                errorMessage = "Số lượng phải là số nguyên.";
                return false;
            }
            if (soLuong <= 0)
            {
                errorMessage = "Số lượng phải lớn hơn 0.";
                return false;
            }

            // Validate Đơn giá
            decimal donGia;
            if (!decimal.TryParse(donGiaStr, out donGia))
            {
                errorMessage = "Đơn giá phải là số.";
                return false;
            }
            if (donGia <= 0)
            {
                errorMessage = "Đơn giá phải lớn hơn 0.";
                return false;
            }

            // Khi thêm mới: kiểm tra trùng mã
            if (isAdd && _sachDAL.IsMaSachExists(maSach.Trim()))
            {
                errorMessage = string.Format("Mã sách '{0}' đã tồn tại.", maSach.Trim());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Thêm sách mới (đã qua validate)
        /// </summary>
        public bool ThemSach(string maSach, string tenSach, string maLoai,
            DateTime ngayNhap, int soLuong, decimal donGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                var sach = new Sach
                {
                    MaSach = maSach.Trim(),
                    TenSach = tenSach.Trim(),
                    MaLoai = maLoai.Trim(),
                    NgayNhap = ngayNhap,
                    SoLuong = soLuong,
                    DonGia = donGia
                };

                bool result = _sachDAL.Add(sach);
                if (!result)
                    errorMessage = "Thêm sách thất bại.";
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Sửa sách (đã qua validate)
        /// </summary>
        public bool SuaSach(string maSach, string tenSach, string maLoai,
            DateTime ngayNhap, int soLuong, decimal donGia, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                var sach = new Sach
                {
                    MaSach = maSach.Trim(),
                    TenSach = tenSach.Trim(),
                    MaLoai = maLoai.Trim(),
                    NgayNhap = ngayNhap,
                    SoLuong = soLuong,
                    DonGia = donGia
                };

                bool result = _sachDAL.Update(sach);
                if (!result)
                    errorMessage = "Cập nhật sách thất bại. Sách không tồn tại.";
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Xóa sách theo mã
        /// </summary>
        public bool XoaSach(string maSach, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(maSach))
            {
                errorMessage = "Mã sách không hợp lệ.";
                return false;
            }
            try
            {
                bool result = _sachDAL.Delete(maSach.Trim());
                if (!result)
                    errorMessage = "Xóa sách thất bại. Sách không tồn tại.";
                return result;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }
    }
}
