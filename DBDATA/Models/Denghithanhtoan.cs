using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Denghithanhtoan
{
    public string Madenghi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Makhachang { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Maloaichungtu { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }

    public string? Noidung { get; set; }

    public string? Sotaikhoan { get; set; }

    public string? Chutaikhoan { get; set; }

    public string? Nganhang { get; set; }

    public string? Thoihanthanhtoan { get; set; }

    public int? Trangthai { get; set; }

    public string? Maloaitien { get; set; }

    public decimal? Tygia { get; set; }
}
