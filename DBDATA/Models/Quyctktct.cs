﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Quyctktct
{
    public string Id { get; set; } = null!;

    public string Mactktpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Ghichu { get; set; }

    public string Matkno { get; set; } = null!;

    public string Matkco { get; set; } = null!;

    public decimal Sotien { get; set; }

    public string? Makhachhang { get; set; }

    public string? Makhachhangco { get; set; }

    public string? Mavuviec { get; set; }

    public string? Manhanviencongno { get; set; }

    public string? Manhanviencongnoco { get; set; }

    public string? Manhanviengiaohang { get; set; }

    public decimal Tienvat { get; set; }

    public string? Mavat { get; set; }

    public string? Makmchiphi { get; set; }

    public string? Magiaodichpk { get; set; }

    public decimal? Sotiendadoitru { get; set; }

    public string? Taikhoan { get; set; }

    public decimal? Sotiennt { get; set; }

    public decimal? Tienvatnt { get; set; }

    public decimal? Sotiendadoitrunt { get; set; }

    public string? Madoituongtaphop { get; set; }

    public string? Taikhoannhan { get; set; }

    public decimal? Tyleckthanhtoanngay { get; set; }

    public string? Macongtrinh { get; set; }

    public string? Mahangmuc { get; set; }

    public string? Makheuoc { get; set; }

    public virtual Quyctkt MactktpkNavigation { get; set; } = null!;
}
