using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class MartLog
{
    public int Id { get; set; }

    public string Madonvi { get; set; } = null!;

    public string Malog { get; set; } = null!;

    public string Nguoitao { get; set; } = null!;

    public DateTime Ngaytao { get; set; }

    public string Tenmay { get; set; } = null!;

    public string Tenform { get; set; } = null!;

    public string Trangthai { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public string? NoidungShort { get; set; }
}
