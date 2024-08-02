using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nguoidung
{
    public string Madonvi { get; set; } = null!;

    public string Tendangnhap { get; set; } = null!;

    public string Manhomquyen { get; set; } = null!;

    public string Hovaten { get; set; } = null!;

    public int? Trangthai { get; set; }

    public string Matkhau { get; set; } = null!;

    public string? Sodienthoai { get; set; }

    public string? Email { get; set; }

    public string? Manguoitao { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string? Manguoisua { get; set; }

    public string? Manvcongno { get; set; }

    public string? Trangthaiof { get; set; }

    public string? Makhohang { get; set; }

    public virtual ICollection<Donvitinh> Donvitinhs { get; set; } = new List<Donvitinh>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Nhomhang> Nhomhangs { get; set; } = new List<Nhomhang>();
}
