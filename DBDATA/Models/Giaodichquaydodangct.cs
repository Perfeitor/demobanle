using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichquaydodangct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Mabohang { get; set; } = null!;

    public string? Makhachhang { get; set; }

    public string Manganh { get; set; } = null!;

    public string? Manhomhang { get; set; }

    public string Makhohang { get; set; } = null!;

    public decimal? Soluong { get; set; }

    public decimal? Giabanlecovat { get; set; }

    public decimal? Giabanlechuavat { get; set; }

    public decimal? Tienhang { get; set; }

    public decimal? Tienvat { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Doanhthu { get; set; }

    public decimal? Tyleck { get; set; }

    public decimal? Tienck { get; set; }

    public int? Sort { get; set; }

    public decimal? Giavon { get; set; }

    public string? Matkno { get; set; }

    public string? Matkco { get; set; }

    public string? Matkthuegtgtno { get; set; }

    public string? Matkthuegtgtco { get; set; }

    public string? Matkchietkhauno { get; set; }

    public string? Matkchietkhauco { get; set; }

    public string? Matkvtu { get; set; }

    public string? Matkgiavon { get; set; }

    public decimal? Tylecknhomkhach { get; set; }

    public decimal? Tiencknhomkhach { get; set; }

    public decimal? Tiendoidiem { get; set; }

    public decimal? Tyleckmathang { get; set; }

    public decimal? Tienckmathang { get; set; }

    public decimal? Tylecknhomhang { get; set; }

    public decimal? Tiencknhomhang { get; set; }

    public decimal? Tylecknganhhang { get; set; }

    public decimal? Tiencknganhhang { get; set; }

    public decimal? Tyleckdonhang { get; set; }

    public decimal? Tienckdonhang { get; set; }

    public decimal? Tyleckbohang { get; set; }

    public decimal? Tienckbohang { get; set; }

    public virtual Giaodichquaydodang Giaodichquaydodang { get; set; } = null!;

    public virtual Khohang Khohang { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;
}
