using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VXdTonghopnhapvtcongtrinh
{
    public string? Macongtrinh { get; set; }

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Manganh { get; set; } = null!;

    public string Manhomhang { get; set; } = null!;

    public decimal Soluong { get; set; }
}
