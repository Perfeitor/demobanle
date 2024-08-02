using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Quayhang
{
    public string Maquay { get; set; } = null!;

    public string? Tenquay { get; set; }

    public string Madonvi { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Makhohang { get; set; }

    public string? Makhokm { get; set; }

    public string? Makhoam { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
