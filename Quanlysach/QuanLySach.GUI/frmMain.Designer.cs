namespace QuanLySach.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.lblMaSach = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnMoi = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.dgvSach = new System.Windows.Forms.DataGridView();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();

            this.grpThongTin.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // ===== FORM =====
            this.Text = "Quản lý sách - SV001 - Nguyễn Văn A";
            this.Name = "frmMain";
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Load += new System.EventHandler(this.frmMain_Load);

            // ===== grpThongTin =====
            this.grpThongTin.Text = "Thông tin sách";
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Size = new System.Drawing.Size(876, 180);
            this.grpThongTin.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.grpThongTin.TabIndex = 0;

            // Row 1: Thể loại | Mã sách
            this.lblTheLoai.Text = "Thể loại:";
            this.lblTheLoai.Location = new System.Drawing.Point(15, 30);
            this.lblTheLoai.Size = new System.Drawing.Size(80, 23);
            this.lblTheLoai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cboTheLoai.Location = new System.Drawing.Point(100, 28);
            this.cboTheLoai.Size = new System.Drawing.Size(220, 23);
            this.cboTheLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheLoai.TabIndex = 0;

            this.lblMaSach.Text = "Mã sách:";
            this.lblMaSach.Location = new System.Drawing.Point(345, 30);
            this.lblMaSach.Size = new System.Drawing.Size(70, 23);
            this.lblMaSach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtMaSach.Location = new System.Drawing.Point(420, 28);
            this.txtMaSach.Size = new System.Drawing.Size(180, 23);
            this.txtMaSach.MaxLength = 20;
            this.txtMaSach.TabIndex = 1;
            this.txtMaSach.TextChanged += new System.EventHandler(this.txtInput_TextChanged);

            // Row 2: Tên sách
            this.lblTenSach.Text = "Tên sách:";
            this.lblTenSach.Location = new System.Drawing.Point(15, 70);
            this.lblTenSach.Size = new System.Drawing.Size(80, 23);
            this.lblTenSach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtTenSach.Location = new System.Drawing.Point(100, 68);
            this.txtTenSach.Size = new System.Drawing.Size(500, 23);
            this.txtTenSach.MaxLength = 150;
            this.txtTenSach.TabIndex = 2;
            this.txtTenSach.TextChanged += new System.EventHandler(this.txtInput_TextChanged);

            // Row 3: Ngày nhập | Số lượng | Đơn giá
            this.lblNgayNhap.Text = "Ngày nhập:";
            this.lblNgayNhap.Location = new System.Drawing.Point(15, 110);
            this.lblNgayNhap.Size = new System.Drawing.Size(80, 23);
            this.lblNgayNhap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.dtpNgayNhap.Location = new System.Drawing.Point(100, 108);
            this.dtpNgayNhap.Size = new System.Drawing.Size(160, 23);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.TabIndex = 3;

            this.lblSoLuong.Text = "Số lượng:";
            this.lblSoLuong.Location = new System.Drawing.Point(280, 110);
            this.lblSoLuong.Size = new System.Drawing.Size(70, 23);
            this.lblSoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtSoLuong.Location = new System.Drawing.Point(355, 108);
            this.txtSoLuong.Size = new System.Drawing.Size(100, 23);
            this.txtSoLuong.MaxLength = 10;
            this.txtSoLuong.TabIndex = 4;
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtInput_TextChanged);

            this.lblDonGia.Text = "Đơn giá:";
            this.lblDonGia.Location = new System.Drawing.Point(475, 110);
            this.lblDonGia.Size = new System.Drawing.Size(65, 23);
            this.lblDonGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtDonGia.Location = new System.Drawing.Point(545, 108);
            this.txtDonGia.Size = new System.Drawing.Size(130, 23);
            this.txtDonGia.MaxLength = 20;
            this.txtDonGia.TabIndex = 5;
            this.txtDonGia.TextChanged += new System.EventHandler(this.txtInput_TextChanged);

            // Add controls to grpThongTin
            this.grpThongTin.Controls.Add(this.lblTheLoai);
            this.grpThongTin.Controls.Add(this.cboTheLoai);
            this.grpThongTin.Controls.Add(this.lblMaSach);
            this.grpThongTin.Controls.Add(this.txtMaSach);
            this.grpThongTin.Controls.Add(this.lblTenSach);
            this.grpThongTin.Controls.Add(this.txtTenSach);
            this.grpThongTin.Controls.Add(this.lblNgayNhap);
            this.grpThongTin.Controls.Add(this.dtpNgayNhap);
            this.grpThongTin.Controls.Add(this.lblSoLuong);
            this.grpThongTin.Controls.Add(this.txtSoLuong);
            this.grpThongTin.Controls.Add(this.lblDonGia);
            this.grpThongTin.Controls.Add(this.txtDonGia);

            // ===== pnlButtons =====
            this.pnlButtons.Location = new System.Drawing.Point(12, 200);
            this.pnlButtons.Size = new System.Drawing.Size(876, 44);
            this.pnlButtons.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.pnlButtons.TabIndex = 1;

            // btnMoi
            this.btnMoi.Text = "&Mới";
            this.btnMoi.Location = new System.Drawing.Point(0, 6);
            this.btnMoi.Size = new System.Drawing.Size(90, 32);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);

            // btnThem
            this.btnThem.Text = "&Thêm";
            this.btnThem.Location = new System.Drawing.Point(100, 6);
            this.btnThem.Size = new System.Drawing.Size(90, 32);
            this.btnThem.TabIndex = 1;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // btnSua
            this.btnSua.Text = "&Sửa";
            this.btnSua.Location = new System.Drawing.Point(200, 6);
            this.btnSua.Size = new System.Drawing.Size(90, 32);
            this.btnSua.TabIndex = 2;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // btnXoa
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.Location = new System.Drawing.Point(300, 6);
            this.btnXoa.Size = new System.Drawing.Size(90, 32);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            // btnThoat
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Location = new System.Drawing.Point(400, 6);
            this.btnThoat.Size = new System.Drawing.Size(90, 32);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            this.pnlButtons.Controls.Add(this.btnMoi);
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Controls.Add(this.btnThoat);

            // ===== grpDanhSach =====
            this.grpDanhSach.Text = "Danh sách sách";
            this.grpDanhSach.Location = new System.Drawing.Point(12, 252);
            this.grpDanhSach.Size = new System.Drawing.Size(876, 350);
            this.grpDanhSach.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.grpDanhSach.TabIndex = 2;

            // ===== dgvSach =====
            this.dgvSach.Location = new System.Drawing.Point(10, 22);
            this.dgvSach.Size = new System.Drawing.Size(856, 318);
            this.dgvSach.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.dgvSach.TabIndex = 0;
            this.dgvSach.AllowUserToAddRows = false;
            this.dgvSach.AllowUserToDeleteRows = false;
            this.dgvSach.ReadOnly = true;
            this.dgvSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSach.MultiSelect = false;
            this.dgvSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSach.RowHeadersVisible = false;
            this.dgvSach.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvSach.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSach.SelectionChanged += new System.EventHandler(this.dgvSach_SelectionChanged);

            this.grpDanhSach.Controls.Add(this.dgvSach);

            // ===== Add controls to Form =====
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpDanhSach);

            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            this.grpDanhSach.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Controls
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnMoi;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.GroupBox grpDanhSach;
    }
}
