using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dathang
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maptnx { get; set; } = null!;

    public string? Ghichu { get; set; }

    public int? Trangthai { get; set; }

    public string Makhachhang { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngaygiaodukien { get; set; }

    public DateTime? Ngaygiaothucte { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Magiaodichphu { get; set; }

    public string? Diachigiaohang { get; set; }

    public string? Nguoinhanhang { get; set; }

    public string? Makhohang { get; set; }

    public string? Madongop { get; set; }

    public virtual ICollection<Dathangct> Dathangcts { get; set; } = new List<Dathangct>();

    public virtual Dmptnx Dmptnx { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
