using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class GiaodichLinkQuy
{
    public string Magiaodichpk { get; set; } = null!;

    public string Mactktpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public decimal? Sotienthanhtoan { get; set; }

    public decimal? Sotienthanhtoannt { get; set; }

    public int? Loai { get; set; }

    public int? Thang { get; set; }

    public int? Nam { get; set; }

    public string? Makhachhang { get; set; }

    public string? Matk { get; set; }
}
