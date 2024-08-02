using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class TsGiaodich
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodichpk { get; set; } = null!;

    public string? Manhomptnx { get; set; }

    public string? Maptnx { get; set; }

    public string? Sohoadon { get; set; }

    public string? Kyhieuhoadongd { get; set; }

    public DateOnly? Ngayhoadon { get; set; }

    public DateOnly? Ngayhoachtoan { get; set; }

    public string? Ghichu { get; set; }

    public int? Trangthai { get; set; }

    public string? Makhachhang { get; set; }

    public string? Manhacungcap { get; set; }

    public string? Maphongban { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public DateOnly? Ngaysua { get; set; }

    public string? Nguoitao { get; set; }

    public string? Nguoisua { get; set; }

    public int? Sort { get; set; }

    public virtual ICollection<TsGiaodichct> TsGiaodichcts { get; set; } = new List<TsGiaodichct>();
}
