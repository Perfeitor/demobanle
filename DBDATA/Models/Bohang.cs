using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Bohang
{
    public string Mabohang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tenbo { get; set; } = null!;

    public string Tendangnhapsua { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public int? Trangthai { get; set; }

    public string? Ghichu { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Barcode { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public virtual ICollection<Bohangct> Bohangcts { get; set; } = new List<Bohangct>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
