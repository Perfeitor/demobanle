using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dathangct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public decimal? Tienhang { get; set; }

    public decimal? Tienvat { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Giabanlecovat { get; set; }

    public decimal? Giabanbuoncovat { get; set; }

    public decimal? Soluongthung { get; set; }

    public decimal? Toncuoikysl { get; set; }

    public decimal? Toncuoikygt { get; set; }

    public decimal? Tondaukysl { get; set; }

    public decimal? Tondaukygt { get; set; }

    public decimal? Nhaptksl { get; set; }

    public decimal? Nhaptkgt { get; set; }

    public decimal? Xuattksl { get; set; }

    public decimal? Xuattkgt { get; set; }

    public decimal? Soluongmax { get; set; }

    public decimal? Soluongmin { get; set; }

    public string? Mavat { get; set; }

    public int? Sort { get; set; }

    public decimal? Giathungchuavat { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Tyleck { get; set; }

    public decimal? Tienck { get; set; }

    public decimal? Tyleckkm { get; set; }

    public decimal? Tienckkm { get; set; }

    public virtual Dathang Dathang { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;
}
