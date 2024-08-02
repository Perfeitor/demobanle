using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtLoaichukybaoduong
{
    public string Maloaichuky { get; set; } = null!;

    public string? Tenloaichuky { get; set; }

    public decimal? Tylevuotchophep { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
