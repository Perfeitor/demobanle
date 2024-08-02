using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichgiaonhan
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Ghichu { get; set; }

    public DateTime Ngaytao { get; set; }

    public string? Magiaodichphu { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Maquay { get; set; }

    public virtual ICollection<Giaodichgiaonhanct> Giaodichgiaonhancts { get; set; } = new List<Giaodichgiaonhanct>();
}
