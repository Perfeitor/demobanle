using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichtondau
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public string Manguoitao { get; set; } = null!;

    public DateTime Ngayphatsinh { get; set; }

    public DateTime? Ngaytao { get; set; }

    public int Trangthai { get; set; }

    public string? Ghichu { get; set; }
}
