using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Bangkethuegtgt
{
    public string Madonvi { get; set; } = null!;

    public string Mabangke { get; set; } = null!;

    public string Maky { get; set; } = null!;

    public int Nam { get; set; }

    public int Loai { get; set; }

    public string? Ghichu { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime Tungay { get; set; }

    public DateTime Denngay { get; set; }
}
