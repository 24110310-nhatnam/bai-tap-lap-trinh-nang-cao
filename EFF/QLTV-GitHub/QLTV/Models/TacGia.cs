using System.ComponentModel.DataAnnotations;

namespace QLTV.Models;

public class TacGia
{
    [Key]
    public int TacGiaId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên tác giả.")]
    [StringLength(100)]
    [Display(Name = "Tên tác giả")]
    public string TenTacGia { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Quốc tịch")]
    public string? QuocTich { get; set; }

    public ICollection<Sach> SachList { get; set; } = new List<Sach>();
}
