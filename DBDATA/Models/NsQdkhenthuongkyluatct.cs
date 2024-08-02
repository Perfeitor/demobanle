using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsQdkhenthuongkyluatct
{
    public string? Maquyetdinh { get; set; }

    public string? Makhenthuongkyluat { get; set; }

    public decimal? Sotien { get; set; }

    public string? Madonvi { get; set; }

    public virtual NsKhenthuongkyluat? NsKhenthuongkyluat { get; set; }

    public virtual NsQdkhenthuongkyluat? NsQdkhenthuongkyluat { get; set; }
}
