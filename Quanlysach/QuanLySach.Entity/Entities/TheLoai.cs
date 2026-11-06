using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.Entity.Entities
{
    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        [Required]
        [StringLength(10)]
        [Column("MaLoai")]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TenLoai")]
        public string TenLoai { get; set; }

        // Navigation property
        public virtual ICollection<Sach> DanhSachSach { get; set; }

        public TheLoai()
        {
            DanhSachSach = new HashSet<Sach>();
        }

        public override string ToString()
        {
            return TenLoai;
        }
    }
}
