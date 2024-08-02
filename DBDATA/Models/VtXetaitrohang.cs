using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtXetaitrohang
{
    public string Maxe { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Biensoxe { get; set; }

    public decimal? Tongtrongluong { get; set; }

    public decimal? Tongsokhoi { get; set; }

    public int? Trangthaisudung { get; set; }

    public string? Ghichu { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }
}
