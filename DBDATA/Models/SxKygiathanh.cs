using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class SxKygiathanh
{
    public string Madonvi { get; set; } = null!;

    public string Ma { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public DateTime Tungay { get; set; }

    public DateTime Denngay { get; set; }

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngaysua { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string Ky { get; set; } = null!;

    public int Nam { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public int? Thang { get; set; }

    public int? Loaidanhgiadodang { get; set; }

    public int? Loai { get; set; }

    public virtual ICollection<SxKygiathanhct> SxKygiathanhcts { get; set; } = new List<SxKygiathanhct>();

    public virtual ICollection<SxPhanbochiphichungct> SxPhanbochiphichungcts { get; set; } = new List<SxPhanbochiphichungct>();
}
