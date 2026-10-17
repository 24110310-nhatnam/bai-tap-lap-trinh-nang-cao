using System;

namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// Thực thể Sinh viên - ánh xạ bảng SinhVien trong cơ sở dữ liệu QLSV.
    /// </summary>
    public class SinhVien
    {
        public string MaSV { get; set; }

        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        /// <summary>Ngày sinh dạng chuỗi theo định dạng dd/MM/yyyy (rỗng nếu không có).</summary>
        public string NgaySinh { get; set; }

        public string SoDienThoai { get; set; }

        public string MaLop { get; set; }

        public double Diem { get; set; }

        public string Email { get; set; }

        public SinhVien()
        {
        }

        public SinhVien(string maSV, string hoTen, string gioiTinh, string ngaySinh,
                        string soDienThoai, string maLop, double diem, string email)
        {
            MaSV = maSV;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            SoDienThoai = soDienThoai;
            MaLop = maLop;
            Diem = diem;
            Email = email;
        }

        /// <summary>Chuỗi hiển thị nhanh trong ComboBox/lưới.</summary>
        public override string ToString()
        {
            return MaSV + " - " + HoTen;
        }
    }
}
