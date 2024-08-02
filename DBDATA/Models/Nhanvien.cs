using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Nhanvien
{
    public string Manhanvien { get; set; } = null!;

    public string Tennhanvien { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string? Manhomkhachhang { get; set; }

    public string? Dienthoai { get; set; }

    public string? Dienthoai2 { get; set; }

    public string? Email { get; set; }

    public DateTime? Ngaynhanviec { get; set; }

    public DateTime Ngaytao { get; set; }

    public DateOnly? Ngaysinh { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? Cmnd { get; set; }

    public DateTime? Ngaycap { get; set; }

    public string? Noicap { get; set; }

    public int? Gioitinh { get; set; }

    public int? Trangthaisudung { get; set; }

    public string? Machamcong { get; set; }

    public string? Maphongban { get; set; }

    public string? Mabophan { get; set; }

    public string? Machucvu { get; set; }

    public string? Tcsonha { get; set; }

    public string? Tcthonxom { get; set; }

    public string? Tcxaphuong { get; set; }

    public string? Tcquanhuyen { get; set; }

    public string? Tctinhtp { get; set; }

    public string? Htsonha { get; set; }

    public string? Htthonxom { get; set; }

    public string? Htxaphuong { get; set; }

    public string? Htquanhuyen { get; set; }

    public string? Httinhtp { get; set; }

    public string? Masothue { get; set; }

    public int? Madongbo { get; set; }

    public string? Manhanvienquanly { get; set; }

    public virtual ICollection<NsBaohiemnhanvien> NsBaohiemnhanviens { get; set; } = new List<NsBaohiemnhanvien>();

    public virtual ICollection<NsChuyenbophan> NsChuyenbophans { get; set; } = new List<NsChuyenbophan>();

    public virtual ICollection<NsDangkylamthem> NsDangkylamthems { get; set; } = new List<NsDangkylamthem>();

    public virtual ICollection<NsDangkynghi> NsDangkynghis { get; set; } = new List<NsDangkynghi>();

    public virtual ICollection<NsGanlichlamviec> NsGanlichlamviecs { get; set; } = new List<NsGanlichlamviec>();

    public virtual ICollection<NsLichlamviecthang> NsLichlamviecthangs { get; set; } = new List<NsLichlamviecthang>();

    public virtual ICollection<NsLuonghopdongnhanvien> NsLuonghopdongnhanviens { get; set; } = new List<NsLuonghopdongnhanvien>();

    public virtual ICollection<NsPhucapnhanvien> NsPhucapnhanviens { get; set; } = new List<NsPhucapnhanvien>();

    public virtual ICollection<NsQdkhenthuongkyluat> NsQdkhenthuongkyluats { get; set; } = new List<NsQdkhenthuongkyluat>();
}
