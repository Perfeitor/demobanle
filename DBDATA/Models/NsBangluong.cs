using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBangluong
{
    public string Mabangluong { get; set; } = null!;

    public string? Tenbangluong { get; set; }

    public int? Thang { get; set; }

    public int? Nam { get; set; }

    public int? Trangthaisudung { get; set; }

    public int? Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Maphongban { get; set; }

    public string? Mabophan { get; set; }

    public string Madonvi { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual NsBophan? NsBophan { get; set; }

    public virtual NsPhongban? NsPhongban { get; set; }
}
