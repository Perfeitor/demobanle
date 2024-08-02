using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class LichDathang
{
    public int ItemId { get; set; }

    public string Makhachhang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public DateTime Ngaydat { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string Tendangnhapsua { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public bool Trangthai { get; set; }

    public string? Makhohang { get; set; }
}
