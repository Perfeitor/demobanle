using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichquay
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maptnx { get; set; } = null!;

    public int Trangthai { get; set; }

    public string Maquay { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string Makhachhang { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public string Manvcongno { get; set; } = null!;

    public DateTime Ngayphatsinh { get; set; }

    public int Thantoanthe { get; set; }

    public string? Ghichu { get; set; }

    public decimal? Tienmat { get; set; }

    public decimal? Tienthe { get; set; }

    public decimal? Tiendoidiem { get; set; }

    public decimal? Tienqrcode { get; set; }

    public string? Sochungtu { get; set; }

    public string? Nguoinhan { get; set; }

    public virtual Dmptnx Dmptnx { get; set; } = null!;

    public virtual ICollection<Giaodichquayct> Giaodichquaycts { get; set; } = new List<Giaodichquayct>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
