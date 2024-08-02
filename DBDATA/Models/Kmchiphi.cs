using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Kmchiphi
{
    public string Makmchiphi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tenkmchiphi { get; set; } = null!;

    public string Manhomkmchiphi { get; set; } = null!;

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhomkmchiphi Nhomkmchiphi { get; set; } = null!;
}
