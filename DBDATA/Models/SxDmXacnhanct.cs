using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmXacnhanct
{
    public string Madonvi { get; set; } = null!;

    public string Maxacnhan { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public int? Capdo { get; set; }

    public virtual SxDmXacnhan SxDmXacnhan { get; set; } = null!;
}
