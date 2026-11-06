using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.Entity.Entities
{
    [Table("Sach")]
    public class Sach
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Column("MaSach")]
        public string MaSach { get; set; }

        [Required]
        [StringLength(150)]
        [Column("TenSach")]
        public string TenSach { get; set; }

        [Column("NgayNhap")]
        public DateTime NgayNhap { get; set; }

        [Column("SoLuong")]
        public int SoLuong { get; set; }

        [Column("DonGia", TypeName = "decimal")]
        [DecimalPrecision(18, 2)]
        public decimal DonGia { get; set; }

        [Required]
        [StringLength(10)]
        [Column("MaLoai")]
        public string MaLoai { get; set; }

        // Navigation property
        [ForeignKey("MaLoai")]
        public virtual TheLoai TheLoai { get; set; }
    }

    /// <summary>
    /// Custom attribute to specify decimal precision for EF Fluent API usage
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }
    }
}
