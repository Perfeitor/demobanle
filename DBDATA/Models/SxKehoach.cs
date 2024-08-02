using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxKehoach
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Magiaodichphu { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public string? Manvkinhdoanh { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Ghichu { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public DateTime Ngaysanxuat { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public virtual ICollection<SxKehoachct> SxKehoachcts { get; set; } = new List<SxKehoachct>();
}
