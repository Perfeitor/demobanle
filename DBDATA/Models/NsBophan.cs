using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsBophan
{
    public string Mabophan { get; set; } = null!;

    public string? Tenbophan { get; set; }

    public int? Trangthaisudung { get; set; }

    public int? Trangthaiphat { get; set; }

    public decimal? Sogiolamtrenca { get; set; }

    public string? Maphongban { get; set; }

    public string Madonvi { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Tendangnhapsua { get; set; }

    public virtual Donvi MadonviNavigation { get; set; } = null!;

    public virtual ICollection<NsBangchamcong> NsBangchamcongs { get; set; } = new List<NsBangchamcong>();

    public virtual ICollection<NsBangluong> NsBangluongs { get; set; } = new List<NsBangluong>();

    public virtual ICollection<NsChuyenbophan> NsChuyenbophans { get; set; } = new List<NsChuyenbophan>();

    public virtual ICollection<NsCongthuctinhluong> NsCongthuctinhluongs { get; set; } = new List<NsCongthuctinhluong>();

    public virtual ICollection<NsDangkylamthem> NsDangkylamthems { get; set; } = new List<NsDangkylamthem>();

    public virtual ICollection<NsDangkynghi> NsDangkynghis { get; set; } = new List<NsDangkynghi>();

    public virtual ICollection<NsDinhmucluong> NsDinhmucluongs { get; set; } = new List<NsDinhmucluong>();

    public virtual ICollection<NsLuonghopdongbophan> NsLuonghopdongbophans { get; set; } = new List<NsLuonghopdongbophan>();

    public virtual ICollection<NsNgaynghi> NsNgaynghis { get; set; } = new List<NsNgaynghi>();

    public virtual NsPhongban? NsPhongban { get; set; }
}
