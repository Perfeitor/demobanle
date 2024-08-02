using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dondat
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Magiaodichphu { get; set; }

    public string Makhachhang { get; set; } = null!;

    public string? Nguoinhanhang { get; set; }

    public string? Manvkinhdoanh { get; set; }

    public string? Ghichu { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public DateTime? Ngaygiaohang { get; set; }

    public string? Matinhtrang { get; set; }

    public string? Tiente { get; set; }

    public decimal? Tygia { get; set; }

    public int? Trangthai { get; set; }

    public string? Maptnx { get; set; }

    public string? Madonggoi { get; set; }

    public string? Mavanchuyen { get; set; }

    public string? Dieukhoanthanhtoan { get; set; }

    public int? Trangthaick { get; set; }

    public virtual ICollection<Dondatct> Dondatcts { get; set; } = new List<Dondatct>();
}
