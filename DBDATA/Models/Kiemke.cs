using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Kiemke
{
    public string Magiaodichpk { get; set; } = null!;

    public string Maptnx { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public string? Manganhhang { get; set; }

    public string? Manhomhang { get; set; }

    public string? Makehang { get; set; }

    public string? Mavtu { get; set; }

    public int Trangthai { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Manguoitao { get; set; } = null!;

    public string? Mavuviecthua { get; set; }

    public string? Mavuviecthieu { get; set; }

    public string? Ghichu { get; set; }

    public virtual ICollection<Kiemkect> Kiemkects { get; set; } = new List<Kiemkect>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
