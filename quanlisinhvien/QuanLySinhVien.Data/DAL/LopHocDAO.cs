using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng truy xuất dữ liệu cho bảng LopHoc, thao tác hoàn toàn bằng ADO.NET
    /// (SqlConnection, SqlCommand, SqlParameter, SqlDataReader, ExecuteScalar).
    /// YÊU CẦU: mọi phương thức đều ném ngoại lệ ra ngoài khi xảy ra lỗi để tầng Business xử lý.
    /// </summary>
    public class LopHocDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        /// <summary>
        /// Lấy danh sách lớp học kèm số sinh viên của mỗi lớp
        /// (đọc bằng SqlDataReader, có JOIN + GROUP BY).
        /// </summary>
        public List<LopHoc> GetAll()
        {
            try
            {
                const string sql =
                    "SELECT lh.MaLop, lh.TenLop, COUNT(sv.MaSV) AS SoSinhVien " +
                    "FROM LopHoc lh LEFT JOIN SinhVien sv ON sv.MaLop = lh.MaLop " +
                    "GROUP BY lh.MaLop, lh.TenLop " +
                    "ORDER BY lh.MaLop";

                List<LopHoc> danhSach = new List<LopHoc>();
                using (SqlDataReader reader = dataHelper.ExecuteReader(sql))
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new LopHoc
                        {
                            MaLop = reader["MaLop"].ToString(),
                            TenLop = reader["TenLop"].ToString(),
                            SoSinhVien = Convert.ToInt32(reader["SoSinhVien"])
                        });
                    }
                }
                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không lấy được danh sách lớp học. " + ex.Message, ex);
            }
        }

        /// <summary>Tìm lớp học theo mã. Trả về null nếu không tồn tại, ném ngoại lệ nếu có lỗi.</summary>
        public LopHoc GetByMaLop(string maLop)
        {
            try
            {
                const string sql = "SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop";

                using (SqlDataReader reader = dataHelper.ExecuteReader(sql,
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = maLop }))
                {
                    if (reader.Read())
                    {
                        return new LopHoc
                        {
                            MaLop = reader["MaLop"].ToString(),
                            TenLop = reader["TenLop"].ToString()
                        };
                    }

                    return null; // không tìm thấy
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không tìm được lớp có mã '" + maLop + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Kiểm tra mã lớp đã tồn tại chưa (dùng ExecuteScalar).</summary>
        public bool Exists(string maLop)
        {
            try
            {
                object ketQua = dataHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = maLop });

                return Convert.ToInt32(ketQua) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: không kiểm tra được lớp '" + maLop + "'. " + ex.Message, ex);
            }
        }

        /// <summary>Thêm lớp học mới (SqlParameter chống SQL Injection).</summary>
        public void Insert(LopHoc lop)
        {
            try
            {
                const string sql = "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)";

                dataHelper.ExecuteNonQuery(sql,
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = lop.MaLop },
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 50) { Value = lop.TenLop });
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng dữ liệu: thêm lớp '" + lop.MaLop + "' thất bại. " + ex.Message, ex);
            }
        }

        /// <summary>Cập nhật tên lớp theo mã (MaLop là khóa chính nên không đổi).</summary>
        public void Update(LopHoc lop)
        {
            try
            {
                const string sql = "UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop";

                int soDongAnhHuong = dataHelper.ExecuteNonQuery(sql,
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = lop.MaLop },
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 50) { Value = lop.TenLop });

                if (soDongAnhHuong == 0)
                    throw new Exception("Không tìm thấy lớp '" + lop.MaLop + "' trong cơ sở dữ liệu để cập nhật.");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Không tìm thấy", StringComparison.Ordinal))
                    throw;

                throw new Exception("Lỗi tầng dữ liệu: cập nhật lớp '" + lop.MaLop + "' thất bại. " + ex.Message, ex);
            }
        }

        /// <summary>Xóa lớp học theo mã (xóa thật trong cơ sở dữ liệu).</summary>
        public void Delete(string maLop)
        {
            try
            {
                int soDongAnhHuong = dataHelper.ExecuteNonQuery(
                    "DELETE FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 10) { Value = maLop });

                if (soDongAnhHuong == 0)
                    throw new Exception("Không tìm thấy lớp '" + maLop + "' trong cơ sở dữ liệu để xóa.");
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Không tìm thấy", StringComparison.Ordinal))
                    throw;

                throw new Exception("Lỗi tầng dữ liệu: xóa lớp '" + maLop + "' thất bại. " + ex.Message, ex);
            }
        }
    }
}
