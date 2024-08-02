using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class VXdTonghopcongviecphatsinh
{
    public string Madonvi { get; set; } = null!;

    public string? Macongtrinh { get; set; }

    public string? Tencongtrinh { get; set; }

    public string Mahangmuc { get; set; } = null!;

    public string Masieuthi { get; set; } = null!;

    public string? Tendaydu { get; set; }

    public decimal? Soluong { get; set; }
}
