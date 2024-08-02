using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Bohangct
{
    public string Mabohang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public int? Soluong { get; set; }

    public decimal? Tylechietkhaule { get; set; }

    public decimal? Tylechietkhaubuon { get; set; }

    public int? Trangthai { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Tongtienbanbuon { get; set; }

    public decimal? Tongtienbanle { get; set; }

    public virtual Bohang Bohang { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;
}
