using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichctkhuyenmai
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal? Soluongkm { get; set; }

    public decimal? Tienkm { get; set; }

    public string Chuongtrinhkm { get; set; } = null!;

    public string Id { get; set; } = null!;

    public int? Kieukm { get; set; }

    public string? Masieuthi { get; set; }
}
