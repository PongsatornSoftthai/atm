using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TmMasterData
{
    public int nMasterID { get; set; }

    public int nTypeID { get; set; }

    public string? MasterName { get; set; }

    public string? sValue { get; set; }

    public decimal? nValue { get; set; }

    public bool isActive { get; set; }
}
