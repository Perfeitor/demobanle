﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsGiaodichct
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public string Mataisan { get; set; } = null!;

    public string? Sohieutaisan { get; set; }

    public string? Tentaisan { get; set; }

    public string? Mangoaite { get; set; }

    public decimal? Tygia { get; set; }

    public decimal? Soluong { get; set; }

    public decimal? Nguyengia { get; set; }

    public decimal? Thanhtien { get; set; }

    public decimal? Dongia { get; set; }

    public string? Vatdb { get; set; }

    public decimal? Tienvatdb { get; set; }

    public int? Sort { get; set; }

    public string? Matkno { get; set; }

    public string? Matkco { get; set; }

    public string? Matkthuenkno { get; set; }

    public string? Matkthuenkco { get; set; }

    public string? Matkthuedbno { get; set; }

    public string? Matkthuedbco { get; set; }

    public string? Matkthuevatno { get; set; }

    public string? Matkthuevatco { get; set; }

    public string? Madvtinh { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Makhonhap { get; set; }

    public string? Makhoxuat { get; set; }

    public string? Maloaitaisan { get; set; }

    public string? Manhomtaisan { get; set; }

    public decimal? Quycach { get; set; }

    public string? Ctdoituong { get; set; }

    public string? Ctdoituongno { get; set; }

    public string? Ctdoituongco { get; set; }

    public virtual TsGiaodich TsGiaodich { get; set; } = null!;

    public virtual TsLoaitaisan? TsLoaitaisan { get; set; }

    public virtual TsNhomtaisan? TsNhomtaisan { get; set; }

    public virtual TsTaisan TsTaisan { get; set; } = null!;
}
