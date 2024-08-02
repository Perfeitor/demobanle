using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmtk
{
    public string Matk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maloaitk { get; set; } = null!;

    public string? Matkcha { get; set; }

    public int? Tkchitiet { get; set; }

    public int? Bactk { get; set; }

    public int? Tinhchat { get; set; }

    public int? Trangthai { get; set; }

    public bool? Trangthaisd { get; set; }

    public string? Tentk { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Dmloaitk MaloaitkNavigation { get; set; } = null!;
}
