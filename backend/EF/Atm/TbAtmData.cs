using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TbAtmData
{
    public int nAtmID { get; set; }

    public string sAtmName { get; set; } = null!;

    public decimal? nTotalAmount { get; set; }

    public int? nCountThousand { get; set; }

    public int? nCountFiveHundred { get; set; }

    public int? nCountHundred { get; set; }

    public bool isActive { get; set; }

    public DateTime? dCreate { get; set; }

    public int? nCreateID { get; set; }

    public DateTime? dUpdate { get; set; }

    public int? nUpdateID { get; set; }
}
