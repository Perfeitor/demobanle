using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichphanboct
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodichphanbo { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal Sotien { get; set; }

    public decimal Sotiennt { get; set; }

    public int Sort { get; set; }

    public string Makhohang { get; set; } = null!;
}
