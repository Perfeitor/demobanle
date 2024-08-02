using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhomquyenphu
{
    public string Manhomquyen { get; set; } = null!;

    public string Menuid { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Chucnangphu { get; set; } = null!;

    public bool Trangthai { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nhomquyen Nhomquyen { get; set; } = null!;
}
