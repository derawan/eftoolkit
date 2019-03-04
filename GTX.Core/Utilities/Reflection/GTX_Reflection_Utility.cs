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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Compilation;

namespace System
{
    public static class GTX_Reflection_Utility
    {
        public static string GetAssemblyId()
        {
            Assembly ass = null;
            try
            {
                ass = BuildManager.GetGlobalAsaxType().BaseType?.Assembly;
            }
            catch
            {
                // ignored
            }
            if (ass == null)
            {
                if ((Web.HttpContext.Current!=null) && ((Web.HttpContext.Current!=null) || (Web.HttpContext.Current.ApplicationInstance!=null)))
                {
                    var type = Web.HttpContext.Current.ApplicationInstance.GetType();
                    while (type != null && type.Namespace == "ASP")
                    {
                        type = type.BaseType;
                    }
                    if (type != null) ass = type.Assembly;
                }
            }
            if (ass == null)
                ass = Assembly.GetEntryAssembly();
            if (ass == null)
                ass = Assembly.GetExecutingAssembly();

            return ((GuidAttribute)((ass.GetCustomAttributes(typeof(GuidAttribute), true))[0])).Value;

        }

        public static string GetAppAssemblyName()
        {
            Assembly ass = null;
            try
            {
                ass = BuildManager.GetGlobalAsaxType().BaseType?.Assembly;
            }
            catch
            {
                // ignored
            }
            if (ass == null)
            {
                if ((Web.HttpContext.Current!=null) && ((Web.HttpContext.Current!=null) || (Web.HttpContext.Current.ApplicationInstance!=null)))
                {
                    var type = Web.HttpContext.Current.ApplicationInstance.GetType();
                    while (type != null && type.Namespace == "ASP")
                    {
                        type = type.BaseType;
                    }
                    if (type != null) ass = type.Assembly;
                }
            }
            if (ass == null)
                ass = Assembly.GetEntryAssembly();
            if (ass == null)
                ass = Assembly.GetExecutingAssembly();
            return ass.FullName;
        }

        public static string GetAppName()
        {
            Assembly ass = null;
            try
            {
                ass = BuildManager.GetGlobalAsaxType().BaseType?.Assembly;
            }
            catch
            {
                // ignored
            }
            if (ass == null)
            {
                if ((Web.HttpContext.Current != null) && ((Web.HttpContext.Current != null) || (Web.HttpContext.Current.ApplicationInstance != null)))
                {
                    var type = Web.HttpContext.Current.ApplicationInstance.GetType();
                    while (type != null && type.Namespace == "ASP")
                    {
                        type = type.BaseType;
                    }
                    if (type != null) ass = type.Assembly;
                }
            }
            if (ass == null)
                ass = Assembly.GetEntryAssembly();
            if (ass == null)
                ass = Assembly.GetExecutingAssembly();

            return ((AssemblyTitleAttribute)((ass.GetCustomAttributes(typeof(AssemblyTitleAttribute), true))[0])).Title;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties<T>()
        {
            return typeof(T).GetProperties();
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static FieldInfo[] GetFields<T>()
        {
            return typeof(T).GetFields();
        }

        public static MethodInfo GetGenericMethod(object obj, string methodName, params Type[] types)
        {
            if (obj==null) return null;
            MethodInfo mtd = obj.GetType().GetMethod(methodName);
            MethodInfo mtdg = mtd.MakeGenericMethod(types);
            return mtdg;
        }

        public static MethodInfo GetMethod(object obj, string methodName)
        {
            if (obj==null) return null;
            MethodInfo mtd = obj.GetType().GetMethod(methodName);
            return mtd;
        }

    }
}
