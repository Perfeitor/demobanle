using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtXe
{
    public string Maxe { get; set; } = null!;

    public string Manhacungcap { get; set; } = null!;

    public string Mathuonghieu { get; set; } = null!;

    public int? Namsanxuat { get; set; }

    public string Bienkiemsoat { get; set; } = null!;

    public string Somay { get; set; } = null!;

    public int? Sochongoi { get; set; }

    public string? Giaydangkyso { get; set; }

    public string? Mauson { get; set; }

    public string Sokhung { get; set; } = null!;

    public decimal? Dongiathue { get; set; }

    public int Trangthaikinhdoanh { get; set; }

    public int Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;
}
