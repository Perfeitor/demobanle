using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomvattu
{
    public string Manhom { get; set; } = null!;

    public string? Tennhom { get; set; }

    public string? Manhomcha { get; set; }

    public string Madonvi { get; set; } = null!;

    public int? Bac { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }
}
