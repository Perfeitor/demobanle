using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dutoanct
{
    public string Madonvi { get; set; } = null!;

    public string Madutoan { get; set; } = null!;

    public string Mahangmuc { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string? Tendaydu { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public decimal? Dongia1 { get; set; }

    public decimal? Dongia2 { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Thanhtien1 { get; set; }

    public decimal? Thanhtien2 { get; set; }

    public decimal? Hesodieuchinh { get; set; }

    public decimal? Hesodieuchinh1 { get; set; }

    public decimal? Hesodieuchinh2 { get; set; }

    public string? Cancu { get; set; }

    public string? Cancu1 { get; set; }

    public string? Cancu2 { get; set; }

    public string? Ghichu { get; set; }

    public int? Sort { get; set; }

    public virtual Dutoan Dutoan { get; set; } = null!;
}
