using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmcongthucsxct
{
    public string Mactpk { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal Soluong { get; set; }

    public string Madonvi { get; set; } = null!;

    public decimal? Tilehaohut { get; set; }

    public string? Makhachhang { get; set; }

    public string? Masieuthithaythe { get; set; }

    public string? Makhachhangthaythe { get; set; }

    public int? Sort { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Tiledat { get; set; }

    public decimal? Heso { get; set; }

    public virtual SxDmcongthucsx SxDmcongthucsx { get; set; } = null!;
}
