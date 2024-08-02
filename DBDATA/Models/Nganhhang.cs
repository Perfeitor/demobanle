using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nganhhang
{
    public string Manganh { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tennganh { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public int? Madongbo { get; set; }

    public int? Loai { get; set; }

    public decimal? Trongsodathang { get; set; }

    public virtual ICollection<Kiemkect> Kiemkects { get; set; } = new List<Kiemkect>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    public virtual ICollection<Mathangthue> Mathangthues { get; set; } = new List<Mathangthue>();

    public virtual ICollection<Nhomhang> Nhomhangs { get; set; } = new List<Nhomhang>();
}
