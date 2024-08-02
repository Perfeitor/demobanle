﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxTiendo
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public DateTime Ngayphatsinh { get; set; }

    public string Masieuthi { get; set; } = null!;

    public string Maca { get; set; } = null!;

    public decimal? Soluongdutinh { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Soluonghong { get; set; }

    public decimal? Nguyenlieudung { get; set; }

    public decimal? Nguyenlieuhong { get; set; }

    public string? Ghichu { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaysua { get; set; }
}
