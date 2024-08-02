using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Khohangtichdiem
{
    public string Makhohang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal? Tienquydoidiem { get; set; }

    public decimal? Tienquydoi { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime Ngayphatsinh { get; set; }
}
