using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomkmchiphi
{
    public string Manhomkmchiphi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tennhomkmchiphi { get; set; } = null!;

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public int Trangthaisudung { get; set; }

    public virtual ICollection<Kmchiphi> Kmchiphis { get; set; } = new List<Kmchiphi>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
