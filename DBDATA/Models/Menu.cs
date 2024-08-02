using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Menu
{
    public string Menuid { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Menuname { get; set; }

    public string? Menucha { get; set; }

    public int? Thutu { get; set; }

    public string? Formname { get; set; }

    public int? Loaimenu { get; set; }

    public string Maphanhe { get; set; } = null!;

    public string? Thamso { get; set; }

    public int? Cap { get; set; }
}
