using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dinhmucdathangnoibo
{
    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public bool Chanthung { get; set; }

    public decimal Tonmax { get; set; }

    public decimal Tonmin { get; set; }

    public bool? Trangthai { get; set; }

    public bool? Loai { get; set; }
}
