using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtThuonghieu
{
    public string Mathuonghieu { get; set; } = null!;

    public string? Tenthuonghieu { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
