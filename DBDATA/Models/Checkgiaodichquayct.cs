using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Checkgiaodichquayct
{
    public string Magiaodichpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string Tensieuthi { get; set; } = null!;

    public decimal Dongia { get; set; }

    public decimal Soluong { get; set; }

    public decimal Tienck { get; set; }

    public decimal Thanhtien { get; set; }

    public decimal? Soluongnhap { get; set; }
}
