using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichctnguyenlieu
{
    public Guid Id { get; set; }

    public string? Magiaodichpk { get; set; }

    public string? Magiaodichphu { get; set; }

    public string? Madonvi { get; set; }

    public int? Trangthai { get; set; }
}
