using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VTsKhauhao
{
    public string Madonvi { get; set; } = null!;

    public string Mataisan { get; set; } = null!;

    public string? Tentaisan { get; set; }

    public string? Maloaitaisan { get; set; }

    public string? Manhomtaisan { get; set; }

    public string? Maphongban { get; set; }

    public int? Loaihang { get; set; }

    public decimal? Haomonluyke { get; set; }

    public int? Songaydakhauhao { get; set; }

    public decimal? Giatriconlai { get; set; }

    public DateOnly? Ngaykhauhaogannhat { get; set; }
}
