using System;
using System.Windows.Forms;
using QuanLySinhVien.View.Forms;

namespace QuanLySinhVien.View
{
    internal static class Program
    {
        /// <summary>Điểm khởi chạy của ứng dụng WinForms.</summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
