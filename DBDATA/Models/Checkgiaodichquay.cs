using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Checkgiaodichquay
{
    public string Magiaodichquay { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Tennguoiban { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }
}
