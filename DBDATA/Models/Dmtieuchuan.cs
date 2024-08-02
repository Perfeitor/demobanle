using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmtieuchuan
{
    public string Madonvi { get; set; } = null!;

    public string Matieuchuan { get; set; } = null!;

    public int Maloaitieuchuan { get; set; }

    public string Tentieuchuan { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime? Ngaysua { get; set; }
}
