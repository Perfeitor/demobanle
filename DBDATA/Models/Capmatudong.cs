using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Capmatudong
{
    public string Madonvi { get; set; } = null!;

    public int? Sotutang { get; set; }

    public string Chucnang { get; set; } = null!;

    public string? Ten { get; set; }

    public int? Sokytu { get; set; }

    public int? IsAuto { get; set; }

    public int? Kieututang { get; set; }

    public string? Matruoc { get; set; }

    public string? Masau { get; set; }

    public string Loai { get; set; } = null!;
}
