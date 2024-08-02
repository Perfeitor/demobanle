using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmloaichungtu
{
    public string Maloaictu { get; set; } = null!;

    public string? Tenloaictu { get; set; }

    public int? Trangthai { get; set; }

    public virtual ICollection<Dmchungtu> Dmchungtus { get; set; } = new List<Dmchungtu>();
}
