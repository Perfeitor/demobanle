using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomhang
{
    public string Manhomhang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tennhomhang { get; set; }

    public string? Manganh { get; set; }

    public string? Tendangnhap { get; set; }

    public DateTime? Ngaytao { get; set; }

    public int? Madongbo { get; set; }

    public int? Trangthaidoanhso { get; set; }

    public decimal? Tylecpvc { get; set; }

    public int? Thutuin { get; set; }

    public int? Trangthaidoanhsonam { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<Kiemkect> Kiemkects { get; set; } = new List<Kiemkect>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    public virtual ICollection<Mathangthue> Mathangthues { get; set; } = new List<Mathangthue>();

    public virtual Nganhhang? Nganhhang { get; set; }

    public virtual Nguoidung? Nguoidung { get; set; }
}
