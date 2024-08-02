using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsLoaitaisan
{
    public string Madonvi { get; set; } = null!;

    public string Maloaitaisan { get; set; } = null!;

    public string? Tenloaitaisan { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Nguoitao { get; set; }

    public int? Issudung { get; set; }

    public int Loaihang { get; set; }

    public virtual ICollection<TsGiaodichct> TsGiaodichcts { get; set; } = new List<TsGiaodichct>();

    public virtual ICollection<TsNhomtaisan> TsNhomtaisans { get; set; } = new List<TsNhomtaisan>();

    public virtual ICollection<TsTaisan> TsTaisans { get; set; } = new List<TsTaisan>();
}
