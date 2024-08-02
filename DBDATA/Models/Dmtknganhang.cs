using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmtknganhang
{
    public string Taikhoan { get; set; } = null!;

    public string Sothe { get; set; } = null!;

    public string Manganhang { get; set; } = null!;

    public string? Tenviettat { get; set; }

    public string Machinhanh { get; set; } = null!;

    public string? Dienthoai { get; set; }

    public string? Email { get; set; }
}
