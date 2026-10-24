using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTV.Models;

public class Sach
{
    [Key]
    public int SachId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sách.")]
    [StringLength(200)]
    [Display(Name = "Tên sách")]
    public string TenSach { get; set; } = string.Empty;

    [Range(0, 2100, ErrorMessage = "Năm xuất bản không hợp lệ.")]
    [Display(Name = "Năm xuất bản")]
    public int NamXuatBan { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn tác giả.")]
    [Display(Name = "Tác giả")]
    public int TacGiaId { get; set; }

    [ForeignKey(nameof(TacGiaId))]
    public TacGia? TacGia { get; set; }
}
