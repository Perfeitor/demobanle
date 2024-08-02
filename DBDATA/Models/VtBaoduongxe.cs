using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtBaoduongxe
{
    public string Maxe { get; set; } = null!;

    public string Maloaibaoduong { get; set; } = null!;

    public DateTime? Ngayhieuluc { get; set; }

    public DateTime? Ngaydenhan { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
