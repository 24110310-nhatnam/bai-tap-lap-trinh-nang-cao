using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using QuanLySinhVien.Business;
using QuanLySinhVien.Data.Entity;
using QuanLySinhVien.View.Utilities;

namespace QuanLySinhVien.View.Forms
{
    /// <summary>
    /// Form quản lý sinh viên. Tầng View CHỈ:
    ///   - Lấy dữ liệu từ điều khiển trên form;
    ///   - Gọi tầng Business;
    ///   - Bọc mọi lời gọi trong try/catch và hiển thị ngoại lệ cho người dùng (LoiHelper).
    /// </summary>
    public partial class FrmSinhVien : Form
    {
        private readonly SinhVienBUS sinhVienBUS = new SinhVienBUS();
        private readonly LopHocBUS lopHocBUS = new LopHocBUS();

        public FrmSinhVien()
        {
            InitializeComponent();
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                TaiDanhSachLop();
                TaiDanhSachSinhVien();
                LamTrongForm();
            }
            catch (Exception ex)
            {
                // Tầng View xử lý ngoại lệ do tầng Business ném lên
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        // ------------------------------------------------------------------
        // Nạp dữ liệu (các phương thức này có thể ném ngoại lệ, handler sẽ bắt)
        // ------------------------------------------------------------------

        private void TaiDanhSachLop()
        {
            List<LopHoc> danhSachLop = lopHocBUS.GetAllLopHoc();

            cboMaLop.DisplayMember = "HienThi";
            cboMaLop.ValueMember = "MaLop";
            cboMaLop.DataSource = null;
            cboMaLop.DataSource = danhSachLop;
        }

        private void TaiDanhSachSinhVien()
        {
            List<SinhVien> danhSach = sinhVienBUS.GetAllSinhVien();

            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = danhSach;
        }

        /// <summary>Lấy dữ liệu người dùng nhập trên form. Ném ArgumentException nếu điểm không hợp lệ.</summary>
        private SinhVien LayDuLieuTuForm()
        {
            double diem;
            string chuoiDiem = txtDiem.Text.Trim().Replace(',', '.');
            if (!double.TryParse(chuoiDiem, NumberStyles.Float, CultureInfo.InvariantCulture, out diem))
            {
                throw new ArgumentException("Điểm phải là một số (ví dụ: 8.5).");
            }

            return new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                NgaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                Diem = diem,
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                MaLop = cboMaLop.SelectedValue as string
            };
        }

        private void LamTrongForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiem.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtTim.Clear();
            cboGioiTinh.SelectedIndex = -1;
            if (cboMaLop.Items.Count > 0)
                cboMaLop.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            dgvSinhVien.ClearSelection();
            txtMaSV.Focus();
        }

        // ------------------------------------------------------------------
        // Các sự kiện - mọi lời gọi tầng Business đều được bọc try/catch
        // ------------------------------------------------------------------

        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SinhVien sv = dgvSinhVien.CurrentRow == null
                    ? null
                    : dgvSinhVien.CurrentRow.DataBoundItem as SinhVien;

                if (sv == null)
                    return;

                txtMaSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                cboGioiTinh.Text = sv.GioiTinh;

                DateTime ngaySinh;
                if (!string.IsNullOrWhiteSpace(sv.NgaySinh) &&
                    DateTime.TryParseExact(sv.NgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }

                txtDiem.Text = sv.Diem.ToString(CultureInfo.InvariantCulture);
                txtSoDienThoai.Text = sv.SoDienThoai;
                txtEmail.Text = sv.Email;
                cboMaLop.SelectedValue = sv.MaLop;
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
                SinhVien sv = LayDuLieuTuForm();
                sinhVienBUS.AddSinhVien(sv);

                MessageBox.Show(this, "Thêm sinh viên thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachSinhVien();
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
                SinhVien sv = LayDuLieuTuForm();
                sinhVienBUS.UpdateSinhVien(sv);

                MessageBox.Show(this, "Cập nhật sinh viên thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachSinhVien();
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
                string maSV = txtMaSV.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                    throw new ArgumentException("Chọn sinh viên cần xóa trên lưới hoặc nhập mã sinh viên.");

                DialogResult traLoi = MessageBox.Show(this,
                    "Bạn có chắc chắn muốn xóa sinh viên '" + maSV + "'?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (traLoi != DialogResult.Yes)
                    return;

                sinhVienBUS.DeleteSinhVien(maSV);

                MessageBox.Show(this, "Xóa sinh viên thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSachSinhVien();
                LamTrongForm();
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtTim.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                    throw new ArgumentException("Nhập mã sinh viên cần tìm.");

                SinhVien sv = sinhVienBUS.GetSinhVienByMaSV(maSV);

                List<SinhVien> ketQua = new List<SinhVien>();
                if (sv != null)
                    ketQua.Add(sv);

                dgvSinhVien.DataSource = null;
                dgvSinhVien.DataSource = ketQua;

                if (sv == null)
                {
                    MessageBox.Show(this, "Không tìm thấy sinh viên '" + maSV + "'.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LoiHelper.HienThiLoi(this, ex);
            }
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                TaiDanhSachSinhVien();
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
