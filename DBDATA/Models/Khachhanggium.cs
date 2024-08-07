﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Khachhanggium
{
    public int Keys { get; set; }

    public string? Makhachhang { get; set; }

    public int Maloaigia { get; set; }

    public string Maapdung { get; set; } = null!;

    public decimal Dongiaban { get; set; }

    public int Trangthaisudung { get; set; }

    public string Madonvi { get; set; } = null!;

    public string? Manhanvien { get; set; }

    public string? Manhomkhachhang { get; set; }

    public decimal? Tyleck { get; set; }
}
