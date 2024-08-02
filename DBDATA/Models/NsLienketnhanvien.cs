using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLienketnhanvien
{
    public string Mabophanlamthem { get; set; } = null!;

    public string? Manhanvienchinh { get; set; }

    public string? Manhanvienphu { get; set; }

    public decimal? Hesoluong { get; set; }

    public string Madonvi { get; set; } = null!;

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaytao { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
