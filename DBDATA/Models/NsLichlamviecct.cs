using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLichlamviecct
{
    public string? Malichlamviec { get; set; }

    public string? Mathutrongtuan { get; set; }

    public string? Macalamviec { get; set; }

    public string? Madonvi { get; set; }

    public virtual NsCalamviec? NsCalamviec { get; set; }

    public virtual NsLichlamviec? NsLichlamviec { get; set; }
}
