using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TbTransactionHistory
{
    public int nHistoryID { get; set; }

    public int nCustomerID { get; set; }

    public int nItemType { get; set; }

    public decimal? nAmount { get; set; }

    public decimal? nRemainingAmount { get; set; }

    public int? nCountThousand { get; set; }

    public int? nCountFiveHundred { get; set; }

    public int? nCountHundred { get; set; }

    public int nStatusID { get; set; }

    public string? sRemark { get; set; }

    public DateTime? dCreate { get; set; }
}
