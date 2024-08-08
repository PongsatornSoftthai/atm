using System.Data;
using System.Text.Json;

namespace backend.Models
{
    #region Upload File
    /// <summary>
    /// </summary>
    public class ItemFileData
    {
        /// <summary>
        /// </summary>
        public int nID { get; set; }

        /// <summary>
        /// </summary>
        public string? sSaveToFileName { get; set; }

        /// <summary>
        /// </summary>
        public string? sSysFileName { get; set; }

        /// <summary>
        /// </summary>
        public string? sFileName { get; set; }

        /// <summary>
        /// </summary>
        public string? sFolderName { get; set; }

        /// <summary>
        /// </summary>
        public string? sSaveToPath { get; set; }

        /// <summary>
        /// </summary>
        public string? sSize { get; set; }

        /// <summary>
        /// </summary>
        public string? sUrl { get; set; }

        /// <summary>
        /// </summary>
        public bool IsNewFile { get; set; }

        /// <summary>
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// </summary>
        public bool IsProgress { get; set; }

        /// <summary>
        /// </summary>
        public string? sMsg { get; set; }

        /// <summary>
        /// </summary>
        public string? sProgress { get; set; }

        /// <summary>
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// </summary>
        public string? sFileType { get; set; }

        /// <summary>
        /// </summary>
        public bool IsNewTab { get; set; }

        /// <summary>
        /// </summary>
        public string? sFileLink { get; set; }

        /// <summary>
        /// </summary>
        public string? sPath { get; set; }

        /// <summary>
        /// </summary>
        public int? nSizeName { get; set; } //cal Size

        /// <summary>
        /// </summary>
        public string? sCropFileLink { get; set; }

        /// <summary>
        /// </summary>
        public string? sCropPath { get; set; }

        /// <summary>
        /// </summary>
        public string? sDescription { get; set; }
    }
    /// <summary>
    /// </summary>
    public class CParamCrop
    {
        /// <summary>
        /// <summary>
        public string? sOldPath { get; set; }

        /// <summary>
        /// </summary>
        public string? sBase64 { get; set; }
    }


    #endregion

    #region remove
    /// <summary>
    /// </summary>
    public class RequestRemove
    {
        /// <summary>
        /// </summary>
        public List<string>? lstRemove { get; set; }

        /// <summary>
        /// </summary>
        public string? sID { get; set; }
    }
    #endregion

    #region import excel
    public class CResultReadFile : ResultApi
    {
        public DataSet? ds { get; set; }

        public DataTable? dt { get; set; }

        public List<string>? lstError { get; set; }
        public string? sMsg { get; set; }
    }
    #endregion

    public class ResultApi
    {
        public int nStatusCode { get; set; } = StatusCodes.Status200OK;

        public string? sMessage { get; set; }

        public object? objResult { get; set; }

        public object? objResultHome { get; set; }
    }
    public class ErrorDetails
    {
        /// <summary>
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class SelectOption
    {
        public string? label { get; set; }
        public string value { get; set; } = string.Empty;
        public string? sText { get; set; }
        public string? sParentCode { get; set; }
        public string? sParentID { get; set; }
        public int? nParentID { get; set; }
        public int? nLevel { get; set; }
        public int? nOrder { get; set; }
        public string? sFontColor { get; set; }
        public bool? IsParent { get; set; }
        public bool disable { get; set; }
        public bool isCrossUnit { get; set; }
        public string sEmail { get; set; } = string.Empty;
        public string sName { get; set; } = string.Empty;
        public bool isActive { get; set; }
        public int nMin { get; set; }
        public int nMax { get; set; }
        public string? sDepartmentCode { get; set; }
        public string? sDepartmentAbbr { get; set; }
        public int? nValue { get; set; }

        public static implicit operator List<object>(SelectOption v)
        {
            throw new NotImplementedException();
        }
    }

    public class ParamDeleteTable
    {
        public List<string>? lstID { get; set; } = new List<string>();
    }
    public class ResultDefaultTableRow
    {
        public string sID { get; set; } = string.Empty;
        public int? nRowSpan { get; set; }
        public int? nColSpan { get; set; }
    }
    public class cUnit
    {
        public string sUnitCode { get; set; } = string.Empty;
        public string sUnitName { get; set; } = string.Empty;
        public string sUnitAbbr { get; set; } = string.Empty;
        public string sUnitFullName { get; set; } = string.Empty;
    }

    public class StyleChip
    {
        public string color { get; set; } = string.Empty;
        public string backgroundColor { get; set; } = string.Empty;
        public string margin
        {
            get
            {

                return "0.2rem";

            }
        }
        public string fontSize
        {
            get
            {

                return "0.8rem";

            }
        }
    }
    public class ItemDownFile
    {
        public string? sFolderName { get; set; }
        public string? sSysFileName { get; set; }
        public string? sFileName { get; set; }
        public string? sPath { get; set; }
    }

    public class CheckResizeImageParams
    {
        public bool? isResize { get; set; }
        public int? nWidthResize { get; set; }
        public int? nHeigthResize { get; set; }
        public bool? IsCheckRecommendSize { get; set; }
        public string filepath { get; set; } = string.Empty;
        public string sSysFileName { get; set; } = string.Empty;
        public string sMapPath { get; set; } = string.Empty;
    }

