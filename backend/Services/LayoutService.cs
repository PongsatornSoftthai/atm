using backend.EF.Atm;
using static backend.Models.CommonModel1;
using static backend.Models.LayoutModel;

namespace backend.Services
{
    public interface ILayoutService
    {

        ResultApi GetMenu();
    }
    public class LayoutService : ILayoutService
    {
        private readonly WebAppEntity _db;
        public LayoutService()
        {
            _db = new WebAppEntity();
        }
        public ResultApi GetMenu()
        {

            ResultApi result = new ResultApi();
            ResultMenu resultMenu = new ResultMenu();
            var lstData = _db.TmMenu.Select(s => new ResultMenu() {
                nMenuID = s.nMenuID,
                sMenuName = s.sMenuName+"",

            }).ToList();
            result.objResult = lstData;
            return result;
        }

    }
}
