using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichgiaonhanct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Manganh { get; set; } = null!;

    public string? Makhachhang { get; set; }

    public string Manhomhang { get; set; } = null!;

    public decimal? Soluongnhap { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public int? Sort { get; set; }

    public virtual Giaodichgiaonhan Giaodichgiaonhan { get; set; } = null!;
}
