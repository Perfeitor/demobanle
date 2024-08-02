using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dutoan
{
    public string Madonvi { get; set; } = null!;

    public string Madutoan { get; set; } = null!;

    public string? Macongtrinh { get; set; }

    public string? Tencongtrinh { get; set; }

    public string? Mahangmuc { get; set; }

    public string? Tenhangmuc { get; set; }

    public string? Ghichu { get; set; }

    public int? Trangthai { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaysua { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }

    public int? Loai { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public string? Makhachhang { get; set; }

    public decimal? Tongdutoan { get; set; }

    public virtual ICollection<Dutoanct> Dutoancts { get; set; } = new List<Dutoanct>();
}
