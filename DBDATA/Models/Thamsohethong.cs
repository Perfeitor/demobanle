using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Thamsohethong
{
    public string Mathamso { get; set; } = null!;

    public string? Tenthamso { get; set; }

    public string? Giatri { get; set; }

    public string? Diengiai { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhapsua { get; set; }
}
