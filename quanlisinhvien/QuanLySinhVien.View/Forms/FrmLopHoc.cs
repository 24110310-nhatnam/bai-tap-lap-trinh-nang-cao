using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLySinhVien.Business;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.View.Utilities;

namespace QuanLySinhVien.View.Forms
{
    /// <summary>
    /// Form quản lý lớp học. Tầng View CHỈ:
    ///   - Lấy dữ liệu từ điều khiển trên form;
    ///   - Gọi tầng Business;
    ///   - Bọc mọi lời gọi trong try/catch và hiển thị ngoại lệ cho người dùng (LoiHelper).
    /// </summary>
    public partial class FrmLopHoc : Form
    {
        private readonly LopHocBUS lopHocBUS = new LopHocBUS();

        public FrmLopHoc()
        {
            InitializeComponent();
        }

        private void FrmLopHoc_Load(object sender, EventArgs e)
        {
            try
            {
                TaiDanhSachLopHoc();
                LamTrongForm();
            }
            catch (Exception ex)
            {
                // Tầng View xử lý ngoại lệ do tầng Business ném lên
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        // ------------------------------------------------------------------
        // Nạp dữ liệu (có thể ném ngoại lệ, handler sẽ bắt)
        // ------------------------------------------------------------------

        private void TaiDanhSachLopHoc()
        {
            List<LopHoc> danhSach = lopHocBUS.GetAllLopHoc();

            dgvLopHoc.DataSource = null;
            dgvLopHoc.DataSource = danhSach;
        }

        private LopHoc LayDuLieuTuForm()
        {
            return new LopHoc(txtMaLop.Text.Trim(), txtTenLop.Text.Trim());
        }

        private void LamTrongForm()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtMaLop.ReadOnly = false;
            dgvLopHoc.ClearSelection();
            txtMaLop.Focus();
        }

        // ------------------------------------------------------------------
        // Các sự kiện - mọi lời gọi tầng Business đều được bọc try/catch
        // ------------------------------------------------------------------

        private void dgvLopHoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                LopHoc lop = dgvLopHoc.CurrentRow == null
                    ? null
                    : dgvLopHoc.CurrentRow.DataBoundItem as LopHoc;

                if (lop == null)
                    return;

                txtMaLop.Text = lop.MaLop;
                txtTenLop.Text = lop.TenLop;
                txtMaLop.ReadOnly = true; // Mã lớp là khóa chính, không cho sửa khi cập nhật
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                LopHoc lop = LayDuLieuTuForm();
                lopHocBUS.AddLopHoc(lop);

                MessageBox.Show(this, "Thêm lớp học thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachLopHoc();
                LamTrongForm();
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                LopHoc lop = LayDuLieuTuForm();
                lopHocBUS.UpdateLopHoc(lop);

                MessageBox.Show(this, "Cập nhật lớp học thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachLopHoc();
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maLop = txtMaLop.Text.Trim();
                if (string.IsNullOrWhiteSpace(maLop))
                    throw new ArgumentException("Chọn lớp cần xóa trên lưới hoặc nhập mã lớp.");

                DialogResult traLoi = MessageBox.Show(this,
                    "Bạn có chắc chắn muốn xóa lớp '" + maLop + "'?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (traLoi != DialogResult.Yes)
                    return;

                lopHocBUS.DeleteLopHoc(maLop);

                MessageBox.Show(this, "Xóa lớp học thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachLopHoc();
                LamTrongForm();
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnLamTrong_Click(object sender, EventArgs e)
        {
            LamTrongForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
