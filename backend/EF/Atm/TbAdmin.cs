using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TbAdmin
{
    public int nUserID { get; set; }

    public string sUserName { get; set; } = null!;

    public string sSecurityCode { get; set; } = null!;

    public int nTitleID { get; set; }

    public string sFname { get; set; } = null!;

    public string sLname { get; set; } = null!;

    public int nPositionID { get; set; }

    public string? sPhone { get; set; }

    public DateTime? dCreate { get; set; }

    public int? nCreateID { get; set; }

    public DateTime? dUpdate { get; set; }

    public int? nUpdateID { get; set; }

    public bool isActive { get; set; }

    public bool isDel { get; set; }
}
