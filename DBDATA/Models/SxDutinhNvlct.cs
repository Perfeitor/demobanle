using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDutinhNvlct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal Soluong { get; set; }

    public decimal? Toncuoikysl { get; set; }

    public decimal? Soluongnhap { get; set; }

    public decimal? Soluongdutinh { get; set; }

    public string? Ghichu { get; set; }

    public virtual SxDutinhNvl SxDutinhNvl { get; set; } = null!;
}
