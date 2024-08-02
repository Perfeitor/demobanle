using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class XntTaisan
{
    public string Madonvi { get; set; } = null!;

    public string Mataisan { get; set; } = null!;

    public string? Maphongban { get; set; }

    public DateOnly? Ngayhoachtoan { get; set; }

    public decimal? Tonslcuoiky { get; set; }
}
