/*
 **************************************************************
 * Author: Irfansjah
 * Email: irfansjah@gmail.com
 * Created: 07/14/2018
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using GTX.Commands;
using System.Threading;

namespace System
{

    public static class GTX_System_Utility
    {
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        
    }
    public static class ExtensionUtility
    {
        #region Object Extension
        /// <summary>
        /// Check If obj value is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Check If obj value is not null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static void ThrowIfParameterNull<T>(this T parameter)
        {
            if (parameter.IsNull())
                throw new ArgumentNullException(
                        //"parameter",
                        string.Format("Parameters ({0}) not defined",
                        typeof(T).FullName));
        }
        #endregion

        #region String Extension

        /// <summary>
        /// Create New String Id extension
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static string NewId(this string data, string newid = "")
        {
            if (!string.IsNullOrEmpty(data) && data.Trim().Length != 0) return data;
            data = string.IsNullOrEmpty(newid) ? GTX_String_Utility.CreateUniqueId : newid;
            return data;
        }

        /// <summary>
        /// Use string as a string format
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <usage>
        ///     "this template {0} a {1}".StringFormat("is","sample")
        /// </usage>
        public static string StringFormat(this string obj, params object[] param)
        {
            return string.Format(obj, param);
        }

        public static string CreateMD5Hash(this string str)
        {
            return GTX_String_Utility.GetHashString(HashStringMode.MD5,str);
        }
        public static string CreateSHA1Hash(this string str)
        {
            return GTX_String_Utility.GetHashString(HashStringMode.SHA1, str);
        }
        public static string CreateSHA256Hash(this string str)
        {
            return GTX_String_Utility.GetHashString(HashStringMode.SHA256, str);
        }
        public static string CreateSHA384Hash(this string str)
        {
            return GTX_String_Utility.GetHashString(HashStringMode.SHA384, str);
        }
        public static string CreateSHA512Hash(this string str)
        {
            return GTX_String_Utility.GetHashString(HashStringMode.SHA512, str);
        }

        public static string FormatNumber(this int anumber, int length, string format = "{0}{1}", string repeatedChar = "0")
        {
            return GTX_String_Utility.MakeStrFormatedNumber(anumber, length, format, repeatedChar);
        }

        public static void SetDictionary(this System.Collections.IDictionary obj, object key, object value)
        {
            List<Object> keys = new List<object>();
            foreach (object item in obj)
            {

                if (keys.IndexOf(item) == -1)
                {
                    keys.Add(item);
                }
            }
            if (keys.IndexOf(key) > -1)
                obj[key] = value;
            else
                obj.Add(key, value);
        }

        public static void SetDictionary(this IDictionary<string, object> obj, string key, object value)
        {
            if (obj.ContainsKey(key))
                obj[key] = value;
            else
                obj.Add(key, value);
        }



        #endregion


        #region Reflection

        /// <summary>
        /// Set obj property value based on its property name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="beforeExecute"></param>
        /// <param name="executed"></param>
        public static void SetPropertyValue(this object obj, string propertyName, object value,
            Action<object, object[]> beforeExecute = null,
            Action<object, object[]> executed = null)
        {
            if (obj.IsNull()) return;
            var propInfo = obj.GetType().GetProperty(propertyName);
            if (propInfo.IsNull()) return;
            try
            {
                var oldValue = propInfo.GetValue(obj);
                if (beforeExecute.IsNotNull())
                    // ReSharper disable once PossibleNullReferenceException
                    beforeExecute(obj, new[] { propertyName, value, oldValue });
                propInfo.SetValue(obj, value);
                if (executed.IsNotNull())
                    // ReSharper disable once PossibleNullReferenceException
                    executed(obj, new[] { propertyName, value, oldValue });
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Get obj property value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj.IsNull()) return null;
            var propInfo = obj.GetType().GetProperty(propertyName);
            if (propInfo.IsNull()) return null;
            return propInfo.GetValue(obj);
        }

        /// <summary>
        /// Get obj property value return as T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            var res = obj.GetPropertyValue(propertyName);
            return (T)res;
        }

        /// <summary>
        /// Check If obj has property propertyName
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasThisProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperties().Any(u => u.Name.Equals(propertyName));
        }

        /// <summary>
        /// Check If obj has Method methodName
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static bool HasThisMethod(this object obj, string methodName)
        {
            return obj.GetType().GetMethods().Any(u => u.Name.Equals(methodName));
        }

        /// <summary>
        /// Execute public Method
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteMethod(this object obj, string methodName, params object[] parameters)
        {
            if (obj.IsNull()) return null;
            var mtd = obj.GetType().GetMethod(methodName);
            if (mtd.IsNotNull())
            {
                return mtd.Invoke(obj, parameters);
            }
            else return null;
        }

        /// <summary>
        /// Execute public Method
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="executed"></param>
        /// <param name="parameters"></param>
        /// <param name="beforeExecute"></param>
        /// <returns></returns>
        public static object ExecuteMethod(this object obj, string methodName,
            Action<object, object[]> beforeExecute,
            Action<object, object[]> executed, params object[] parameters)
        {
            if (obj.IsNull()) return null;
            var mtd = obj.GetType().GetMethod(methodName);
            if (mtd.IsNotNull())
            {
                Exception errorbuf = null;
                object result = null;
                if (beforeExecute.IsNotNull())
                    beforeExecute(obj, parameters);
                try
                {
                    result = mtd.Invoke(obj, parameters);
                }
                catch (Exception err)
                {
                    errorbuf = err;
                }
                if (executed.IsNotNull())
                    executed(obj, parameters);
                if (errorbuf.IsNull())
                    return result;
                // ReSharper disable once PossibleNullReferenceException
                throw errorbuf;
            }
            else return null;
        }

        #endregion

        public static bool IsResultSuccess<T>(this ICommandResult<T> result) where T : class
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return result.Success && (result is IErrorResultData<T>);
        }

        public static bool IsResultSuccess<T>(this IResultData<T> result) where T : class
        {
            return !(result is IErrorResultData<T>);
        }
    }

    public static class CultureHelper
    {
        // Valid cultures
        private static readonly List<string> ValidCultures = new List<string> { "af", "af-ZA", "sq", "sq-AL", "gsw-FR", "am-ET", "ar", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "hy", "hy-AM", "as-IN", "az", "az-Cyrl-AZ", "az-Latn-AZ", "ba-RU", "eu", "eu-ES", "be", "be-BY", "bn-BD", "bn-IN", "bs-Cyrl-BA", "bs-Latn-BA", "br-FR", "bg", "bg-BG", "ca", "ca-ES", "zh-HK", "zh-MO", "zh-CN", "zh-Hans", "zh-SG", "zh-TW", "zh-Hant", "co-FR", "hr", "hr-HR", "hr-BA", "cs", "cs-CZ", "da", "da-DK", "prs-AF", "div", "div-MV", "nl", "nl-BE", "nl-NL", "en", "en-AU", "en-BZ", "en-CA", "en-029", "en-IN", "en-IE", "en-JM", "en-MY", "en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT", "en-GB", "en-US", "en-ZW", "et", "et-EE", "fo", "fo-FO", "fil-PH", "fi", "fi-FI", "fr", "fr-BE", "fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "fy-NL", "gl", "gl-ES", "ka", "ka-GE", "de", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el", "el-GR", "kl-GL", "gu", "gu-IN", "ha-Latn-NG", "he", "he-IL", "hi", "hi-IN", "hu", "hu-HU", "is", "is-IS", "ig-NG", "id", "id-ID", "iu-Latn-CA", "iu-Cans-CA", "ga-IE", "xh-ZA", "zu-ZA", "it", "it-IT", "it-CH", "ja", "ja-JP", "kn", "kn-IN", "kk", "kk-KZ", "km-KH", "qut-GT", "rw-RW", "sw", "sw-KE", "kok", "kok-IN", "ko", "ko-KR", "ky", "ky-KG", "lo-LA", "lv", "lv-LV", "lt", "lt-LT", "wee-DE", "lb-LU", "mk", "mk-MK", "ms", "ms-BN", "ms-MY", "ml-IN", "mt-MT", "mi-NZ", "arn-CL", "mr", "mr-IN", "moh-CA", "mn", "mn-MN", "mn-Mong-CN", "ne-NP", "no", "nb-NO", "nn-NO", "oc-FR", "or-IN", "ps-AF", "fa", "fa-IR", "pl", "pl-PL", "pt", "pt-BR", "pt-PT", "pa", "pa-IN", "quz-BO", "quz-EC", "quz-PE", "ro", "ro-RO", "rm-CH", "ru", "ru-RU", "smn-FI", "smj-NO", "smj-SE", "se-FI", "se-NO", "se-SE", "sms-FI", "sma-NO", "sma-SE", "sa", "sa-IN", "sr", "sr-Cyrl-BA", "sr-Cyrl-SP", "sr-Latn-BA", "sr-Latn-SP", "nso-ZA", "tn-ZA", "si-LK", "sk", "sk-SK", "sl", "sl-SI", "es", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-US", "es-UY", "es-VE", "sv", "sv-FI", "sv-SE", "syr", "syr-SY", "tg-Cyrl-TJ", "tzm-Latn-DZ", "ta", "ta-IN", "tt", "tt-RU", "te", "te-IN", "th", "th-TH", "bo-CN", "tr", "tr-TR", "tk-TM", "ug-CN", "uk", "uk-UA", "wen-DE", "ur", "ur-PK", "uz", "uz-Cyrl-UZ", "uz-Latn-UZ", "vi", "vi-VN", "cy-GB", "wo-SN", "sah-RU", "ii-CN", "yo-NG" };
        // Include ONLY cultures you are implementing
        private static readonly List<string> Cultures = new List<string>
        {
            //"en-US",  // first culture is the DEFAULT
            "en",  // English NEUTRAL culture
            "id"//,  // Indonesian NEUTRAL culture
            //"ar"  // Arabic NEUTRAL culture
        
        };

        /// <summary>
        /// Returns true if the language is a right-to-left language. Otherwise, false.
        /// </summary>
        public static bool IsRighToLeft()
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;

        }
        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-US)</param>
        public static string GetImplementedCulture(string name)
        {
            // make sure it's not null
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture(); // return Default culture
            // make sure it is a valid culture first
            if (!ValidCultures.Any(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                return GetDefaultCulture(); // return Default culture if it is invalid
            // if it is implemented, accept it
            if (Cultures.Any(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                return name; // accept it
            // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
            // the function will return closes match that is "en-US" because at least the language is the same (ie English)  
            var n = GetNeutralCulture(name);
            foreach (var c in Cultures)
                if (c.StartsWith(n))
                    return c;
            // else 
            // It is not implemented
            return GetDefaultCulture(); // return Default culture as no match found
        }
        /// <summary>
        /// Returns default culture name which is the first name declared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCulture()
        {
            return Cultures[0]; // return Default culture
        }
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }
        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-')[0]; // Read first part only. E.g. "en", "es"
        }
    }


}
