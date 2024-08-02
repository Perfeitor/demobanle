using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Lichsugiaodichgopdon
{
    public string Magiaodichdongop { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Soctugoc { get; set; } = null!;

    public DateTime? Ngaygop { get; set; }

    public DateTime? Ngaytralai { get; set; }

    public string? Lydotralai { get; set; }

    public string? Nguoigop { get; set; }

    public string? Nguoilamtralai { get; set; }

    public int? Trangthaithanhcong { get; set; }

    public string? Makhachhang { get; set; }

    public string? Manvgiaohang { get; set; }
}
