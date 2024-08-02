using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsPhucap
{
    public string Maphucap { get; set; } = null!;

    public string? Tenphucap { get; set; }

    public int? Trangthaiphucap { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsPhucapnhanvien> NsPhucapnhanviens { get; set; } = new List<NsPhucapnhanvien>();
}
