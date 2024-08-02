using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmcongthucsx
{
    public string Mactpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Tenct { get; set; } = null!;

    public int Trangthai { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string Tendangnhapsua { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Ghichu { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<SxDmcongthucsxct> SxDmcongthucsxcts { get; set; } = new List<SxDmcongthucsxct>();
}
