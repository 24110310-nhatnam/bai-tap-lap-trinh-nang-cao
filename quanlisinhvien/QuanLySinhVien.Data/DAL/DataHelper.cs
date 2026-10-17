using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Lớp tiện ích thao tác cơ sở dữ liệu bằng ADO.NET (Microsoft.Data.SqlClient).
    /// Mọi phương thức khi gặp lỗi đều NÉM NGOẠI LỆ ra ngoài (kèm InnerException) để tầng gọi xử lý.
    /// </summary>
    public class DataHelper
    {
        // Chuỗi kết nối mặc định (giống mã nguồn cũ). Có thể ghi đè trong App.config tầng View với tên "QLSV".
        private const string ChuoiKetNoiMacDinh =
            @"Server=LAPTOP-6SN65NCS\HUECTMDB;Database=QLSV;Integrated Security=True;TrustServerCertificate=True";

        private readonly string connectionString;

        public DataHelper()
        {
            // Ưu tiên đọc chuỗi kết nối từ App.config; nếu không có thì dùng mặc định.
            ConnectionStringSettings cauHinh = ConfigurationManager.ConnectionStrings["QLSV"];
            connectionString = (cauHinh != null && !string.IsNullOrWhiteSpace(cauHinh.ConnectionString))
                ? cauHinh.ConnectionString
                : ChuoiKetNoiMacDinh;
        }

        public DataHelper(string chuoiKetNoi)
        {
            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
                throw new ArgumentException("Chuỗi kết nối không được để trống.", nameof(chuoiKetNoi));

            connectionString = chuoiKetNoi;
        }

        /// <summary>Mở kết nối tới SQL Server. Ném ngoại lệ nếu không kết nối được.</summary>
        public SqlConnection OpenConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                con.Dispose();
                throw new Exception("Không thể kết nối tới cơ sở dữ liệu. " + ex.Message, ex);
            }
        }

        /// <summary>Thực thi câu lệnh INSERT/UPDATE/DELETE, trả về số dòng bị ảnh hưởng.</summary>
        public int ExecuteNonQuery(string sql, params SqlParameter[] thamSo)
        {
            try
            {
                using (SqlConnection con = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (thamSo != null && thamSo.Length > 0)
                        cmd.Parameters.AddRange(thamSo);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi câu lệnh thêm/sửa/xóa dữ liệu. " + ex.Message, ex);
            }
        }

        /// <summary>Thực thi câu lệnh trả về một giá trị duy nhất (COUNT, SUM...).</summary>
        public object ExecuteScalar(string sql, params SqlParameter[] thamSo)
        {
            try
            {
                using (SqlConnection con = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (thamSo != null && thamSo.Length > 0)
                        cmd.Parameters.AddRange(thamSo);

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi câu lệnh truy vấn một giá trị. " + ex.Message, ex);
            }
        }

        /// <summary>Đọc dữ liệu vào DataTable bằng SqlDataAdapter.</summary>
        public DataTable GetDataTable(string sql, params SqlParameter[] thamSo)
        {
            try
            {
                using (SqlConnection con = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (thamSo != null && thamSo.Length > 0)
                        cmd.Parameters.AddRange(thamSo);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đọc dữ liệu. " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Đọc dữ liệu bằng SqlDataReader.
        /// Người gọi phải dùng khối using để đóng reader (nhờ CommandBehavior.CloseConnection
        /// kết nối cũng được đóng theo).
        /// </summary>
        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] thamSo)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                if (thamSo != null && thamSo.Length > 0)
                    cmd.Parameters.AddRange(thamSo);

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (con != null)
                    con.Dispose();

                throw new Exception("Lỗi khi đọc dữ liệu. " + ex.Message, ex);
            }
        }
    }
}
