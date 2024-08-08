using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TbCustomer
{
    public int nCustomerID { get; set; }

    public string sCustomerCode { get; set; } = null!;

    public string sSecurityCoden { get; set; } = null!;

    public int nTitleID { get; set; }

    public string sFname { get; set; } = null!;

    public string sLname { get; set; } = null!;

    public string sCardID { get; set; } = null!;

    public string sPhone { get; set; } = null!;

    public string sAddress { get; set; } = null!;

    public string sProfileUrl { get; set; } = null!;

    public string sEmail { get; set; } = null!;

    public bool isActive { get; set; }

    public bool isDel { get; set; }

    public DateTime? dCreate { get; set; }

    public int? nCreateID { get; set; }

    public DateTime? dUpdate { get; set; }

    public int? nUpdateID { get; set; }

    public decimal? nTotalAmount { get; set; }
}
