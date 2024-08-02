using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxDmXacnhan
{
    public string Maxacnhan { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Nghiepvu { get; set; }

    public string? Ghichu { get; set; }

    public virtual ICollection<SxDmXacnhanct> SxDmXacnhancts { get; set; } = new List<SxDmXacnhanct>();

    public virtual ICollection<SxNvXacnhan> SxNvXacnhans { get; set; } = new List<SxNvXacnhan>();
}
