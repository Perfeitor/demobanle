using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class DmMahaiquan
{
    public string Madonvi { get; set; } = null!;

    public string Mahaiquan { get; set; } = null!;

    public string Tendaydu { get; set; } = null!;

    public string Tenrutgon { get; set; } = null!;

    public string? Quycach { get; set; }

    public string Mahs { get; set; } = null!;

    public string Madvtinh { get; set; } = null!;

    public decimal Quydoi { get; set; }

    public decimal? Giabuonchuavat { get; set; }
}
