using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class GridSetting
{
    public string Madonvi { get; set; } = null!;

    public string Formname { get; set; } = null!;

    public string Columnname { get; set; } = null!;

    public bool? Ishide { get; set; }

    public int? Position { get; set; }

    public double? Width { get; set; }

    public string? Manhomquyen { get; set; }
}
