using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Menunhomquyen
{
    public string Manhomquyen { get; set; } = null!;

    public string Menuid { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public bool? Them { get; set; }

    public bool? Sua { get; set; }

    public bool? Xoa { get; set; }

    public bool? Luutam { get; set; }

    public bool? Luuthuc { get; set; }

    public bool? Khoiphuc { get; set; }

    public bool? Xem { get; set; }

    public string Manguoitao { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public bool? GridSetting { get; set; }

    public bool? SolieutheoUser { get; set; }

    public bool? Quyen01 { get; set; }

    public bool? Quyen02 { get; set; }

    public bool? Quyen03 { get; set; }

    public bool? Quyen04 { get; set; }

    public bool? Quyen05 { get; set; }

    public bool? Quyen06 { get; set; }

    public bool? Quyen07 { get; set; }

    public bool? Quyen08 { get; set; }

    public bool? Quyen09 { get; set; }

    public bool? Quyen10 { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhomquyen Nhomquyen { get; set; } = null!;
}
