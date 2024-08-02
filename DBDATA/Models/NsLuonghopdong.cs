using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLuonghopdong
{
    public string Maluonghopdong { get; set; } = null!;

    public string? Tenluonghopdong { get; set; }

    public int? Sothang { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsLuonghopdongbophan> NsLuonghopdongbophans { get; set; } = new List<NsLuonghopdongbophan>();

    public virtual ICollection<NsLuonghopdongnhanvien> NsLuonghopdongnhanviens { get; set; } = new List<NsLuonghopdongnhanvien>();
}
