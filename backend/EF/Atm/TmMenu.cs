using System;
using System.Collections.Generic;

namespace backend.EF.Atm;

public partial class TmMenu
{
    public int nMenuID { get; set; }

    public string? sMenuName { get; set; }

    public bool? IsHaveSub { get; set; }

    public int? nHeadMenu { get; set; }

    public string? sIcon { get; set; }

    public string? sURL { get; set; }

    public int? nSortOrder { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TmMenu> InversenHeadMenuNavigation { get; set; } = new List<TmMenu>();

    public virtual TmMenu? nHeadMenuNavigation { get; set; }
}
