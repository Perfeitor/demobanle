using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxNvXacnhan
{
    public string Madonvi { get; set; } = null!;

    public string Manhanvien { get; set; } = null!;

    public string? Maxacnhan { get; set; }

    public string Manghiepvu { get; set; } = null!;

    public int? Trangthai { get; set; }

    public string? Ghichu { get; set; }

    public virtual SxDmXacnhan? SxDmXacnhan { get; set; }
}
