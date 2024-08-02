using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Kehang
{
    public string Makehang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tenkehang { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Ghichu { get; set; }
}
