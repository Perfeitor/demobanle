﻿using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VtGiaodichthuexect
{
    public string Madonvi { get; set; } = null!;

    public string Magiaodich { get; set; } = null!;

    public string Maxe { get; set; } = null!;

    public decimal Dongia { get; set; }

    public decimal DongiaphutroitheoKm { get; set; }

    public DateTime Ngaygiaoxe { get; set; }

    public int Giogiaoxe { get; set; }

    public int Phutgiaoxe { get; set; }

    public DateTime Ngaytraxe { get; set; }

    public int Giotraxe { get; set; }

    public int Phuttraxe { get; set; }

    public int Gioquydinhtra { get; set; }

    public int Giovuot { get; set; }

    public decimal DongiaphutroitheoGio { get; set; }

    public int Kmdi { get; set; }

    public int Kmve { get; set; }

    public int SoKmdi { get; set; }

    public int SoKmgps { get; set; }

    public int SoKmvuot { get; set; }

    public decimal Tienthuexe { get; set; }

    public decimal TienvuotKm { get; set; }

    public decimal Tienquagio { get; set; }

    public decimal Tongtien { get; set; }
}
