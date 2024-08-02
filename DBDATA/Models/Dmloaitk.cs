using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmloaitk
{
    public string Maloaitk { get; set; } = null!;

    public string? Tenloaitk { get; set; }

    public virtual ICollection<Dmtk> Dmtks { get; set; } = new List<Dmtk>();
}
