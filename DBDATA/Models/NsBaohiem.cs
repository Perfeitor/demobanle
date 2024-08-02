using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBaohiem
{
    public string Mabaohiem { get; set; } = null!;

    public string? Tenbaohiem { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsBaohiemnhanvien> NsBaohiemnhanviens { get; set; } = new List<NsBaohiemnhanvien>();
}
