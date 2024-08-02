using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VXdTonghopvattucongtrinh
{
    public string? Mactpk { get; set; }

    public decimal? Tilehaohut { get; set; }

    public decimal? Heso { get; set; }

    public decimal? Dinhluong { get; set; }

    public string Masieuthi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tendaydu { get; set; }

    public string? Tenviettat { get; set; }

    public string Manhomhang { get; set; } = null!;

    public string? Tennhomhang { get; set; }

    public string Donvile { get; set; } = null!;

    public decimal? SumSoluong { get; set; }

    public string Ghichu { get; set; } = null!;

    public string Macongviec { get; set; } = null!;

    public decimal? Khoiluong { get; set; }

    public string Mahangmuc { get; set; } = null!;

    public decimal? Quycach { get; set; }

    public string Madutoan { get; set; } = null!;

    public string Macongtrinh { get; set; } = null!;

    public string? Tencongtrinh { get; set; }

    public string? Tenhangmuc { get; set; }
}
