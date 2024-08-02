using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dinhmucdathang
{
    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public bool Chanthung { get; set; }

    public decimal Tonmax { get; set; }

    public decimal Tonmin { get; set; }
}
