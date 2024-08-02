using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBaohiemnhanvien
{
    public string Mabaohiem { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public string Maluongcoban { get; set; } = null!;

    public decimal? Heso { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Nhanvien Nhanvien { get; set; } = null!;

    public virtual NsBaohiem NsBaohiem { get; set; } = null!;
}
