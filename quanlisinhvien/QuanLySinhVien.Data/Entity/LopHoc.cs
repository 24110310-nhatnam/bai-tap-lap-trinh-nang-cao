namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// Thực thể Lớp học - ánh xạ bảng LopHoc trong cơ sở dữ liệu QLSV.
    /// </summary>
    public class LopHoc
    {
        public string MaLop { get; set; }

        public string TenLop { get; set; }

        /// <summary>Số sinh viên của lớp (được tính bằng truy vấn JOIN khi lấy danh sách).</summary>
        public int SoSinhVien { get; set; }

        public LopHoc()
        {
        }

        public LopHoc(string maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        /// <summary>Chuỗi hiển thị trong ComboBox: "CNTT01 - Công nghệ thông tin 1".</summary>
        public string HienThi
        {
            get { return MaLop + " - " + TenLop; }
        }

        public override string ToString()
        {
            return HienThi;
        }
    }
}
