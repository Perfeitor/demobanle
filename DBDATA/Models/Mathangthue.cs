using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Mathangthue
{
    public string Masieuthi { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Manganh { get; set; } = null!;

    public string Manhomhang { get; set; } = null!;

    public string Makhachhang { get; set; } = null!;

    public string Madvtinh { get; set; } = null!;

    public string? Tendaydu { get; set; }

    public string? Tenviettat { get; set; }

    public string? Mahangcuancc { get; set; }

    public string? Mavatmua { get; set; }

    public string? Mavatban { get; set; }

    public int? Trangthaikd { get; set; }

    public DateTime Ngaytao { get; set; }

    public string? Barcode { get; set; }

    public decimal? Quycach { get; set; }

    public decimal? Giabanlecovat { get; set; }

    public decimal? Giabanbuoncovat { get; set; }

    public decimal? Giabanlechuavat { get; set; }

    public decimal? Giabanbuonchuavat { get; set; }

    public decimal? Giamuacovat { get; set; }

    public decimal? Giamuachuavat { get; set; }

    public decimal? Tylelaibuon { get; set; }

    public decimal? Tylelaile { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Itemcode { get; set; }

    public bool? QuanlySerial { get; set; }

    public string? Thoigianbaohanh { get; set; }

    public string? Kieubaohanh { get; set; }

    public string? Makehang { get; set; }

    public int? Madongbo { get; set; }

    public decimal? Hieusuat { get; set; }

    public string? Mavatnk { get; set; }

    public string? Mahaiquan { get; set; }

    public string? Mota { get; set; }

    public string? Mahs { get; set; }

    public string? Mactpk { get; set; }

    public string? Maplv { get; set; }

    public decimal? Tyleckmua { get; set; }

    public decimal? Tyleckban { get; set; }

    public int? Trangthaitruton { get; set; }

    public virtual Donvitinh Donvitinh { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual Nganhhang Nganhhang { get; set; } = null!;

    public virtual Nhomhang Nhomhang { get; set; } = null!;
}
