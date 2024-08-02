using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Kichthuocsp
{
    public string Masieuthi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal? Chieucao { get; set; }

    public decimal? Chieurong { get; set; }

    public decimal? Chieudai { get; set; }

    public decimal? Trongluong { get; set; }

    public virtual Mathang Mathang { get; set; } = null!;
}
