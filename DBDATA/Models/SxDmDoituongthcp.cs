using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmDoituongthcp
{
    public string Madonvi { get; set; } = null!;

    public string Madoituongtaphop { get; set; } = null!;

    public int Loaidoituong { get; set; }

    public string Tendoituongtaphop { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaysua { get; set; }

    public string Masieuthi { get; set; } = null!;
}
