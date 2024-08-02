using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class DmVanchuyen
{
    public string Madonvi { get; set; } = null!;

    public string Mavanchuyen { get; set; } = null!;

    public string Tenvanchuyen { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaysua { get; set; }
}
