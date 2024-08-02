using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsPhucapnhanvien
{
    public string Maphucap { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public decimal? Sotien { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhanvien Nhanvien { get; set; } = null!;

    public virtual NsPhucap NsPhucap { get; set; } = null!;
}
