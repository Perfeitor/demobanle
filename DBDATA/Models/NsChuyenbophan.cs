using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsChuyenbophan
{
    public string Machuyenbophan { get; set; } = null!;

    public string? Tenchuyenbophan { get; set; }

    public string? Manhanvien { get; set; }

    public string? Mabophan { get; set; }

    public DateTime? Tungay { get; set; }

    public DateTime? Denngay { get; set; }

    public int? Giovao { get; set; }

    public int? Giove { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string Madonvi { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual NsBophan? NsBophan { get; set; }
}
