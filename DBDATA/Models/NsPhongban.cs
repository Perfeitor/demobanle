using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsPhongban
{
    public string Maphongban { get; set; } = null!;

    public string? Tenphongban { get; set; }

    public int? Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsBangluong> NsBangluongs { get; set; } = new List<NsBangluong>();

    public virtual ICollection<NsBophan> NsBophans { get; set; } = new List<NsBophan>();

    public virtual ICollection<NsCongthuctinhluong> NsCongthuctinhluongs { get; set; } = new List<NsCongthuctinhluong>();

    public virtual ICollection<NsDinhmucluong> NsDinhmucluongs { get; set; } = new List<NsDinhmucluong>();

    public virtual NsLichlamvieccasangchieu? NsLichlamvieccasangchieu { get; set; }
}
