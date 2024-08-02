using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VXdDinhmuc
{
    public string Mathanhpham { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Mactpk { get; set; }

    public string? Maplv { get; set; }

    public string Masieuthi { get; set; } = null!;

    public decimal? Soluong { get; set; }

    public decimal? Tilehaohut { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Heso { get; set; }
}
