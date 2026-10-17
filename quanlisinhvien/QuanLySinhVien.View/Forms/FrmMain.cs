using System;
using System.Windows.Forms;

namespace QuanLySinhVien.View.Forms
{
    /// <summary>Form chính: mở các form quản lý Sinh viên / Lớp học.</summary>
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnQuanLySinhVien_Click(object sender, EventArgs e)
        {
            using (FrmSinhVien frm = new FrmSinhVien())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnQuanLyLopHoc_Click(object sender, EventArgs e)
        {
            using (FrmLopHoc frm = new FrmLopHoc())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
