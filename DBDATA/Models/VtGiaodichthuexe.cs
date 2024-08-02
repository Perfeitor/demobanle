using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtGiaodichthuexe
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodich { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public DateTime Tungay { get; set; }

    public DateTime Denngay { get; set; }

    public string Tuyenduong { get; set; } = null!;

    public string Maloaithechap { get; set; } = null!;

    public string? Loaithechapso { get; set; }

    public string? Ghichuloaithechap { get; set; }

    public string Mahinhthucthanhtoan { get; set; } = null!;

    public string? Ghichu { get; set; }

    public int Trangthai { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }
}
