using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtLoaibaoduong
{
    public string Maloaibaoduong { get; set; } = null!;

    public string? Tenloaibaoduong { get; set; }

    public string? Maloaichuky { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
