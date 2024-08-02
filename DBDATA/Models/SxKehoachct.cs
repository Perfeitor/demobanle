using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxKehoachct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public decimal Soluong { get; set; }

    public string Mactpk { get; set; } = null!;

    public virtual SxKehoach SxKehoach { get; set; } = null!;
}
