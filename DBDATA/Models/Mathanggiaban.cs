using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Mathanggiaban
{
    public string Id { get; set; } = null!;

    public string? Masieuthi { get; set; }

    public string? Madonvi { get; set; }

    public decimal? Giabanbuoncovat { get; set; }
}
