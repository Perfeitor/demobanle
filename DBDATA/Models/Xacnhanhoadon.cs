using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Xacnhanhoadon
{
    public string Magiaodich { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Sohoadon { get; set; }

    public decimal? Tongtienhoadon { get; set; }

    public int? Trangthaikt { get; set; }

    public string? Manguoixacnhan { get; set; }

    public DateTime? Ngayxacnhan { get; set; }

    public string? Manhomptnx { get; set; }

    public string? Manvgiaohang { get; set; }

    public string? Manvlaixe { get; set; }
}
