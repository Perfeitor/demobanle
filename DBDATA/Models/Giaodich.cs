﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Giaodich
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maptnx { get; set; } = null!;

    public string? Ghichu { get; set; }

    public int? Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string? Sochungtugoc { get; set; }

    public DateTime? Ngaychungtugoc { get; set; }

    public string? Kemtheo { get; set; }

    public decimal? Tiendathanhtoan { get; set; }

    public string? Mahopdong { get; set; }

    public DateTime? Ngaythanhtoan { get; set; }

    public DateTime? Ngayhoadon { get; set; }

    public string? Sohoadon { get; set; }

    public string? Kyhieuhoadon { get; set; }

    public string? Magiaodichphu { get; set; }

    public string? Makhachhang { get; set; }

    public string? Diachigiaohang { get; set; }

    public string? Manhanviencongno { get; set; }

    public string? Manhanviendathang { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? Nguoigiaohang { get; set; }

    public DateTime Ngayphatsinh { get; set; }

    public string? Maquay { get; set; }

    public int? Solaninhoadon { get; set; }

    public int? Trangthaidonhang { get; set; }

    public string? Mucdouutien { get; set; }

    public string? Mangoaite { get; set; }

    public decimal? Tygia { get; set; }

    public int? Trangthaick { get; set; }

    public int? Trangthaitinhdoanhso { get; set; }

    public int? Trangthaickthunglon { get; set; }

    public string? Mausohoadon { get; set; }

    public virtual Dmptnx Dmptnx { get; set; } = null!;
}
