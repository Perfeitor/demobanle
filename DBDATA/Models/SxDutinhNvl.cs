using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDutinhNvl
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public DateTime Ngayphatsinh { get; set; }

    public string? Magiaodichphu { get; set; }

    public string? Ghichu { get; set; }

    public string? Kehoach { get; set; }

    public string? Dondat { get; set; }

    public string? Thanhpham { get; set; }

    public string? Khohang { get; set; }

    public int? Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngaysua { get; set; }

    public string? Tendangnhapsua { get; set; }

    public int Loaigiaodich { get; set; }

    public DateTime? Ngaydukien { get; set; }

    public virtual ICollection<SxDutinhNvlct> SxDutinhNvlcts { get; set; } = new List<SxDutinhNvlct>();
}
