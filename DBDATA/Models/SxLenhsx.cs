using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxLenhsx
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public DateTime Ngayphatsinh { get; set; }

    public int Tinhtrang { get; set; }

    public string? Ghichu { get; set; }

    public string? Magiaodichphu { get; set; }

    public int Trangthai { get; set; }

    public DateTime? Ngaysanxuat { get; set; }

    public string? Mahopdong { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaysua { get; set; }

    public DateTime? Ngayketthuc { get; set; }
}
