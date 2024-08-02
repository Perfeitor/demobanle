using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtDinhmucthuexe
{
    public string Maxe { get; set; } = null!;

    public int? SoKmtrenngay { get; set; }

    public decimal? SotienvuottrenKm { get; set; }

    public decimal? Sotienvuottrengio { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
