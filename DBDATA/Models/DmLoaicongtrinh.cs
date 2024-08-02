using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class DmLoaicongtrinh
{
    public string Madonvi { get; set; } = null!;

    public string Ma { get; set; } = null!;

    public string? Ten { get; set; }

    public string? Ghichu { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Nguoitao { get; set; }

    public DateTime? Ngaysua { get; set; }

    public string? Nguoisua { get; set; }

    public int? Trangthai { get; set; }

    public virtual ICollection<DmCongtrinh> DmCongtrinhs { get; set; } = new List<DmCongtrinh>();
}
