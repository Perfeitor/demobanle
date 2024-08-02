using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDenghilinhlieuctnguyenlieu
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Mathanhpham { get; set; } = null!;

    public decimal Soluong { get; set; }

    public decimal Dinhluong { get; set; }

    public decimal Tilehaohut { get; set; }

    public decimal Hieusuat { get; set; }

    public string? Ghichu { get; set; }

    public string? Makhachhang { get; set; }

    public int? Sort { get; set; }
}
