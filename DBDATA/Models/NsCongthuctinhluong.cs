using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsCongthuctinhluong
{
    public string Macongthuc { get; set; } = null!;

    public string? Maphongban { get; set; }

    public string? Mabophan { get; set; }

    public string? Congthuctinhluong { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string Madonvi { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual NsBophan? NsBophan { get; set; }

    public virtual NsPhongban? NsPhongban { get; set; }
}
