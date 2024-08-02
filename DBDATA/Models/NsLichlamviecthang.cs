using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLichlamviecthang
{
    public string Malichlamviecthang { get; set; } = null!;

    public string? Tenlichlamviecthang { get; set; }

    public string Manhanvien { get; set; } = null!;

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public int? Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Nhanvien Nhanvien { get; set; } = null!;
}
