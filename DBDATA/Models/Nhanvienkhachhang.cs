using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhanvienkhachhang
{
    public string Manhanvien { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public int? Madongbo { get; set; }
}
