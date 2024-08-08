using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TbAtmDataHistory
{
    public int nItemID { get; set; }

    public int nAtmID { get; set; }

    public int nItemTypeID { get; set; }

    public decimal? nAmount { get; set; }

    public int? nCountThousand { get; set; }

    public int? nCountFiveHundred { get; set; }

    public int? nCountHundred { get; set; }

    public DateTime? dCreate { get; set; }

    public int? nUpdateID { get; set; }
}
