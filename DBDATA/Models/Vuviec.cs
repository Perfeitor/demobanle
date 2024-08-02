using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Vuviec
{
    public string Mavuviec { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tenvuviec { get; set; }

    public string Manhomvuviec { get; set; } = null!;

    public int? Trangthai { get; set; }

    public int? Trangthaisudung { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhomvuviec Nhomvuviec { get; set; } = null!;
}
