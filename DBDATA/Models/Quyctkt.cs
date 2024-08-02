using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Quyctkt
{
    public string Mactktpk { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Kieuct { get; set; }

    public string Mactu { get; set; } = null!;

    public string Soctkt { get; set; } = null!;

    public DateTime Ngayctkt { get; set; }

    public string? Kemtheo { get; set; }

    public string? Ghichu { get; set; }

    public int Trangthai { get; set; }

    public DateTime Ngaytao { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public DateTime? Ngayhoadon { get; set; }

    public string? Sohoadon { get; set; }

    public string? Kyhieuhoadon { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string? Nguoinoptien { get; set; }

    public string? Mangoaite { get; set; }

    public decimal? Tygia { get; set; }

    public string? Magiaodichphu { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<Quyctktct> Quyctktcts { get; set; } = new List<Quyctktct>();
}
