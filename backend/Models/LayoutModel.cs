namespace backend.Models
{
    public class LayoutModel
    {
        public class ResultMenu
        {
            public int nMenuID { get; set; }
            public string sMenuName { get; set; } = string.Empty;
            public bool isHaveSub { get; set; } = false;
            public int? nHeadMenu { get; set; } = 0;
            public string? sIcon { get; set; } = string.Empty;
            public string? sURL {  get; set; } = string.Empty;
            public List<ResultMenu> lstSubmenu { get; set; }
        }
        // public List<ResultMenu> lstMenu { get; set; }
    }
}
