using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class DmKheuoc
{
    public string Makheuoc { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Manganhang { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public int Trangthai { get; set; }

    public DateTime Ngaybatdau { get; set; }

    public DateTime Ngayketthuc { get; set; }

    public decimal Sotien { get; set; }

    public decimal Tylelai { get; set; }

    public int Songay { get; set; }
}
