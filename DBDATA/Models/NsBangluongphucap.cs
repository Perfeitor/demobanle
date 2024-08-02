using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBangluongphucap
{
    public string? Mabangluong { get; set; }

    public string? Manhanvien { get; set; }

    public string? Maphucap { get; set; }

    public decimal? Sotien { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? Madonvi { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual NsBangluong? NsBangluong { get; set; }

    public virtual NsPhucap? NsPhucap { get; set; }
}
