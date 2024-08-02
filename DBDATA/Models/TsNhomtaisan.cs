using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsNhomtaisan
{
    public string Madonvi { get; set; } = null!;

    public string Manhomtaisan { get; set; } = null!;

    public string? Tennhomtaisan { get; set; }

    public string? Maloaitaisan { get; set; }

    public int Loaihang { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Nguoitao { get; set; }

    public int? Issudung { get; set; }

    public virtual ICollection<TsGiaodichct> TsGiaodichcts { get; set; } = new List<TsGiaodichct>();

    public virtual TsLoaitaisan? TsLoaitaisan { get; set; }

    public virtual ICollection<TsTaisan> TsTaisans { get; set; } = new List<TsTaisan>();
}
