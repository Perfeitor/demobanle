using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Donvi
{
    public string Madonvi { get; set; } = null!;

    public string? Madonvicha { get; set; }

    public string Tendonvi { get; set; } = null!;

    public string? Dienthoai { get; set; }

    public string? Dienthoai2 { get; set; }

    public string? Fax { get; set; }

    public string? Diachi { get; set; }

    public string? Email { get; set; }

    public string? Masothue { get; set; }

    public string? Tendangnhapsua { get; set; }

    public string? Giamdoc { get; set; }

    public string? Ketoantruong { get; set; }

    public string? Nguoibanhang { get; set; }

    public string? Nguoikybieu { get; set; }

    public string? Kyhieuhoadon { get; set; }

    public string? Mausohoadon { get; set; }

    public string? Nganhang { get; set; }

    public string? Chutaikhoan { get; set; }

    public string? Sotaikhoan { get; set; }

    public virtual ICollection<Baogium> Baogia { get; set; } = new List<Baogium>();

    public virtual ICollection<Baogiact> Baogiacts { get; set; } = new List<Baogiact>();

    public virtual ICollection<Barcode> Barcodes { get; set; } = new List<Barcode>();

    public virtual ICollection<Bohangct> Bohangcts { get; set; } = new List<Bohangct>();

    public virtual ICollection<Bohang> Bohangs { get; set; } = new List<Bohang>();

    public virtual ICollection<Dathangct> Dathangcts { get; set; } = new List<Dathangct>();

    public virtual ICollection<Dathang> Dathangs { get; set; } = new List<Dathang>();

    public virtual ICollection<Dmchungtu> Dmchungtus { get; set; } = new List<Dmchungtu>();

    public virtual ICollection<Dmptnx> Dmptnxes { get; set; } = new List<Dmptnx>();

    public virtual ICollection<Dmtk> Dmtks { get; set; } = new List<Dmtk>();

    public virtual ICollection<Donvitinh> Donvitinhs { get; set; } = new List<Donvitinh>();

    public virtual ICollection<Giaodichquaydodang> Giaodichquaydodangs { get; set; } = new List<Giaodichquaydodang>();

    public virtual ICollection<Giaodichquay> Giaodichquays { get; set; } = new List<Giaodichquay>();

    public virtual ICollection<Khachhang> Khachhangs { get; set; } = new List<Khachhang>();

    public virtual ICollection<Khohang> Khohangs { get; set; } = new List<Khohang>();

    public virtual ICollection<Khuyenmaict> Khuyenmaicts { get; set; } = new List<Khuyenmaict>();

    public virtual ICollection<Khuyenmai> Khuyenmais { get; set; } = new List<Khuyenmai>();

    public virtual ICollection<Kiemke> Kiemkes { get; set; } = new List<Kiemke>();

    public virtual ICollection<Kmchiphi> Kmchiphis { get; set; } = new List<Kmchiphi>();

    public virtual ICollection<Macandientu> Macandientus { get; set; } = new List<Macandientu>();

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    public virtual ICollection<Mathangthue> Mathangthues { get; set; } = new List<Mathangthue>();

    public virtual ICollection<Menunhomquyen> Menunhomquyens { get; set; } = new List<Menunhomquyen>();

    public virtual ICollection<Nganhhang> Nganhhangs { get; set; } = new List<Nganhhang>();

    public virtual ICollection<Nguoidung> Nguoidungs { get; set; } = new List<Nguoidung>();

    public virtual ICollection<Nhomhang> Nhomhangs { get; set; } = new List<Nhomhang>();

    public virtual ICollection<Nhomkhachhang> Nhomkhachhangs { get; set; } = new List<Nhomkhachhang>();

    public virtual ICollection<Nhomkmchiphi> Nhomkmchiphis { get; set; } = new List<Nhomkmchiphi>();

    public virtual ICollection<Nhomquyenphu> Nhomquyenphus { get; set; } = new List<Nhomquyenphu>();

    public virtual ICollection<Nhomquyen> Nhomquyens { get; set; } = new List<Nhomquyen>();

    public virtual ICollection<NsBangluong> NsBangluongs { get; set; } = new List<NsBangluong>();

    public virtual ICollection<NsBaohiem> NsBaohiems { get; set; } = new List<NsBaohiem>();

    public virtual ICollection<NsBophan> NsBophans { get; set; } = new List<NsBophan>();

    public virtual ICollection<NsCalamviec> NsCalamviecs { get; set; } = new List<NsCalamviec>();

    public virtual ICollection<NsChucvu> NsChucvus { get; set; } = new List<NsChucvu>();

    public virtual ICollection<NsChuyenbophan> NsChuyenbophans { get; set; } = new List<NsChuyenbophan>();

    public virtual ICollection<NsCongthuctinhluong> NsCongthuctinhluongs { get; set; } = new List<NsCongthuctinhluong>();

    public virtual ICollection<NsDangkylamthem> NsDangkylamthems { get; set; } = new List<NsDangkylamthem>();

    public virtual ICollection<NsDangkynghi> NsDangkynghis { get; set; } = new List<NsDangkynghi>();

    public virtual ICollection<NsDinhmucluong> NsDinhmucluongs { get; set; } = new List<NsDinhmucluong>();

    public virtual ICollection<NsKhenthuongkyluat> NsKhenthuongkyluats { get; set; } = new List<NsKhenthuongkyluat>();

    public virtual ICollection<NsLichlamviec> NsLichlamviecs { get; set; } = new List<NsLichlamviec>();

    public virtual ICollection<NsLienketnhanvien> NsLienketnhanviens { get; set; } = new List<NsLienketnhanvien>();

    public virtual ICollection<NsLuongcoban> NsLuongcobans { get; set; } = new List<NsLuongcoban>();

    public virtual ICollection<NsLuonghopdongbophan> NsLuonghopdongbophans { get; set; } = new List<NsLuonghopdongbophan>();

    public virtual ICollection<NsLuonghopdongnhanvien> NsLuonghopdongnhanviens { get; set; } = new List<NsLuonghopdongnhanvien>();

    public virtual ICollection<NsLuonghopdong> NsLuonghopdongs { get; set; } = new List<NsLuonghopdong>();

    public virtual ICollection<NsNgaynghi> NsNgaynghis { get; set; } = new List<NsNgaynghi>();

    public virtual ICollection<NsPhongban> NsPhongbans { get; set; } = new List<NsPhongban>();

    public virtual ICollection<NsPhucapnhanvien> NsPhucapnhanviens { get; set; } = new List<NsPhucapnhanvien>();

    public virtual ICollection<NsPhucap> NsPhucaps { get; set; } = new List<NsPhucap>();

    public virtual ICollection<NsQdkhenthuongkyluat> NsQdkhenthuongkyluats { get; set; } = new List<NsQdkhenthuongkyluat>();

    public virtual ICollection<Quayhang> Quayhangs { get; set; } = new List<Quayhang>();

    public virtual ICollection<Quyctkt> Quyctkts { get; set; } = new List<Quyctkt>();

    public virtual ICollection<SxDenghilinhlieu> SxDenghilinhlieus { get; set; } = new List<SxDenghilinhlieu>();

    public virtual ICollection<Vuviec> Vuviecs { get; set; } = new List<Vuviec>();
}
