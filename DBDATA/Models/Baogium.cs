using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Baogium
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Nguoilienhe { get; set; }

    public string? Dienthoai { get; set; }

    public string? Maptnx { get; set; }

    public string? Ghichu { get; set; }

    public int? Trangthai { get; set; }

    public string? Makhachhang { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public DateTime? Tungay { get; set; }

    public DateTime? Denngay { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string? Manhacungcap { get; set; }

    public string? Tiente { get; set; }

    public string? NguoilienheKh { get; set; }

    public string? DienthoaiKh { get; set; }

    public string? Magiaodichphu { get; set; }

    public decimal? Tygia { get; set; }

    public string? Madonggoi { get; set; }

    public string? Mavanchuyen { get; set; }

    public string? Dieukhoanthanhtoan { get; set; }

    public int? Trangthaick { get; set; }

    public virtual ICollection<Baogiact> Baogiacts { get; set; } = new List<Baogiact>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
