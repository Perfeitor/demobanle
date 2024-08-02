using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giavon
{
    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string? Makhohang { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Giavon1 { get; set; }

    public int Thang { get; set; }

    public int Nam { get; set; }
}
