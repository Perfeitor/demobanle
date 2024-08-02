using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Barcode
{
    public string Masieuthi { get; set; } = null!;

    public string Barcode1 { get; set; } = null!;

    public string Madonvi { get; set; } = null!;

    public virtual Donvi MadonviNavigation { get; set; } = null!;
}
