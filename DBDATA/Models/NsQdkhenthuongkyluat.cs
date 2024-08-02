using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsQdkhenthuongkyluat
{
    public string Maquyetdinh { get; set; } = null!;

    public string? Manhanvien { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Ghichu { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhanvien? Nhanvien { get; set; }
}
