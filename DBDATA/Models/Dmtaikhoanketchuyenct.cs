using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmtaikhoanketchuyenct
{
    public string Matkketchuyen { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Matkno { get; set; } = null!;

    public string Matkco { get; set; } = null!;

    public string Tinhchat { get; set; } = null!;

    public int Trangthaisudung { get; set; }

    public int Thutu { get; set; }

    public string? Ghichu { get; set; }
}
