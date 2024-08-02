using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmnganhang
{
    public string Manganhang { get; set; } = null!;

    public string? Tennganhang { get; set; }

    public string? Diachi { get; set; }

    public string? Dienthoai { get; set; }

    public string? Email { get; set; }

    public string? Tenviettat { get; set; }

    public virtual ICollection<Dmchinhanhnganhang> Dmchinhanhnganhangs { get; set; } = new List<Dmchinhanhnganhang>();
}
