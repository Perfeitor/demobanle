using System;
using System.Collections.Generic;

namespace DBDATA.Models;

public partial class NsLichlamviecctthang
{
    public string? Malichlamviecthang { get; set; }

    public DateTime? Ngaylamviec { get; set; }

    public string? Mathutrongtuan { get; set; }

    public string? Macalamviec { get; set; }

    public string? Madonvi { get; set; }

    public virtual NsCalamviec? NsCalamviec { get; set; }

    public virtual NsLichlamviecthang? NsLichlamviecthang { get; set; }
}
