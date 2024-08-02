using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Khohang
{
    public string Makhohang { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public string Maloaikho { get; set; } = null!;

    public string Tenkhohang { get; set; } = null!;

    public string? Diachi { get; set; }

    public DateTime Ngaytao { get; set; }

    public string? Ghichu { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string? Tendangnhapsua { get; set; }

    public string? MaptnxTienmat { get; set; }

    public string? MaptnxThe { get; set; }

    public string? Dienthoai { get; set; }

    public string? Makhoxuat { get; set; }

    public string? Tenhienthihoadon { get; set; }

    public string? Manganhang { get; set; }

    public string? Taikhoannganhang { get; set; }

    public string? Tentaikhoan { get; set; }

    public int? HienthiQr { get; set; }

    public virtual ICollection<Giaodichquayct> Giaodichquaycts { get; set; } = new List<Giaodichquayct>();

    public virtual ICollection<Giaodichquaydodangct> Giaodichquaydodangcts { get; set; } = new List<Giaodichquaydodangct>();

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
