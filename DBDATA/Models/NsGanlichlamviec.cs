using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsGanlichlamviec
{
    public string Maganlichlamviec { get; set; } = null!;

    public string? Malichlamviec { get; set; }

    public string? Manhanvien { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public int? Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual NsLichlamviec? NsLichlamviec { get; set; }
}
