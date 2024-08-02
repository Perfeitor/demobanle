using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsTaisanct
{
    public string Madonvi { get; set; } = null!;

    public string Mataisan { get; set; } = null!;

    public string Mataisanphu { get; set; } = null!;

    public int? Loaihang { get; set; }

    public decimal? Soluong { get; set; }

    public int? Sort { get; set; }

    public decimal? Giatri { get; set; }
}
