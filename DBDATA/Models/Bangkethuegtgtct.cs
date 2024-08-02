using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Bangkethuegtgtct
{
    public string Madonvi { get; set; } = null!;

    public string Mabangke { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public string Sohoadon { get; set; } = null!;

    public DateTime Ngayhoadon { get; set; }

    public string Makhachhang { get; set; } = null!;

    public string Tenkhachhang { get; set; } = null!;

    public string? Masothue { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Tienhang { get; set; }

    public decimal Thuesuat { get; set; }

    public decimal? Tienvat { get; set; }

    public string? Mathang { get; set; }
}
