using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Phanquyenhanghoa
{
    public string Madonvi { get; set; } = null!;

    public string Manhomquyen { get; set; } = null!;

    public string Manganh { get; set; } = null!;

    public string? Manhomhang { get; set; }

    public bool? Trangthai { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? Manhanvien { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhomquyen Nhomquyen { get; set; } = null!;
}
