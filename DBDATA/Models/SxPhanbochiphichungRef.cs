using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxPhanbochiphichungRef
{
    public string Madonvi { get; set; } = null!;

    public string Maky { get; set; } = null!;

    public string Matk { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public decimal Sotien { get; set; }

    public decimal Sotiendaphanbo { get; set; }

    public int? Loaiphanbo { get; set; }

    public decimal? Tilephanbo { get; set; }
}
