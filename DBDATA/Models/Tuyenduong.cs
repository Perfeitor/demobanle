using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Tuyenduong
{
    public string Matuyen { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tentuyen { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string? Sdtnguoigiansat { get; set; }
}
