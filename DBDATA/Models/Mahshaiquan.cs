using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Mahshaiquan
{
    public string Mahs { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string Tendangnhapsua { get; set; } = null!;
}
