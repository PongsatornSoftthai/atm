using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;

namespace Extensions.STExtension
{
    public static class STExtension
    {
        #region Encrypt and Decrypt
        public static int DecryptParameterToInt(this string? instance)
        {
            int Result = 0;
            try
            {
                Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(HttpUtility.UrlDecode(instance)).ToInt() : 0;
            }
            catch
            {
                try
                {
                    Result = !string.IsNullOrEmpty(instance) ? HttpUtility.UrlDecode(STCrypt.Decrypt(instance)).ToInt() : 0;
                }
                catch
                {
                    Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(instance).ToInt() : 0;
                }
            }

            return Result;
        }
        public static int? DecryptParameterToIntNull(this string? instance)
        {
            int? Result = null;
            try
            {
                Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(HttpUtility.UrlDecode(instance)).ToInt() : null;
            }
            catch
            {
                try
                {
                    Result = !string.IsNullOrEmpty(instance) ? HttpUtility.UrlDecode(STCrypt.Decrypt(instance)).ToInt() : null;
                }
                catch
                {
                    Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(instance).ToInt() : null;
                }
            }

            return Result;
        }
        public static string DecryptParameter(this string? instance)
        {
            try
            {
                return STCrypt.Decrypt(HttpUtility.UrlDecode(instance.ToStringEmpty()));
            }
            catch
            {
                try
                {
                    return HttpUtility.UrlDecode(STCrypt.Decrypt(instance.ToStringEmpty()));
                }
                catch
                {
                    return STCrypt.Decrypt(instance.ToStringEmpty());
                }
            }
        }

        public static string EncryptParameter(this string instance)
        {
            return (!string.IsNullOrEmpty(instance)) ? HttpUtility.UrlEncode(STCrypt.Encrypt(instance)) : "";
        }

        public static string EncryptParameter(this int instance)
        {
            return instance.ToString().EncryptParameter();
        }
        #endregion

