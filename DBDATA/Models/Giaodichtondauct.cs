using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichtondauct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal Soluong { get; set; }

    public decimal Giavon { get; set; }

    public decimal Tienvon { get; set; }

    public int Sort { get; set; }
}
