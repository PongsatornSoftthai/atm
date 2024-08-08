using Azure.Identity;
using Extensions.STExtension;
using Extensions.STFunction;
using System.Text;

namespace backend.Extensions
{
    public static class Systemfunction
    {
        public static readonly bool isTrue = true;
        public static readonly bool isFalse = false;

        /// <summary>
        /// Connection DB STConnection
        /// </summary>
        /// <param name="sConnName">ตั้งต้นให้ STConnection</param>
        /// <returns></returns>
        public static string GetConnectionString(string? sConnName = null)
        {
            string result;
            string ConnectionName = !string.IsNullOrEmpty(sConnName) ? STFunction.GetAppSettingJson("AzureKeyVault:" + sConnName) : STFunction.GetAppSettingJson("AzureKeyVault:SaleConnection");
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json");

            using (var configuration = (ConfigurationRoot)builder.Build())
            {
                 string sName = !string.IsNullOrEmpty(sConnName) ? sConnName : "AtmConnection";
                 result = configuration.GetConnectionString(sName) ?? "";
            }
            return result;
        }
    }
}