    #region Breadcrumbs
    public class Breadcrumbs
    {
        public List<ItemBreadcrumbs> lstBreadcrumbs { get; set; } = new();
        public int nPermission { get; set; }
        public string? sDescription { get; set; }
    }
    public class ItemBreadcrumbs
    {
        /// <summary>
        /// Key
        /// </summary>
        public int nMenuID { get; set; }
        /// <summary>
        /// Item Name
        /// </summary>
        public string? sMenuName { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        public string? sIcon { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string? sRoute { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        public int? nLevel { get; set; }
    }
    public class CreateCalendarEventModel
    {
        public string sMailCreateBy { get; set; } = string.Empty;
        public string sEventSubject { get; set; } = string.Empty;
        public string sDetailEvent { get; set; } = string.Empty;
        public ItemFileData? oAttachment { get; set; }
        public string sStartEventDate { get; set; } = string.Empty;
        public string sEndEventDate { get; set; } = string.Empty;
        public List<string>? lstAttendee { get; set; }

    }

    public class AttendeeUserModel
    {
        public string sEmail { get; set; } = string.Empty;
        public string sName { get; set; } = string.Empty;
    }
    #endregion
    public class ResultDownloadFile
    {
        public byte[] objData { get; set; } = Array.Empty<byte>();
        public string sType { get; set; } = string.Empty;
        public string sName { get; set; } = string.Empty;
    }
    public class Pagination //: ResultApi
    {
        public int nDataLength { get; set; } //nDataCountAll
        public int nPageIndex { get; set; }
        public int nSkip { get; set; } //nSkipData
        public int nTake { get; set; } //nTakeData
        public int nStartIndex { get; set; } //nStartItemIndex
    }
    public class PaginationData : Option
    {
        public string sSearch { get; set; } = "";


        public int nDataLength { get; set; }
    }

    public class Option
    {
        private class SortingDirection
        {
            public static string ASCENDING => "asc";

            public static string DESCENDING => "desc";
        }

        public int nPageSize { get; set; }

        public int nPageIndex { get; set; }

        public string sSortExpression { get; set; } = "";


        public string sSortDirection { get; set; } = "";


        public bool isASC => sSortDirection == SortingDirection.ASCENDING;

        public bool isDESC => sSortDirection == SortingDirection.DESCENDING;
    }

    public class OptionDropdown
    {
        /// <summary>
        /// label
        /// </summary>
        public string label { get; set; } = string.Empty;
        /// <summary>
        /// value
        /// </summary>
        public string value { get; set; } = string.Empty;
        public int nValue { get; set; }
        public string sUrl { get; set; } = "";
        public string sHeadLabel { get; set; } = string.Empty;
        public string sSubLabel { get; set; } = string.Empty;
        public string sSubLabel2 { get; set; } = string.Empty;
        public string sIcon { get; set; } = string.Empty;
    }
    public class SearchAutoComplete
    {
        public string strSearch { get; set; } = string.Empty;
    }
    /// <summary>
    /// Set Table Custom
    /// </summary>
    public class DataTableRows
    {
        public string? sField { get; set; } = string.Empty;
        public int? nColSpan { get; set; }
        public bool isShowBorder { get; set; } = true;
        public bool isColSpanCheckBox { get; set; } = false;
    }
    public class STCompany
    {
        /// <summary>
        ///Company Name
        /// </summary>
        public string sCompanyName { get; set; } = string.Empty;
        /// <summary>
        /// Company sContract
        /// </summary>
        public string sContract { get; set; } = string.Empty;
        /// <summary>
        /// Company Address
        /// </summary>
        public string sAddress { get; set; } = string.Empty;
        /// <summary>
        /// Tax ID
        /// </summary>
        public string sTaxID { get; set; } = string.Empty;
        /// <summary>
        /// Title + Tax ID
        /// </summary>
        public string sHeadDetailTaxID { get; set; } = string.Empty;
        /// <summary>
        /// ชื่อบัญชีธนาคาร
        /// </summary>
        public string sBankAccountName { get; set; } = string.Empty;
        /// <summary>
        /// เลขที่บัญชีธนาคาร
        /// </summary>
        public string sBankAccountNo { get; set; } = string.Empty;
        /// <summary>
        /// สาขาของธนาคาร
        /// </summary>
        public string sBankBranch { get; set; } = string.Empty;
        /// <summary>
        /// ชื่อธนาคาร
        /// </summary>
        public string sBankName { get; set; } = string.Empty;
    }

    /// <summary>
    /// ข้อมูลบริษัทลูกค้า
    /// </summary>
    public class DataDetailCustomer
    {
        /// <summary>
        /// ชื่อบริษัท
        /// </summary>
        public string sCustomerName { get; set; } = string.Empty;
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string sTax { get; set; } = string.Empty;
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string sAddress { get; set; } = string.Empty;
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string sTelephone { get; set; } = string.Empty;
        /// <summary>
        /// อีเมล
        /// </summary>
        public string sEmail { get; set; } = string.Empty;
    }
    public class ObjPermission
    {
        public int nPermission { get; set; }
    }
    public class ObjRateCalculate
    {
        public int? nVat { get; set; }
        public int? nTax { get; set; }
        public int? nPercentPriceTotal { get; set; }
    }
}

