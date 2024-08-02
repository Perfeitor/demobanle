using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsChucvu
{
    public string Machucvu { get; set; } = null!;

    public string? Tenchucvu { get; set; }

    public int? Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
