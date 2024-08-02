using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichtichdiem
{
    public int Id { get; set; }

    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maptnx { get; set; } = null!;

    public string? Makhohang { get; set; }

    public string? Makhachhang { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public decimal? Doanhsoban { get; set; }

    public decimal? Tiendoidiem { get; set; }

    public decimal? Tientralai { get; set; }

    public decimal? Diemtichluy { get; set; }

    public decimal? Diemdadoi { get; set; }

    public decimal? Diemtralai { get; set; }

    public DateTime? Ngaytao { get; set; }
}
