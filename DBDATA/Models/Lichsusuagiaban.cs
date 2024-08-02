using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Lichsusuagiaban
{
    public string Masieuthi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public decimal Giabancu { get; set; }

    public decimal Giabanmoi { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Nguoitao { get; set; }

    public string? Madonvi { get; set; }

    public string? Formname { get; set; }

    public string? Magiaodichpk { get; set; }

    public decimal? Giamuacu { get; set; }

    public decimal? Giamuamoi { get; set; }
}
