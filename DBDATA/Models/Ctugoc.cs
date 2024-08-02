using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Ctugoc
{
    public string Magiaodichpk { get; set; } = null!;

    public string Matk { get; set; } = null!;

    public string Matkdoiung { get; set; } = null!;

    public decimal? Sotienno { get; set; }

    public decimal? Sotienco { get; set; }

    public string Madonvi { get; set; } = null!;

    public string? Manvcongno { get; set; }

    public string? Makhachhang { get; set; }

    public string? Makhohang { get; set; }

    public string Maloaict { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string? Mavuviec { get; set; }

    public string? Makmchiphi { get; set; }

    public string? Madoituongtaphop { get; set; }

    public string? Taikhoan { get; set; }

    public DateTime Ngayghiso { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Magiaodichphu { get; set; }

    public string? Mangoaite { get; set; }

    public decimal? Tygia { get; set; }

    public decimal? Sotiennont { get; set; }

    public decimal? Sotiencont { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
