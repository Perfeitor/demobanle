using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtLoaithechap
{
    public string Maloaithechap { get; set; } = null!;

    public string? Tenloaithechap { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
