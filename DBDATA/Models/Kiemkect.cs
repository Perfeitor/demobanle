using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Kiemkect
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Manganh { get; set; }

    public string? Manhomhang { get; set; }

    public string? Makehang { get; set; }

    public string Masieuthi { get; set; } = null!;

    public decimal? Soluongtonmay { get; set; }

    public decimal? Soluongkiemke { get; set; }

    public decimal? Soluongchenhlech { get; set; }

    public decimal? Tientonmay { get; set; }

    public decimal? Tienkiemke { get; set; }

    public decimal? Tienchenhlech { get; set; }

    public decimal? Giavon { get; set; }

    public decimal? Soluongnhap { get; set; }

    public decimal? Soluongxuat { get; set; }

    public virtual Kiemke Kiemke { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;

    public virtual Nganhhang? Nganhhang { get; set; }

    public virtual Nhomhang? Nhomhang { get; set; }
}
