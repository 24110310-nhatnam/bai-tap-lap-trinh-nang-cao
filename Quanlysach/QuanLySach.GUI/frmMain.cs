using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Windows.Forms;
using QuanLySach.BUS;
using QuanLySach.Entity.Entities;

namespace QuanLySach.GUI
{
    public partial class frmMain : Form
    {
        // ===== BUS instances =====
        private readonly SachBUS _sachBUS = new SachBUS();
        private readonly TheLoaiBUS _theLoaiBUS = new TheLoaiBUS();

        // ===== Trạng thái =====
        private bool _dangNhap = false;   // đang nhập dữ liệu (chưa lưu)

        public frmMain()
        {
            InitializeComponent();
        }

        // ============================================================
        // FORM LOAD
        // ============================================================
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Seed dữ liệu mẫu nếu DB mới
            try
            {
                _theLoaiBUS.SeedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NapTheLoaiVaoComboBox();
            NapSachVaoDGV();
            SetTrangThaiNhapLieu(false);
            SetTrangThaiDongng(false);
        }

        // ============================================================
        // NẠP DỮ LIỆU
        // ============================================================
        private void NapTheLoaiVaoComboBox()
        {
            var danhSach = _theLoaiBUS.GetAll();
            cboTheLoai.DataSource = null;
            cboTheLoai.DataSource = danhSach;
            cboTheLoai.DisplayMember = "TenLoai";
            cboTheLoai.ValueMember = "MaLoai";
            cboTheLoai.SelectedIndex = -1;
        }

        private void NapSachVaoDGV()
        {
            var danhSach = _sachBUS.GetAll();
            dgvSach.DataSource = null;
            dgvSach.DataSource = danhSach;

            // Ẩn cột không cần thiết
            if (dgvSach.Columns["TheLoai"] != null)
                dgvSach.Columns["TheLoai"].Visible = false;
            if (dgvSach.Columns["MaLoai"] != null)
                dgvSach.Columns["MaLoai"].Visible = false;

            // Đặt tên hiển thị
            SetColumnHeader("MaSach",   "Mã sách");
            SetColumnHeader("TenSach",  "Tên sách");
            SetColumnHeader("NgayNhap", "Ngày nhập");
            SetColumnHeader("SoLuong",  "Số lượng");
            SetColumnHeader("DonGia",   "Đơn giá");

            // Thêm cột thể loại (tên) nếu chưa có
            if (!dgvSach.Columns.Contains("TenTheLoai"))
            {
                dgvSach.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "TenTheLoai",
                    HeaderText = "Thể loại",
                    ReadOnly = true
                });
            }

            // Điền tên thể loại vào cột TenTheLoai
            foreach (DataGridViewRow row in dgvSach.Rows)
            {
                var sach = row.DataBoundItem as Sach;
                if (sach != null && sach.TheLoai != null)
                    row.Cells["TenTheLoai"].Value = sach.TheLoai.TenLoai;
            }

            // Định dạng cột ngày
            if (dgvSach.Columns["NgayNhap"] != null)
                dgvSach.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Định dạng cột đơn giá
            if (dgvSach.Columns["DonGia"] != null)
                dgvSach.Columns["DonGia"].DefaultCellStyle.Format = "N0";

            dgvSach.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void SetColumnHeader(string colName, string header)
        {
            if (dgvSach.Columns[colName] != null)
                dgvSach.Columns[colName].HeaderText = header;
        }

        // ============================================================
        // TRẠNG THÁI BUTTONS
        // ============================================================
        /// <summary>
        /// Cấu hình button khi đang nhập liệu (Thêm/Sửa active) hay không
        /// </summary>
        private void SetTrangThaiNhapLieu(bool dangNhap)
        {
            _dangNhap = dangNhap;
        }

        /// <summary>
        /// true = đang chọn 1 dòng (Sửa/Xóa active), false = không chọn (Thêm active)
        /// </summary>
        private void SetTrangThaiDongng(bool dangChon)
        {
            if (dangChon)
            {
                btnThem.Enabled = false;
                btnSua.Enabled  = true;
                btnXoa.Enabled  = true;
                txtMaSach.ReadOnly = true;
            }
            else
            {
                btnThem.Enabled = true;
                btnSua.Enabled  = false;
                btnXoa.Enabled  = false;
                txtMaSach.ReadOnly = false;
            }
        }

        // ============================================================
        // ĐIỀN THÔNG TIN VÀO FORM
        // ============================================================
        private void DiemThongTinVaoForm(Sach sach)
        {
            if (sach == null) return;
            txtMaSach.Text  = sach.MaSach;
            txtTenSach.Text = sach.TenSach;
            txtSoLuong.Text = sach.SoLuong.ToString();
            txtDonGia.Text  = sach.DonGia.ToString("N0");
            dtpNgayNhap.Value = sach.NgayNhap;
            cboTheLoai.SelectedValue = sach.MaLoai;
        }

