using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Sodudaukybk2020
{
    public int Thang { get; set; }

    public int Nam { get; set; }

    public string Matk { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public string Makho { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal Sotienno { get; set; }

    public decimal Sotienco { get; set; }

    public int Trangthai { get; set; }

    public string Kieusodu { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public decimal? Sotiennont { get; set; }

    public decimal? Sotiencont { get; set; }

    public string? Mangoaite { get; set; }

    public string? Taikhoan { get; set; }
}
