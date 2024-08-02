using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsNgaynghi
{
    public string Mangaynghi { get; set; } = null!;

    public string Mabophan { get; set; } = null!;

    public DateTime? Tungay { get; set; }

    public DateTime? Denngay { get; set; }

    public decimal? Songaynghi { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual NsBophan NsBophan { get; set; } = null!;
}