        #region Convert to int 
        public static int ToInt(this string instance)
        {
            return instance.ToIntOrNull().GetValueOrDefault();
        }
        public static int ToInt(this double? instance)
        {
            int value = instance.ToIntOrNull() ?? 0;
            return value;
        }
        public static int ToInt(this double instance)
        {
            int value = (int)instance;
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to int or null</br>
        /// <br>string stringNumber = "1";</br>
        /// <br>int? numberOrNull = stringNumber.toIntOrNull();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return int or null.</returns>
        public static int? ToIntOrNull(this string instance)
        {
            int? value = null;
            if (!string.IsNullOrEmpty(instance))
            {
                instance = ReplaceExponential(instance);
                int temp;
                bool isInt = int.TryParse(instance, out temp);
                if (isInt)
                {
                    value = temp;
                }
            }
            return value;
        }
        public static int? ToIntOrNull2(this string? instance)
        {
            int value = 0;
            if (!string.IsNullOrEmpty(instance))
            {
                instance = ReplaceExponential(instance);
                int temp;
                bool isInt = int.TryParse(instance, out temp);
                if (isInt)
                {
                    value = temp;
                }
            }
            return value;
        }
        public static int? ToIntOrNull(this double? instance)
        {
            int? value = null;
            if (instance != null)
            {
                value = (int)instance;
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert int? to int</br>
        /// <br>int? numberOrNull = stringNumber.toIntOrNull();</br>
        /// <br> int numberToNumber = numberOrNull.toInt();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return int or 0.</returns>
        public static int toInt(this int? instance)
        {
            int value = instance.HasValue ? instance.Value : 0;
            return value;
        }
        public static int toInt(this Enum instance)
        {
            return Convert.ToInt32(instance);
        }
        #endregion

        #region Convert to DateTime
        /// <summary>
        /// String From Date
        /// </summary>
        public static string ToStringFromDate(this DateTime? instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            return instance != null ? instance.Value.ToStringFromDate(sFormat, sCulture) : "";
        }
        public static string ToStringFromDateOnly(this DateOnly instance, string sCulture = "en-US")
        {
            sCulture = string.IsNullOrEmpty(sCulture) ? "en-US" : sCulture;
            return instance.ToString("dd/MM/yyyy", new CultureInfo(sCulture));
        }
        /// <summary>
        /// String From Date
        /// </summary>
        public static string ToStringFromDate(this DateTime instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            sCulture = string.IsNullOrEmpty(sCulture) ? "en-US" : sCulture;
            return instance.ToString(sFormat, new CultureInfo(sCulture));
        }
        public static string ToStringFromDateTimeNullAble(this DateTime? instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            sCulture = string.IsNullOrEmpty(sCulture) ? "en-US" : sCulture;
            return instance.HasValue ? instance.Value.ToString(sFormat, new CultureInfo(sCulture)) : "";
        }
        public static DateTime? ConvertDateFromString(string instance, bool checkDate, string strTime, DateTime dTemp)
        {
            DateTime? dResult = null;

            string[] sDateTemp = instance.Split('/');
            string sDate_Index = sDateTemp[0];
            if (sDate_Index.Length == 1)
            {
                sDate_Index = "0" + sDate_Index;
            }
            string sMonth_Index = sDateTemp[1];
            if (sMonth_Index.Length == 1)
            {
                sMonth_Index = "0" + sMonth_Index;
            }
            instance = sDateTemp[2].Substring(0, 4).Trim() + "-" + sMonth_Index + "-" + sDate_Index;
            checkDate = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp);
            if (!checkDate)
            {
                if (strTime.Length < 5)
                {
                    dResult = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                }
            }
            else
            {
                dResult = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
            }
            return dResult;
        }
        public static DateTime? dResultLengthLessThanFiveLength(string instance, string strTime, DateTime dTemp)
        {
            DateTime? dResult = null;
            string sTime = strTime != "" ? "0" + strTime : "00.00";
            dResult = DateTime.TryParseExact(instance + " " + sTime, "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
            return dResult;
        }
        public static DateTime? dResultLengthMoreThanFiveLength(string instance, string strTime, DateTime dTemp)
        {
            DateTime? dResult = null;
            string sTime = strTime != "" ? strTime : "00.00";
            dResult = DateTime.TryParseExact(instance + " " + sTime, "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
            return dResult;
        }
        /// <summary>
        /// Date From String
        /// </summary>
        public static DateTime? ToDateFromString(this string instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            try
            {
                string strTime = "";
                DateTime? dResult = null;
                DateTime dTemp;

                bool checkDate = DateTime.TryParseExact(instance + " " + "00.00", "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp);
                if (!checkDate)
                {
                    if (strTime.Length < 5)
                    {
                        dResult = dResultLengthLessThanFiveLength(instance, strTime, dTemp);
                    }
                }
                else
                {
                    dResult = dResultLengthMoreThanFiveLength(instance, strTime, dTemp);
                }

                if (!dResult.HasValue && !string.IsNullOrEmpty(instance))
                {
                    dResult = ConvertDateFromString(instance, checkDate, strTime, dTemp);
                }

                return dResult;
            }
            catch { return null; }

        }
        /// <summary>
        /// Change Time
        /// </summary>
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
        /// <summary>
        /// DateTime From String
        /// </summary>
        public static DateTime? ToDateTimeFromString(this string instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            try
            {
                DateTime? dResult = null;
                DateTime dTemp;

                bool checkDate = DateTime.TryParseExact(instance, sFormat, new CultureInfo(sCulture), DateTimeStyles.None, out dTemp);
                if (checkDate && !string.IsNullOrEmpty(instance))
                {
                    dResult = dTemp;
                }
                return dResult;
            }
            catch { return null; }

        }
        #endregion

        #region Convert to Datetime By UnixTime
        /// <summary>
        /// <br>Ex. Convert double to datetime</br>
        /// <br>double numberDate = "1661851724";</br>
        /// <br>DateTime? date = numberDate.toDateTime();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return datetime or null.</returns> 
        public static DateTime toDateTime(this double instance)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            result = result.AddMilliseconds(instance).ToLocalTime();
            return result;
        }
        public static DateOnly toDate(this double instance)
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            DateOnly dResult = DateOnly.FromDateTime(result.AddMilliseconds(instance).ToLocalTime());
            return dResult;
        }
        /// <summary>
        /// <br>Ex. Convert double to datetime</br>
        /// <br>double numberDate = "1661851724";</br>
        /// <br>DateTime? date = numberDate.toDateTime();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return datetime or null.</returns> 
        public static DateTime? toDateTime(this double? instance)
        {
            DateTime? result = null;
            if (instance != null)
            {
                double nDate = (double)instance;
                DateTime dResult = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                result = dResult.AddMilliseconds(nDate).ToLocalTime();
            }
            return result;
        }
        /// <summary>
        /// <br>Ex. Convert datetime to double</br>
        /// <br>DateTime datetime = DateTime.Now;</br>
        /// <br>double number = datetime.ToUnixTime();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return datetime or null.</returns> 
        public static double toUnixTime(this DateTime instance)
        {
            double result;
            DateTimeOffset dateValue = new DateTimeOffset(instance.ToUniversalTime());
            result = dateValue.ToUnixTimeMilliseconds();
            return result;
        }
        public static double toUnixDate(this DateOnly instance)
        {
            double result;
            DateTime dDateTime = instance.ToDateTime(TimeOnly.MinValue);
            DateTimeOffset dateValue = new DateTimeOffset(dDateTime.ToUniversalTime());
            result = dateValue.ToUnixTimeMilliseconds();
            return result;
        }
        /// <summary>
        /// <br>Ex. Convert datetime to double</br>
        /// <br>DateTime datetime = DateTime.Now;</br>
        /// <br>double number = datetime.ToUnixTime();</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Return datetime or null.</returns> 
        public static double? toUnixTime(this DateTime? instance)
        {
            double? result = null;
            if (instance != null)
            {
                DateTime dDate = (DateTime)instance;
                DateTimeOffset dateValue = new DateTimeOffset(dDate.ToUniversalTime());
                result = dateValue.ToUnixTimeMilliseconds();
            }
            return result;
        }
        #endregion

        #region Convert to double 
        /// <summary>
        /// <br>Ex. Convert string to double</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double number = stringNumber.toDouble();</br>
        /// <br>double number = stringNumber.toDouble(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double only normal case less more than 5 round down and more than or equal 5 round up.
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double ToDouble(this string instance, int? digit = null)
        {
            double value = instance.ToDoubleOrNull(digit) ?? 0;
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert double to double</br>
        /// <br>double numberOrNull = 10.255;</br>
        /// <br>double numberToNumber = numberOrNull.ToDouble();</br>
        /// <br>double numberToNumber = numberOrNull.ToDouble(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double only normal case less more than 5 round down and more than or equal 5 round up.
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double ToDouble(this double? instance, int? digit = null)
        {
            double value;
            if (digit.HasValue)
            {
                value = instance.HasValue ? Math.Round(instance.Value, digit.Value) : 0;
            }
            else
            {
                value = instance.HasValue ? instance.Value : 0;
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to double round up</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double number = stringNumber.toDoubleRoundUp();</br>
        /// <br>double number = stringNumber.toDoubleRoundUp(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double only and round up.
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.26</br>
        /// </returns> 
        public static double ToDoubleRoundUp(this string instance, int? digit = null)
        {
            double value = instance.ToDoubleRoundUpOrNull(digit) ?? 0;
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert double to double round up</br>
        /// <br>double numberOrNull = 10.255;</br>
        /// <br>double numberToNumber = numberOrNull.toDoubleRoundUp();</br>
        /// <br>double numberToNumber = numberOrNull.toDoubleRoundUp(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double round up only .
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.26</br>
        /// </returns> 
        public static double ToDoubleRoundUp(this double? instance, int? digit = null)
        {
            double value;
            if (digit.HasValue)
            {
                double mathPow = Math.Pow(10, digit.Value);
                value = instance.HasValue ? (Math.Ceiling(instance.Value * mathPow) / mathPow) : 0;
            }
            else
            {
                value = instance.HasValue ? instance.Value : 0;
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to double round down</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double number = stringNumber.toDoubleRoundDown();</br>
        /// <br>double number = stringNumber.toDoubleRoundDown(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double only and round down.
        /// <br>Ex. value = 1.255 return 1.25</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double ToDoubleRoundDown(this string instance, int? digit = null)
        {
            double value = instance.ToDoubleRoundDownOrNull(digit) ?? 0;
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert double to double round down</br>
        /// <br>double numberOrNull = 10.255;</br>
        /// <br>double numberToNumber = numberOrNull.toDoubleRoundDown();</br>
        /// <br>double numberToNumber = numberOrNull.toDoubleRoundDown(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double round down only.
        /// <br>Ex. value = 1.255 return 1.25</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double ToDoubleRoundDown(this double? instance, int? digit = null)
        {
            double value;
            if (digit.HasValue)
            {
                value = instance.HasValue ? Math.Round(instance.Value, digit.Value, MidpointRounding.ToZero) : 0;
            }
            else
            {
                value = instance.HasValue ? instance.Value : 0;
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to double or null</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double? number = stringNumber.toDoubleOrNull();</br>
        /// <br>double? number = stringNumber.toDoubleOrNull(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double or null.
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double? ToDoubleOrNull(this string instance, int? digit = null)
        {
            double? value = null;
            if (!string.IsNullOrEmpty(instance))
            {
                double temp;
                string stringValue = ReplaceExponential(instance);
                bool isDouble = double.TryParse(stringValue, out temp);
                if (isDouble)
                {
                    if (digit.HasValue)
                    {
                        value = Math.Round(temp, digit.Value);
                    }
                    else
                    {
                        value = temp;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to double round up or null</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double? number = stringNumber.toDoubleRoundUpOrNull();</br>
        /// <br>double? number = stringNumber.toDoubleRoundUpOrNull(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double or null.
        /// <br>Ex. value = 1.255 return 1.26</br>
        /// <br>Ex. value = 1.254 return 1.26</br>
        /// </returns> 
        public static double? ToDoubleRoundUpOrNull(this string instance, int? digit = null)
        {
            double? value = null;
            if (!string.IsNullOrEmpty(instance))
            {
                double temp;
                string stringValue = ReplaceExponential(instance);
                bool isDouble = double.TryParse(stringValue, out temp);
                if (isDouble)
                {
                    if (digit.HasValue)
                    {
                        double mathPow = Math.Pow(10, digit.Value);
                        value = (Math.Ceiling(temp * mathPow) / mathPow);
                    }
                    else
                    {
                        value = temp;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// <br>Ex. Convert string to double round down or null</br>
        /// <br>string stringNumber = "1.256";</br>
        /// <br>double? number = stringNumber.toDoubleRoundDownOrNull();</br>
        /// <br>double? number = stringNumber.toDoubleRoundDownOrNull(2);</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="digit"></param>
        /// <returns>Return double or null.
        /// <br>Ex. value = 1.255 return 1.25</br>
        /// <br>Ex. value = 1.254 return 1.25</br>
        /// </returns> 
        public static double? ToDoubleRoundDownOrNull(this string instance, int? digit = null)
        {
            double? value = null;
            if (!string.IsNullOrEmpty(instance))
            {
                double temp;
                string stringValue = ReplaceExponential(instance);
                bool isDouble = double.TryParse(stringValue, out temp);
                if (isDouble)
                {
                    if (digit.HasValue)
                    {
                        value = Math.Round(temp, digit.Value, MidpointRounding.ToZero);
                    }
                    else
                    {
                        value = temp;
                    }
                }
            }
            return value;
        }
        #endregion

        #region Numberic
        public static string ToStringFromNumber(this int? instance)
        {
            return instance != null ? instance.Value.ToString("n0") : "";
        }
        public static string ToStringFromNumber(this int instance)
        {
            return instance.ToString("n0");
        }
        public static string ToStringFromNumber(this double instance)
        {
            return instance.ToString("n0");
        }

        public static string ToStringFromNumber(this decimal? instance, int nDigit)
        {
            return instance != null ? instance.Value.ToString("n" + nDigit) : "";
        }
        public static string ToStringFromNumber(this double instance, int nDigit)
        {
            return instance.ToString("n" + nDigit);
        }

        public static string ToStringFromNumber(this decimal? instance, int nDigit, bool IsPercent)
        {
            string sPercent = IsPercent ? "%" : "";
            return instance != null ? instance.Value.ToString(("n" + nDigit) + sPercent) : "";
        }

        public static string ToStringFromNumber(this double? instance, int nDigit, bool IsPercent = false)
        {
            string sPercent = IsPercent ? "%" : "";
            return instance != null ? instance.Value.ToString(("n" + nDigit) + sPercent) : "";
        }
        #endregion

        #region String
        public static string GetQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.CustomAttributes.FirstOrDefault()?.ConstructorArguments.FirstOrDefault().Value + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
        public static string ToStringEmpty(this string? instance)
        {
            return instance ?? "";
        }
        public static string TrimToLower(this string? instance)
        {
            return instance.ToStringEmpty().Trim().ToLower();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetNameFromEmail(this object instance)
        {
            return instance != null ? (instance + "").Split('@')[0] + "" : "";
        }

        /// <summary>
        /// Extension
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nStartIndex"></param>
        /// <param name="nLength"></param>
        /// <returns></returns>
        public static string SubStr(this string instance, int nStartIndex, int nLength)
        {
            if (!string.IsNullOrEmpty(instance))
            {
                if (instance.Length < (nStartIndex + nLength))
                {
                    return instance.Substring(nStartIndex, instance.Length);
                }
                else
                {
                    return instance.Substring(nStartIndex, nLength);
                }
            }
            else
                return "";
        }
        /// <summary>
        /// 
        /// </summary>
        public static string SubStrMax(this string instance, int nMaxLength)
        {
            return instance.SubStr(0, nMaxLength);
        }
        /// <summary>
        /// 
        /// </summary>
        public static string TrimAll(this string instance)
        {
            return (instance + "").Trim();
        }
        #endregion

        #region Sum
        /// <summary>
        /// Sum Decimal
        /// </summary>
        public static decimal? SumOrDefault(this List<decimal?> instance)
        {
            if (instance.Exists(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }
        /// <summary>
        /// Sum Double
        /// </summary>
        public static double? SumOrDefault(this List<double?> instance)
        {
            if (instance.Exists(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }
        /// <summary>
        /// Sum int
        /// </summary>
        public static int? SumOrDefault(this List<int?> instance)
        {
            if (instance.Exists(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }
        #endregion

        /// <summary>
        /// Has Item
        /// </summary>
        public static bool HasItems<T>(this IEnumerable<T> source)
        {
            return (source?.Any() ?? false);
        }

        /// <summary>
        /// Replace Exponential
        /// </summary>
        public static string ReplaceExponential(this string sVal)
        {
            string sRsult = "";
            try
            {
                decimal nTemp = 0;
                bool check = decimal.TryParse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float, null, out nTemp);
                if (check)
                {
                    decimal d = decimal.Parse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float);
                    sRsult = (d + "").Replace(",", "");
                }
                else
                {
                    sRsult = sVal;
                }
            }
            catch
            {
                sRsult = sVal;
            }

            return sRsult;
        }
        /// <summary>
        /// Is Number
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNumber(this string instance)
        {
            foreach (char ch in instance)
            {
                if (!char.IsNumber(ch)) return false;
            }
            return true;
        }
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            string name2 = name;
            PropertyInfo[] properties = objType.GetProperties();
            PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo p) => p.Name == name2);
            if (propertyInfo == null)
            {
                throw new ArgumentException("name");
            }

            return propertyInfo;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            ParameterExpression parameterExpression = Expression.Parameter(objType);
            MemberExpression body = Expression.PropertyOrField(parameterExpression, pi.Name);
            return Expression.Lambda(body, parameterExpression);
        }
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(T), name);
            LambdaExpression orderExpression = GetOrderExpression(typeof(T), propertyInfo);
            MethodInfo methodInfo = typeof(Enumerable).GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            if (methodInfo == null)
            {
                throw new Exception("ST.INFRA.Common.STExtension.OrderBy method is null");
            }

            MethodInfo methodInfo2 = methodInfo.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
            return (IEnumerable<T>)methodInfo2.Invoke(null, new object[2]
            {
            query,
            orderExpression.Compile()
            });
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(T), name);
            LambdaExpression orderExpression = GetOrderExpression(typeof(T), propertyInfo);
            MethodInfo methodInfo = typeof(Enumerable).GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            if (methodInfo == null)
            {
                throw new Exception("ST.INFRA.Common.STExtension.OrderByDescending method is null");
            }

            MethodInfo methodInfo2 = methodInfo.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
            return (IEnumerable<T>)methodInfo2.Invoke(null, new object[2]
            {
            query,
            orderExpression.Compile()
            });
        }
        /// <summary>
        /// Is Digit
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsDigit(this string instance)
        {
            foreach (char ch in instance)
            {
                if (!instance.Any(char.IsDigit)) return false;
            }
            return true;
        }

        /// <summary>
        /// ToQueryable
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new[] { instance }.AsQueryable();
        }

        public static string GetEnumDescription(this Enum value)
        {
            string result = "";
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    result = attributes[0].Description;
                }
                else
                {
                    result = value.ToString();
                }
            }
            return result;
        }

        public static List<E> ToShuffleList<E>(this List<E> inputList)
        {
            List<E> randomList = new List<E>();

            while (inputList.Count > 0)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(inputList.Count);
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
        public static string MaxLengthText(string instance, int nLength)
        {
            if (!string.IsNullOrEmpty(instance))
            {
                if (instance.Length <= nLength)
                {
                    return instance;
                }
                else
                {
                    return instance.Substring(0, nLength);
                }
            }
            else
                return "";
        }
    }
}
