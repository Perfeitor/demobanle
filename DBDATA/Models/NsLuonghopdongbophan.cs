﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLuonghopdongbophan
{
    public string Maluonghopdong { get; set; } = null!;

    public string Mabophan { get; set; } = null!;

    public decimal? Sotien { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual NsBophan NsBophan { get; set; } = null!;

    public virtual NsLuonghopdong NsLuonghopdong { get; set; } = null!;
}
