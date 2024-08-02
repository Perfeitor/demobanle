using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Baogiact
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal? Congxuat { get; set; }

    public decimal? Dongia { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Thanhtien { get; set; }

    public DateTime? Ngaygiaohang { get; set; }

    public string? Noigiaohang { get; set; }

    public string? Ghichu { get; set; }

    public int? Sort { get; set; }

    public decimal? Dongiant { get; set; }

    public decimal? Tienhang { get; set; }

    public decimal? Tienhangnt { get; set; }

    public decimal? Dongiachuavat { get; set; }

    public decimal? Dongiachuavatnt { get; set; }

    public decimal? Tyleck { get; set; }

    public decimal? Tienck { get; set; }

    public decimal? Tiencknt { get; set; }

    public string? Mavat { get; set; }

    public decimal? Tienvat { get; set; }

    public decimal? Tienvatnt { get; set; }

    public decimal? Tylecktrendon { get; set; }

    public decimal? Tiencktrendon { get; set; }

    public decimal? Tiencktrendonnt { get; set; }

    public string? Mamau { get; set; }

    public decimal? Thanhtiennt { get; set; }

    public virtual Baogium Baogium { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;
}
