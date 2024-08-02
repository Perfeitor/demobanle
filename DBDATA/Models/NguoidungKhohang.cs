using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NguoidungKhohang
{
    public string Madonvi { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public bool? Trangthai { get; set; }
}
