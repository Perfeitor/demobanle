using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Tkdoituong
{
    public string Madonvi { get; set; } = null!;

    public string Matk { get; set; } = null!;

    public string Madoituongtheodoi { get; set; } = null!;

    public bool Trangthaitheodoi { get; set; }

    public bool Trangthaibatbuoc { get; set; }
}
