using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLuongcoban
{
    public string Maluongcoban { get; set; } = null!;

    public string? Tenluongcoban { get; set; }

    public decimal? Sotien { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
