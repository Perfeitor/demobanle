using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomkhachhang
{
    public string Manhomkhachhang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tennhomkhachhang { get; set; } = null!;

    public decimal? Tilechietkhau { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string Maloaikhach { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
