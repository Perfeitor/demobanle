using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Khuyenmai
{
    public string Machuongtrinh { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    /// <summary>
    /// Phân biệt dùng cho bán lẻ hay bán buôn
    /// </summary>
    public int Loaiapdung { get; set; }

    public int Maloaichuongtrinh { get; set; }

    public string? Tenchuongtrinh { get; set; }

    public int? Trangthai { get; set; }

    public int? Trangthaikm { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public int? Giobatdau { get; set; }

    public int? Gioketthuc { get; set; }

    public int? Phutbatdau { get; set; }

    public int? Phutkethuc { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngayphatsinh { get; set; }

    public string? Mact { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public virtual ICollection<Khuyenmaict> Khuyenmaicts { get; set; } = new List<Khuyenmaict>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
