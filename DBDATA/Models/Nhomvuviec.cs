using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomvuviec
{
    public string Manhomvuviec { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tennhomvuviec { get; set; } = null!;

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public virtual ICollection<Vuviec> Vuviecs { get; set; } = new List<Vuviec>();
}
