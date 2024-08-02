using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class KhohangChungtu
{
    public string Madonvi { get; set; } = null!;

    public string Makhohang { get; set; } = null!;

    public string Ma { get; set; } = null!;

    public bool? Trangthai { get; set; }

    public string Loai { get; set; } = null!;
}
