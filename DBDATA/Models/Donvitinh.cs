using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Donvitinh
{
    public string Madvtinh { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string Donvile { get; set; } = null!;

    public string Donvilon { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    public virtual ICollection<Mathangthue> Mathangthues { get; set; } = new List<Mathangthue>();

    public virtual Nguoidung Nguoidung { get; set; } = null!;
}
