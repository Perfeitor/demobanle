using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Denghithanhtoanct
{
    public string Madenghi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public decimal? Sotien { get; set; }

    public decimal? Sotiennt { get; set; }

    public string? Ghichu { get; set; }

    public string? Diengiai { get; set; }
}
