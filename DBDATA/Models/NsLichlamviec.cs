using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLichlamviec
{
    public string Malichlamviec { get; set; } = null!;

    public string? Tenlichlamviec { get; set; }

    public int? Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsGanlichlamviec> NsGanlichlamviecs { get; set; } = new List<NsGanlichlamviec>();
}
