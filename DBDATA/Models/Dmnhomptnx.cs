using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class Dmnhomptnx
{
    public string Manhomptnx { get; set; } = null!;

    public string? Tennhomptnx { get; set; }

    public string? Ghichu { get; set; }

    public bool? Trangthai { get; set; }

    public virtual ICollection<Dmptnx> Dmptnxes { get; set; } = new List<Dmptnx>();
}
