namespace QuanLySinhVien.View.Forms
{
    partial class FrmMain
    {
        /// <summary>Biến thiết kế.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Giải phóng tài nguyên đang dùng.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnQuanLySinhVien = new System.Windows.Forms.Button();
            this.btnQuanLyLopHoc = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(12, 25);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(496, 40);
            this.lblTieuDe.TabIndex = 3;
            this.lblTieuDe.Text = "ỨNG DỤNG QUẢN LÝ SINH VIÊN (ADO.NET)";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnQuanLySinhVien
            // 
            this.btnQuanLySinhVien.Location = new System.Drawing.Point(60, 95);
            this.btnQuanLySinhVien.Name = "btnQuanLySinhVien";
            this.btnQuanLySinhVien.Size = new System.Drawing.Size(180, 60);
            this.btnQuanLySinhVien.TabIndex = 0;
            this.btnQuanLySinhVien.Text = "Quản lý Sinh viên";
            this.btnQuanLySinhVien.UseVisualStyleBackColor = true;
            this.btnQuanLySinhVien.Click += new System.EventHandler(this.btnQuanLySinhVien_Click);
            // 
            // btnQuanLyLopHoc
            // 
            this.btnQuanLyLopHoc.Location = new System.Drawing.Point(280, 95);
            this.btnQuanLyLopHoc.Name = "btnQuanLyLopHoc";
            this.btnQuanLyLopHoc.Size = new System.Drawing.Size(180, 60);
            this.btnQuanLyLopHoc.TabIndex = 1;
            this.btnQuanLyLopHoc.Text = "Quản lý Lớp học";
            this.btnQuanLyLopHoc.UseVisualStyleBackColor = true;
            this.btnQuanLyLopHoc.Click += new System.EventHandler(this.btnQuanLyLopHoc_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(170, 180);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(180, 40);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 245);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnQuanLyLopHoc);
            this.Controls.Add(this.btnQuanLySinhVien);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Sinh viên - Trang chính";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnQuanLySinhVien;
        private System.Windows.Forms.Button btnQuanLyLopHoc;
        private System.Windows.Forms.Button btnThoat;
    }
}
