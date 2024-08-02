using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmtaikhoanketchuyen
{
    public string Matkketchuyen { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public int? Nam { get; set; }

    public int? Thang { get; set; }

    public string? Ghichu { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
