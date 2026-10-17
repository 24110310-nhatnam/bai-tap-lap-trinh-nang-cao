using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu cho bảng SinhVien, thao tác hoàn toàn bằng ADO.NET
    /// (SqlConnection, SqlCommand, SqlParameter, SqlDataReader, SqlDataAdapter).
    /// YÊU CẦU: mọi phương thức đều ném ngoại lệ ra ngoài khi xảy ra lỗi để tầng Business xử lý.
    /// </summary>
    public class SinhVienDAO
    {
        private const string DanhSachCot =
            "MaSV, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email, MaLop";

        private readonly DataHelper dataHelper = new DataHelper();

        /// <summary>Lấy toàn bộ sinh viên (đọc bằng SqlDataReader).</summary>
        public List<SinhVien> GetAll()
        {
            try
            {
                List<SinhVien> danhSach = new List<SinhVien>();
                string sql = "SELECT " + DanhSachCot + " FROM SinhVien ORDER BY MaSV";

                using (SqlDataReader reader = dataHelper.ExecuteReader(sql))
                {
                    while (reader.Read())
                    {
                        danhSach.Add(DocDuLieu(reader));
                    }
                }
                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không lấy được danh sách sinh viên. " + ex.Message, ex);
            }
        }

        /// <summary>Tìm sinh viên theo mã. Trả về null nếu không tồn tại, ném ngoại lệ nếu có lỗi.</summary>
        public SinhVien GetByMaSV(string maSV)
        {
            try
            {
                string sql = "SELECT " + DanhSachCot + " FROM SinhVien WHERE MaSV = @MaSV";

                using (SqlDataReader reader = dataHelper.ExecuteReader(sql,
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 10) { Value = maSV }))
                {
                    if (reader.Read())
                        return DocDuLieu(reader);

                    return null; // không tìm thấy
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không tìm được sinh viên có mã '" + maSV + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Lấy danh sách sinh viên theo lớp (đọc bằng SqlDataAdapter + DataTable).</summary>
        public List<SinhVien> GetByMaLop(string maLop)
        {
            try
            {
                string sql = "SELECT " + DanhSachCot + " FROM SinhVien WHERE MaLop = @MaLop ORDER BY MaSV";

                DataTable dt = dataHelper.GetDataTable(sql,
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = maLop });

                List<SinhVien> danhSach = new List<SinhVien>();
                foreach (DataRow dong in dt.Rows)
                {
                    danhSach.Add(DocDuLieu(dong));
                }
                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không lấy được sinh viên thuộc lớp '" + maLop + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Kiểm tra mã sinh viên đã tồn tại chưa (dùng ExecuteScalar).</summary>
        public bool Exists(string maSV)
        {
            try
            {
                object ketQua = dataHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV",
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 10) { Value = maSV });

                return Convert.ToInt32(ketQua) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không kiểm tra được sinh viên '" + maSV + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Đếm số sinh viên của một lớp (dùng cho quy tắc "không xóa lớp còn sinh viên").</summary>
        public int CountByMaLop(string maLop)
        {
            try
            {
                object ketQua = dataHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM SinhVien WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = maLop });

                return Convert.ToInt32(ketQua);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không đếm được sinh viên của lớp '" + maLop + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Thêm sinh viên mới (SqlParameter chống SQL Injection).</summary>
        public void Insert(SinhVien sv)
        {
            try
            {
                const string sql =
                    "INSERT INTO SinhVien (MaSV, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email, MaLop) " +
                    "VALUES (@MaSV, @HoTen, @GioiTinh, @NgaySinh, @Diem, @SoDienThoai, @Email, @MaLop)";

                dataHelper.ExecuteNonQuery(sql, TaoThamSo(sv));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: thêm sinh viên '" + sv.MaSV + "' thất bại. " + ex.Message, ex);
            }
        }

        /// <summary>Cập nhật sinh viên theo mã (SqlParameter chống SQL Injection).</summary>
        public void Update(SinhVien sv)
        {
            try
            {
                const string sql =
                    "UPDATE SinhVien SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, " +
                    "Diem = @Diem, SoDienThoai = @SoDienThoai, Email = @Email, MaLop = @MaLop " +
                    "WHERE MaSV = @MaSV";

                int soDongAnhHuong = dataHelper.ExecuteNonQuery(sql, TaoThamSo(sv));
                if (soDongAnhHuong == 0)
                    throw new Exception("Không tìm thấy sinh viên '" + sv.MaSV + "' trong cơ sở dữ liệu để cập nhật.");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Không tìm thấy", StringComparison.Ordinal))
                    throw;

                throw new Exception("Lỗi tầng dữ liệu: cập nhật sinh viên '" + sv.MaSV + "' thất bại. " + ex.Message, ex);
            }
        }

        /// <summary>Xóa sinh viên theo mã (xóa thật trong cơ sở dữ liệu).</summary>
        public void Delete(string maSV)
        {
            try
            {
                int soDongAnhHuong = dataHelper.ExecuteNonQuery(
                    "DELETE FROM SinhVien WHERE MaSV = @MaSV",
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 10) { Value = maSV });

                if (soDongAnhHuong == 0)
                    throw new Exception("Không tìm thấy sinh viên '" + maSV + "' trong cơ sở dữ liệu để xóa.");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Không tìm thấy", StringComparison.Ordinal))
                    throw;

                throw new Exception("Lỗi tầng dữ liệu: xóa sinh viên '" + maSV + "' thất bại. " + ex.Message, ex);
            }
        }

        // ------------------------------------------------------------------
        // Các phương thức hỗ trợ nội bộ
        // ------------------------------------------------------------------

        /// <summary>Tạo danh sách tham số dùng chung cho Insert/Update.</summary>
        private static SqlParameter[] TaoThamSo(SinhVien sv)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.NVarChar, 10) { Value = sv.MaSV },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 50) { Value = sv.HoTen },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 5) { Value = (object)sv.GioiTinh ?? DBNull.Value },
                new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = DoiNgaySinh(sv.NgaySinh) },
                new SqlParameter("@Diem", SqlDbType.Float) { Value = sv.Diem },
                new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 15) { Value = (object)sv.SoDienThoai ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = (object)sv.Email ?? DBNull.Value },
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = (object)sv.MaLop ?? DBNull.Value }
            };
        }

        /// <summary>Chuyển chuỗi dd/MM/yyyy thành giá trị Date (hoặc DBNull nếu để trống).</summary>
        private static object DoiNgaySinh(string chuoiNgaySinh)
        {
            if (string.IsNullOrWhiteSpace(chuoiNgaySinh))
                return DBNull.Value;

            DateTime ngaySinh;
            if (!DateTime.TryParseExact(chuoiNgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out ngaySinh))
            {
                throw new FormatException("Ngày sinh '" + chuoiNgaySinh + "' không đúng định dạng dd/MM/yyyy.");
            }

            return ngaySinh;
        }

        /// <summary>Đọc một dòng SqlDataReader thành đối tượng SinhVien.</summary>
        private static SinhVien DocDuLieu(SqlDataReader reader)
        {
            return new SinhVien
            {
                MaSV = reader["MaSV"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                GioiTinh = reader["GioiTinh"] == DBNull.Value ? "" : reader["GioiTinh"].ToString(),
                NgaySinh = reader["NgaySinh"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy"),
                Diem = reader["Diem"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Diem"]),
                SoDienThoai = reader["SoDienThoai"] == DBNull.Value ? "" : reader["SoDienThoai"].ToString(),
                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                MaLop = reader["MaLop"] == DBNull.Value ? "" : reader["MaLop"].ToString()
            };
        }

        /// <summary>Đọc một dòng DataRow thành đối tượng SinhVien.</summary>
        private static SinhVien DocDuLieu(DataRow dong)
        {
            return new SinhVien
            {
                MaSV = dong["MaSV"].ToString(),
                HoTen = dong["HoTen"].ToString(),
                GioiTinh = dong["GioiTinh"] == DBNull.Value ? "" : dong["GioiTinh"].ToString(),
                NgaySinh = dong["NgaySinh"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(dong["NgaySinh"]).ToString("dd/MM/yyyy"),
                Diem = dong["Diem"] == DBNull.Value ? 0 : Convert.ToDouble(dong["Diem"]),
                SoDienThoai = dong["SoDienThoai"] == DBNull.Value ? "" : dong["SoDienThoai"].ToString(),
                Email = dong["Email"] == DBNull.Value ? "" : dong["Email"].ToString(),
                MaLop = dong["MaLop"] == DBNull.Value ? "" : dong["MaLop"].ToString()
            };
        }
    }
}
