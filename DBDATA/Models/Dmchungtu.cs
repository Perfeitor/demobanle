using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmchungtu
{
    public string Mactu { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Tenctu { get; set; }

    public string? Matkno { get; set; }

    public string? Matkco { get; set; }

    public bool? Trangthai { get; set; }

    public string? Matknodf { get; set; }

    public string? Matkcodf { get; set; }

    public string Kyhieuct { get; set; } = null!;

    public string Maloaictu { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Dmloaichungtu MaloaictuNavigation { get; set; } = null!;
}
