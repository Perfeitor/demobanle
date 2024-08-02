using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichdongop
{
    public string Magiaodichdongop { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Manvgiaohang { get; set; }

    public string? Matuyen { get; set; }

    public string? Goptheoin { get; set; }

    public string? Nguoigop { get; set; }

    public string? Nguoisua { get; set; }

    public DateTime? Ngaygopdon { get; set; }

    public decimal? Tongsl { get; set; }

    public decimal? Tongst { get; set; }

    public decimal? Tongthantien { get; set; }

    public int? Solanin { get; set; }

    public int? Trangthaidonhang { get; set; }

    public string? Ghichu { get; set; }

    public string? Malichxe { get; set; }

    public string? Manvlaixe { get; set; }
}
