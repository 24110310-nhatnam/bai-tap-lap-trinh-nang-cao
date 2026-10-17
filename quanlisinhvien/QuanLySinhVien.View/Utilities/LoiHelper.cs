using System;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien.View.Utilities
{
    /// <summary>
    /// Tiện ích hiển thị ngoại lệ của tầng View.
    /// In ra toàn bộ chuỗi ngoại lệ lồng nhau (DAL -> Business) để dễ tìm nguyên nhân.
    /// </summary>
    public static class LoiHelper
    {
        /// <summary>Hiển thị ngoại lệ (và các InnerException) trong một MessageBox.</summary>
        public static void HienThiLoi(IWin32Window cha, Exception ex)
        {
            if (ex == null)
                return;

            StringBuilder chuoi = new StringBuilder();
            Exception hienTai = ex;
            while (hienTai != null)
            {
                chuoi.AppendLine("- " + hienTai.Message);
                hienTai = hienTai.InnerException;
            }

            MessageBox.Show(cha, chuoi.ToString().TrimEnd(),
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
