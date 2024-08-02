using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsKhauhao
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public DateOnly? Ngayhoachtoan { get; set; }

    public string? Diengiai { get; set; }

    public string? Manvlapbieu { get; set; }

    public int? Trangthai { get; set; }

    public string? Maptnx { get; set; }

    public string? Manhomptnx { get; set; }

    public DateOnly? Tungay { get; set; }

    public DateOnly? Denngay { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Nguoitao { get; set; }

    public virtual ICollection<TsKhauhaoct> TsKhauhaocts { get; set; } = new List<TsKhauhaoct>();
}
