using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Loaikhohang
{
    public string Maloaikho { get; set; } = null!;

    public string? Tenloaikho { get; set; }

    public string? Ghichu { get; set; }

    public int? Sokhomo { get; set; }
}
