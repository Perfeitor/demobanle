using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBangluongthuong
{
    public string? Mabangluong { get; set; }

    public string? Manhanvien { get; set; }

    public string? Makhenthuongkyluat { get; set; }

    public decimal? Sotien { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? Madonvi { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual NsBangluong? NsBangluong { get; set; }

    public virtual NsKhenthuongkyluat? NsKhenthuongkyluat { get; set; }
}
