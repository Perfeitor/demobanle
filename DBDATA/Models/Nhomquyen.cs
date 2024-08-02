using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomquyen
{
    public string Manhomquyen { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tennhomquyen { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string? Tendangnhapsua { get; set; }

    public int? Solien { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Menunhomquyen> Menunhomquyens { get; set; } = new List<Menunhomquyen>();

    public virtual ICollection<Nhomquyenphu> Nhomquyenphus { get; set; } = new List<Nhomquyenphu>();
}
