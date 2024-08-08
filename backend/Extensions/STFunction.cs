using Extensions.STExtension;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Azure.Identity;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Drawing;
using backend.Extensions;
using System.Data;
using Ganss.Xss;
using backend.Models;


namespace Extensions.STFunction
{
    /// <summary>
    /// ST Function
    /// </summary>
    public class STFunction
    {        
        //dotnet-ef dbcontext scaffold Name=AwardCenterConnection Microsoft.EntityFrameworkCore.SqlServer --context PICEntity --output-dir EF/Award --use-database-names --force  --no-pluralize
        /// <summary>
        /// 
        /// </summary>
        public static string GetAppSettingJson(string GetParameter)
        {
            string Result = "";
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            IConfigurationRoot configuration = builder.Build();
            IConfigurationSection section = configuration.GetSection(GetParameter);
            Result = section != null ? (section.Value + "") : "";
            return Result;
        }

        #region File
        public static string ReplaceSeparatorPathByOS(string instance)
        {
            if (OperatingSystem.IsWindows())
            {
                instance = instance.Replace("/", "\\").Trim();
            }

            return instance;

        }
        /// <summary>
        /// ใช้สำหรับตรวจสอบ Path ว่ามีตัวอักษรที่ไม่เหมาะสมหรือไม่
        /// </summary>
        /// <param name="sPath">Path ที่ต้องการ Create Ex. Uploadfiles/Test</param>
        public static bool FilePathHasInvalidChars(string? path)
        {
            return !string.IsNullOrEmpty(path) && path.IndexOfAny(Path.GetInvalidPathChars()) >= 0 && !path.Contains("..");
        }
        /// <summary>
        /// ใช้สำหรับการ Create Directory Path
        /// </summary>
        /// <param name="sPath">Path ที่ต้องการ Create Ex. Uploadfiles/Test</param>
        /// <param name="_env">IHostEnvironment</param>
        public static string CreateDirectoryPath(string? sPath, IHostEnvironment _env)
        {
            string sDirectory = MapPath(sPath.ToStringEmpty(), "", _env);
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
            }
            return sDirectory;
        }
        /// <summary>
        /// ใช้สำหรับการลบข้อความที่ไม่ผ่าน Security ของไฟล์
        /// </summary>
        /// <param name="sFileName">FileName ที่ต้องการ Create</param>
        public static string RemoveInvalidFileName(string? sFileName)
        {
            string result = string.Empty;
            var sanitiser = new HtmlSanitizer();
            if (!string.IsNullOrEmpty(sFileName))
            {
                var sanitised = sanitiser.Sanitize(sFileName);
                if (sFileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 1)
                {
                    throw new ArgumentNullException(sFileName, "FileName Error");
                }
                else if (sFileName != sanitised.Replace("&amp;", "&"))
                {
                    throw new ArgumentNullException(sFileName, "FileName Cross Site Scriping");
                }
                result = sFileName.Replace("'", "").Replace(";", "");
            }
            return result;
        }
        /// <summary>
        /// ใช้สำหรับกการลบข้อความที่ไม่ผ่าน Security ของ Directory
        /// </summary>
        /// <param name="sPath">Path ที่ต้องการ Create Ex. Uploadfiles/Test</param>
        public static string RemoveInvalidPath(string? sFilePath)
        {
            return !string.IsNullOrEmpty(sFilePath) ? string.Concat(sFilePath.Split(Path.GetInvalidPathChars())) : "";
        }
        public static string RemoveUnsupportFolderChar(string? fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string sPathFolder = "";
                fileName = fileName.Replace('\\', '/');
                string[] arrData = fileName.Split('/');
                foreach (var item in arrData)
                {
                    if (item.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 1)
                    {
                        throw new ArgumentNullException(fileName, "FileName Error");
                    }
                    else
                    {
                        StringBuilder sData = new StringBuilder();
                        sData.Append(sPathFolder + "/" + item);
                        sPathFolder = sData.ToString();
                    }
                }
                string sNewFoler = sPathFolder.Length > 0 ? sPathFolder.Remove(0, 1) : "";
                fileName = sNewFoler.Replace("'", "").Replace(";", "");
            }
            else
            {
                fileName = "";
            }
            return fileName;
        }
        /// <summary>
        /// ใช้สำหรับการหา Root Directory
        /// </summary>
        /// <param name="sPath">Path ที่ต้องการ Create Ex. Uploadfiles/Test</param>
        /// <param name="sFileName">FileName ที่ต้องการ Create</param>
        /// <param name="_env">IHostEnvironment</param>
        public static string MapPath(string? sPath, string? sFileName, IHostEnvironment _env)
        {
            if (FilePathHasInvalidChars(sPath.ToStringEmpty()))
            {
                return "Trying to read path outside of root";
            }
            else
            {
                string sBaseFolder = _env.ContentRootPath;
                string sSecurityPath = RemoveInvalidPath(sPath);
                string sSecurityFileName = RemoveInvalidFileName(sFileName);
                string sSecurityFile = !string.IsNullOrEmpty(sSecurityFileName) ? "/" + sSecurityFileName : "";
                string[] arrPath = { _env.ContentRootPath, "wwwroot", sSecurityPath, sSecurityFile };
                string sFullPath = Path.Join(arrPath);
                string GetFullPath = Path.GetFullPath(sFullPath);
                if (FilePathHasInvalidChars(GetFullPath))
                {
                    return "Trying to read path outside of root";
                }

                if (!GetFullPath.StartsWith(sBaseFolder))
                {
                    return "Trying to read path outside of root";
                }

                return GetFullPath;
            }
        }

        #region Path Manage
        public static string GetPathTemp(string? sFolderName, string sFolder)
        {
            string result = "";
            string sFormatTemp = "Temp/{0}/";
            string sFormatFolder = "{0}/";
            if (!string.IsNullOrEmpty(sFolderName))
            {
                result = string.Format(sFormatFolder, sFolderName);
            }
            else
            {
                result = string.Format(sFormatTemp, sFolder);
            }
            return result;
        }
        public static string GetPathTrue(string? sFolder, int nID, bool IsSaveDB)
        {
            string result = "";
            string sFormatMove = "{0}/{1}/";
            string sFormatSave = "{0}/{1}/";
            if (IsSaveDB)
            {
                result = string.Format(sFormatSave, sFolder, nID);
            }
            else
            {
                result = string.Format(sFormatMove, sFolder, nID);
            }
            return result;
        }
        #endregion
        public static string RemoveInvalidFolderChar(string? sFileName)
        {
            if (!string.IsNullOrEmpty(sFileName))
            {
                string sPathFolder = "";
                sFileName = sFileName.Replace('\\', '/');
                string[] arrData = sFileName.Split('/');
                foreach (var item in arrData)
                {
                    if (item.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 1)
                    {
                        throw new ArgumentNullException(sFileName, "FileName Error");
                    }
                    else
                    {
                        StringBuilder sData = new StringBuilder();
                        sData.Append(sPathFolder + "/" + item);
                        sPathFolder = sData.ToString();
                    }
                }
                string sNewFolder = sPathFolder.Length > 0 ? sPathFolder.Remove(0, 1) : "";
                sFileName = sNewFolder.Replace("'", "").Replace(";", "");
            }
            return sFileName ?? "";
        }
        public static string MapPathAppSetting(string? sPath, string? sFileName, IHostEnvironment _env)
        {
            if (FilePathHasInvalidChars(sPath.ToStringEmpty()))
            {
                return "Trying to read path outside of root";
            }
            else
            {
                string sBaseFolder = _env.ContentRootPath;
                string sSecurityPath = RemoveInvalidPath(sPath);
                string sSecurityFileName = RemoveInvalidFileName(sFileName);
                string[] arrPath = { _env.ContentRootPath, sSecurityPath, "/", sSecurityFileName };
                string sFullPath = Path.Join(arrPath);
                string GetFullPath = Path.GetFullPath(sFullPath);
                if (FilePathHasInvalidChars(GetFullPath))
                {
                    return "Trying to read path outside of root";
                }

                if (!GetFullPath.StartsWith(sBaseFolder))
                {
                    return "Trying to read path outside of root";
                }

                return GetFullPath;
            }
        }


        /// <summary>
        /// ใช้สำหรับการ ย้ายไฟล์
        /// </summary>
        /// <param name="TempPath">Path เก่าที่ต้องการย้าย</param>
        /// <param name="TruePath">Path ใหม่ที่ต้องการไปวาง</param>
        /// <param name="SysFileName">ชื่อไฟล์ในระบบ</param>
        /// <param name="_env">IHostEnvironment</param>
        public static void MoveFile(string TempPath, string TruePath, string SysFileName, IHostEnvironment _env)
        {
            CreateDirectoryPath(TruePath, _env);
            string ServerTempPath = ReplaceSeparatorPathByOS(MapPath(TempPath, SysFileName, _env));
            string ServerTruePath = ReplaceSeparatorPathByOS(MapPath(TruePath, SysFileName, _env));
            if (File.Exists(ServerTempPath) && ServerTempPath != ServerTruePath)
            {
                File.Move(ServerTempPath, ServerTruePath);
            }
        }
        /// <summary>
        /// ใช้สำหรับการ ลบไฟล์
        /// </summary>
        /// <param name="sPath">Path for delete</param>
        /// <param name="_env">IHostEnvironment</param>
        public static void DeleteFile(string sPath, IHostEnvironment _env)
        {
            try
            {
                var urlPath = ReplaceSeparatorPathByOS(sPath);
                bool isCheckFile = FilePathHasInvalidChars(urlPath);
                string sSystemFileName = RemoveInvalidFileName(urlPath + "");

                //if (isCheckFile && !string.IsNullOrEmpty(sSystemFileName))
                if (!string.IsNullOrEmpty(sSystemFileName))
                {
                    var path = MapPath(sSystemFileName, "", _env);
                    var urlPath2 = ReplaceSeparatorPathByOS(path);
                    bool isCheckFile2 = FilePathHasInvalidChars(urlPath2);
                    if (isCheckFile2 && File.Exists(urlPath2))
                    {
                        var PathDel = Scan_CWE22_FullPathFile(urlPath2);
                        var urlPath3 = ReplaceSeparatorPathByOS(PathDel);
                        bool isCheckFile3 = FilePathHasInvalidChars(PathDel);
                        string sSystemFileName2 = RemoveInvalidFileName(PathDel + "");
                        if (isCheckFile3 && File.Exists(sSystemFileName2))
                        {
                            File.Delete(PathDel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message.ToString();
                if (x.Contains("The process cannot access the file"))
                {
                    Thread.Sleep(1000);
                    DeleteFile(sPath, _env);
                }
            }
        }
        /// <summary>
        /// Get path File for display
        /// </summary>
        /// <param name="FilePath">Path file</param>
        /// <param name="SysFileName">ชื่อไฟล์ในระบบ</param>
        public static string GetPathUploadFile(string FilePath, string SysFileName)
        {
            string sFullPath = "";
            if (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(SysFileName) && !STFunction.FilePathHasInvalidChars(FilePath))
            {
                string sPathWeb = GetAppSettingJson("AppSetting:UrlSiteBackend");
                string sSecurityPath = RemoveInvalidPath(FilePath);
                string sSecurityFileName = RemoveInvalidFileName(SysFileName);
                sFullPath = sPathWeb + sSecurityPath + "/" + sSecurityFileName;
            }
            return sFullPath;
        }
        /// <summary>
        /// ใช้สำหรับการ Create Directory Path
        /// </summary>
        /// <param name="sPath">Path ที่ต้องการ Create Ex. UploadFiles/Test</param>
        /// <param name="_env">IHostEnvironment</param>
        public static void FolderCreate(string sPath, IHostEnvironment _env)
        {
            var path = MapPath(sPath, "", _env);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// check have Folder name
        /// </summary>
        /// <param name="sPath">Path file</param>
        /// <param name="_env">IHostEnvironment</param>
        public static bool isCheckFolder(string sPath, IHostEnvironment _env)
        {
            bool isHaveFolder = false;
            var path = MapPath(sPath, "", _env);
            if (Directory.Exists(path))
            {
                isHaveFolder = true;
            }
            return isHaveFolder;
        }
        /// <summary>
        /// check have file name
        /// </summary>
        /// <param name="sPath">Path file</param>
        /// <param name="_env">IHostEnvironment</param>
        public static bool isCheckFile(string sPath, IHostEnvironment _env)
        {
            bool isHaveFile = false;
            var path = MapPath(sPath, "", _env);
            if (File.Exists(path))
            {
                isHaveFile = true;
            }
            return isHaveFile;
        }
        

        public static string ConvertStringFilePathToBase64(string filePath)
        {
            try
            {
                if (!STFunction.FilePathHasInvalidChars(filePath))
                {
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        string base64String = "data:" + GetMimeType(filePath) + ";base64," + Convert.ToBase64String(fileBytes);
                        return base64String;
                    }
                    else
                    {
                        Console.WriteLine("File path is empty or the file does not exist.");
                        return string.Empty;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting file to base64: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Scan_CWE22_FullPathFile
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public static string Scan_CWE22_FullPathFile(string pathFile)
        {
            string sPathSecurity = "";
            try
            {
                var sPath = ReplaceSeparatorPathByOS(pathFile);
                bool isCheckFile = FilePathHasInvalidChars(sPath);

                if (isCheckFile)
                {
                    sPathSecurity = pathFile
                    .Replace("../", "")
                    .Replace("..\\", "")
                    .Replace("..", "").Replace("\\", "/").Trim();
                    if (OperatingSystem.IsWindows())
                    {
                        sPathSecurity = sPathSecurity.Replace("/", "\\").Trim();
                    }
                    Uri uriAddress2 = new Uri(sPathSecurity);
                    if (!uriAddress2.IsFile)
                    {
                        sPathSecurity = "";
                    }
                }
            }
            catch (Exception)
            {
                sPathSecurity = "";
            }
            return sPathSecurity;
        }
        
        private static string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            switch (extension.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }
        public static string GetTypeApplication(string sFileName)
        {
            string[] GetFileType = sFileName.Split('.');
            string sFileType = GetFileType[GetFileType.Length - 1].ToLower();
            string result = "";
            switch (sFileType)
            {
                case "ppt":
                    result = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    result = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case "pdf":
                    result = "application/pdf";
                    break;
                case "xls":
                case "xlsx":
                    result = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "doc":
                case "docx":
                    result = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "zip":
                    result = "application/zip";
                    break;
                case "txt":
                    result = "text/plain";
                    break;
                case "msg":
                    result = "application/vnd.ms-outlook";
                    break;
            }


            return result;
        }
        #endregion

        public static CReportFont GetReportFont(string sType)
        {
            CReportFont objFont = new CReportFont();
            if (sType == "docx" || sType == "pdf")
            {
                objFont.sFont = "Angsana New";
                objFont.nFontSize = 16;
            }
            else
            {
                objFont.sFont = "Tahoma";
                objFont.nFontSize = 12;
            }
            return objFont;
        }


        /// <summary>
        /// </summary>
        public static string GetConnectionString()
        {
            return GetAppSettingJson("ConnectionStrings:FossilFundManagement");
        }
        public static string GetFullUnitName(string? sUnitCode, string? sUnitAbbr, string? sUnitName)
        {
            return $"{sUnitCode} - {sUnitName} ({sUnitAbbr})";
        }
        /// <summary>
        /// </summary>
        public class ChgFilter
        {
            /// <summary>name table for change nOrder</summary>
            public string Table { get; set; } = string.Empty;
            /// <summary>Column for change</summary>
            public string TypeColumn { get; set; } = string.Empty;
            /// <summary>Column for change</summary>
            public int? TypeID { get; set; }
            /// <summary>Column for change</summary>
            public string Column { get; set; } = string.Empty;
            /// <summary>id primary key table </summary>

            public string? sID { get; set; }
            //public int? nID
            //{
            //    get
            //    {
            //        return !string.IsNullOrEmpty(sID) ? sID.DecryptParameterToInt() : 0;
            //    }
            //}
            /// <summary>order target</summary>
            public int NewOrder { get; set; }
        }

        public class CReportFont
        {
            public string sFont { get; set; } = string.Empty;
            public int nFontSize { get; set; }
        }

        /// <summary>
        /// Gen Password
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GenPWD()
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@";
            StringBuilder result = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(characters.Length);
                result.Append(characters[randomIndex]);
            }
            return result.ToString();
        }

        /// <summary>
        /// Gen OTP
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GenOTP()
        {
            string characters = "0123456789";
            StringBuilder result = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(characters.Length);
                result.Append(characters[randomIndex]);
            }
            return result.ToString();
        }

        
        public static Pagination Paging(int nPageSize, int nPageIndex, int nCountAllData)
        {
            Pagination pagination = new Pagination();
            nPageIndex = ((nPageIndex == 0) ? 1 : nPageIndex);
            decimal d = ((nPageSize > 0) ? ((decimal)nCountAllData / (decimal)nPageSize) : 0m);
            int num = (int)Math.Ceiling(d);
            nPageIndex = ((nPageIndex > num) ? num : nPageIndex);
            int num2 = nPageSize * (nPageIndex - 1);
            num2 = ((num2 >= 0) ? num2 : 0);
            pagination.nDataLength = nCountAllData;
            pagination.nSkip = num2;
            pagination.nTake = nPageSize;
            pagination.nPageIndex = nPageIndex;
            pagination.nStartIndex = num2 + 1;
            return pagination;
        }

        public class CSortTable
        {
            public string sCaseName { get; set; } = string.Empty;
            public string sSortColumn { get; set; } = string.Empty;
        }

        public class ResultTable<T> : Pagination
        {
            public List<T> lstData { get; set; } = new();
        }


        public static bool CheckPassSecurePath(string path)
        {
            Regex tagRegex = new Regex(@"<[^>]+>");
            bool hasTags = tagRegex.IsMatch(path);
            bool isContainBackFolder = path.Contains("..");
            return !hasTags && !isContainBackFolder;
        }
        public static bool CheckByte(byte[] arrByte)
        {

            string sStrValue = Encoding.ASCII.GetString(arrByte);
            bool isPass = CheckPassSecurePath(sStrValue);



            return isPass;
        }
        

    }
}