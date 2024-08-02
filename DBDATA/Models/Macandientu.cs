using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Macandientu
{
    public string Masieuthi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Itemcode { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Mathang Mathang { get; set; } = null!;
}
