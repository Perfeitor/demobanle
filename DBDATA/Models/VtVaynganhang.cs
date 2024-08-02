using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtVaynganhang
{
    public string Maxe { get; set; } = null!;

    public decimal? Sotienvay { get; set; }

    public decimal? Sothangtra { get; set; }

    public DateTime? Ngayhieuluc { get; set; }

    public decimal? Sotientratheothang { get; set; }

    public decimal? Sotienconlai { get; set; }

    public string Manganhang { get; set; } = null!;

    public string? Taikhoangiaodich { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
