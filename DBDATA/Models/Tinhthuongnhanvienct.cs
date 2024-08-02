using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Tinhthuongnhanvienct
{
    public string Matinhthuong { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public string Maapdung { get; set; } = null!;

    public decimal? Tilethuong { get; set; }

    public decimal? Tienthuong { get; set; }

    public decimal? Doanhso { get; set; }
}
