using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmHieusuat
{
    public string Mahieusuat { get; set; } = null!;

    public string Tenhieusuat { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string Madonvi { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaysua { get; set; }
}
