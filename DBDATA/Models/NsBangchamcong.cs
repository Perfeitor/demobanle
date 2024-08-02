using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBangchamcong
{
    public string Mabangchamcong { get; set; } = null!;

    public string? Mabophan { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Tungay { get; set; }

    public DateTime? Denngay { get; set; }

    public int? Trangthai { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaysua { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Ghichu { get; set; }

    public virtual NsBophan? NsBophan { get; set; }
}
