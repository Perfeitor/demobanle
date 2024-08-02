using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Canhbaomaxmin
{
    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public decimal Tonmax { get; set; }

    public decimal Tonmin { get; set; }
}
