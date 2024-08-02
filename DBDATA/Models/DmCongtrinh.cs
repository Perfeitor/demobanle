using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class DmCongtrinh
{
    public string Madonvi { get; set; } = null!;

    public string Ma { get; set; } = null!;

    public string? Ten { get; set; }

    public string? Maloaicongtrinh { get; set; }

    public int? Trangthai { get; set; }

    public decimal? Dutoan { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public string? Makhachhang { get; set; }

    public string? Diachi { get; set; }

    public string? Ghichu { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaysua { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }

    public int? Sort { get; set; }

    public virtual DmLoaicongtrinh? DmLoaicongtrinh { get; set; }
}
