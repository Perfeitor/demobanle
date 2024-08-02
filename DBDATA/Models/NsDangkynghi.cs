using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsDangkynghi
{
    public string Madangkynghi { get; set; } = null!;

    public string? Tendangkynghi { get; set; }

    public string? Manhanvien { get; set; }

    public string? Mabophan { get; set; }

    public DateTime? Tungay { get; set; }

    public DateTime? Denngay { get; set; }

    public string? Ghichu { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual NsBophan? NsBophan { get; set; }
}
