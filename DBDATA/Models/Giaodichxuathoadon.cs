using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodichxuathoadon
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Nguoimuahang { get; set; }

    public string? Dienthoai { get; set; }

    public string? Ghichu { get; set; }

    public DateTime? Ngaytra { get; set; }
}
