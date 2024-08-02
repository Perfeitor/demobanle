using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichphanbo
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public string Magiaodichphanbo { get; set; } = null!;

    public int Loaiphanbo { get; set; }

    public decimal Sotien { get; set; }
}