        private void XoaForm()
        {
            txtMaSach.Text  = string.Empty;
            txtTenSach.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtDonGia.Text  = string.Empty;
            dtpNgayNhap.Value = DateTime.Now;
            cboTheLoai.SelectedIndex = -1;
        }

        // ============================================================
        // SỰ KIỆN CHỌN DÒNG TRONG DGV
        // ============================================================
        private void dgvSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSach.CurrentRow == null) return;
            var sach = dgvSach.CurrentRow.DataBoundItem as Sach;
            if (sach == null) return;

            DiemThongTinVaoForm(sach);
            SetTrangThaiDongng(true);   // Bật Sửa/Xóa, tắt Thêm
            SetTrangThaiNhapLieu(false);
        }

        // ============================================================
        // NÚT MỚI
        // ============================================================
        private void btnMoi_Click(object sender, EventArgs e)
        {
            XoaForm();
            SetTrangThaiDongng(false);    // Bật Thêm, tắt Sửa/Xóa
            SetTrangThaiNhapLieu(true);   // Đánh dấu đang nhập
            dgvSach.ClearSelection();
            txtMaSach.Focus();
        }

        // ============================================================
        // NÚT THÊM
        // ============================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSach    = txtMaSach.Text;
            string tenSach   = txtTenSach.Text;
            string maLoai    = cboTheLoai.SelectedValue as string;
            string soLuongStr = txtSoLuong.Text;
            string donGiaStr  = txtDonGia.Text.Replace(",", "").Replace(".", "");
            DateTime ngayNhap = dtpNgayNhap.Value;

            string errorMsg;
            if (!_sachBUS.Validate(maSach, tenSach, maLoai, ngayNhap,
                    soLuongStr, donGiaStr, true, out errorMsg))
            {
                MessageBox.Show(errorMsg, "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = int.Parse(soLuongStr);
            decimal donGia = decimal.Parse(donGiaStr);

            if (_sachBUS.ThemSach(maSach, tenSach, maLoai, ngayNhap, soLuong, donGia, out errorMsg))
            {
                MessageBox.Show("Thêm sách thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapSachVaoDGV();
                XoaForm();
                SetTrangThaiDongng(false);
                SetTrangThaiNhapLieu(false);
            }
            else
            {
                MessageBox.Show("Thêm thất bại: " + errorMsg, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // NÚT SỬA
        // ============================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSach     = txtMaSach.Text;
            string tenSach    = txtTenSach.Text;
            string maLoai     = cboTheLoai.SelectedValue as string;
            string soLuongStr = txtSoLuong.Text;
            string donGiaStr  = txtDonGia.Text.Replace(",", "").Replace(".", "");
            DateTime ngayNhap = dtpNgayNhap.Value;

            string errorMsg;
            if (!_sachBUS.Validate(maSach, tenSach, maLoai, ngayNhap,
                    soLuongStr, donGiaStr, false, out errorMsg))
            {
                MessageBox.Show(errorMsg, "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = int.Parse(soLuongStr);
            decimal donGia = decimal.Parse(donGiaStr);

            if (_sachBUS.SuaSach(maSach, tenSach, maLoai, ngayNhap, soLuong, donGia, out errorMsg))
            {
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapSachVaoDGV();
                SetTrangThaiNhapLieu(false);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại: " + errorMsg, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // NÚT XÓA
        // ============================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text.Trim();
            if (string.IsNullOrEmpty(maSach))
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa sách '{0}'?", maSach),
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            string errorMsg;
            if (_sachBUS.XoaSach(maSach, out errorMsg))
            {
                MessageBox.Show("Xóa sách thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapSachVaoDGV();
                XoaForm();
                SetTrangThaiDongng(false);
                SetTrangThaiNhapLieu(false);
            }
            else
            {
                MessageBox.Show("Xóa thất bại: " + errorMsg, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // NÚT THOÁT
        // ============================================================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (_dangNhap)
            {
                var confirm = MessageBox.Show(
                    "Bạn đang nhập dữ liệu chưa lưu. Bạn có chắc chắn muốn thoát?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }
            else
            {
                var confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát chương trình?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }
            Application.Exit();
        }

        // ============================================================
        // ENTER = Thêm, ESC = Thoát
        // ============================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (btnThem.Enabled)
                {
                    btnThem_Click(this, EventArgs.Empty);
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                btnThoat_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Đánh dấu đang nhập khi người dùng gõ vào các TextBox
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            _dangNhap = true;
        }
    }
}
