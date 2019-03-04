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
using System;
using System.Security.Claims;
using GTX.Commands;
using GTX.Core;

namespace GTX.Bootstrapper
{
    public static class BootStrapManager
    {
        public static IAuthenticationContext AuthenticationContext
        {
            get
            {
                return Bootstrap.AuthenticationContext;
            }
        }

        public static IApplicationContext ApplicationContext { get; set; }
        public static object Applications { get; set; }
        public static IBootstrap Bootstrap { get; set; }

        public static void Init<T>(object apps = null)
        {
            if (Bootstrap.IsNotNull()) return;
            if (apps.IsNotNull())
                Applications = apps;
            Bootstrap = (IBootstrap)Activator.CreateInstance<T>();
            InitApplication();
        }

        public static void InitApplication()
        {
            if (ApplicationContext.IsNotNull()) return;
            ApplicationContext = Bootstrap.Resolve<IApplicationContext>();
        }
        public static void StopApplication()
        {
            if (ApplicationContext.IsNull()) return;
            ApplicationContext.Dispose();
        }

        public static T Resolve<T>()
        {
            Type tipe = typeof(T);
            if (tipe.IsAbstract && tipe.IsSealed)
            {
                return default(T);
            }
            else
            {
                return Bootstrap.Resolve<T>();
            }
        }

        public static object Resolve(Type tipe)
        {
            if (tipe.IsAbstract && tipe.IsSealed)
            {
                return null;
            }
            else
            {
                return Bootstrap?.Resolve(tipe);
            }
        }

        public static bool IsAuthenticated<TCommandParameter, TModel, TKey>(ICommandHandler<TCommandParameter, TModel> handler)
            where TCommandParameter : IBaseCommandParameters<TModel, TKey>
            where TModel : class
        {
            return (AuthenticationContext == null) ? false : AuthenticationContext.User.IsAuthenticated;
        }

        public static IRequestContext RequestContext
        {
            get
            {
                return Bootstrap?.RequestContext;
            }
        }


        public static void LogError(object sender, string sourceMethod, string message, long? errorcode, Exception error)
        {

        }

        public static void LogError(object sender, string sourceMethod, IError error)
        {

        }

        public static void LogError(object sender, string sourceMethod, Exception error)
        {
        }

        public static string User
        {
            get
            {
                return (AuthenticationContext.IsNull()) ? "" : AuthenticationContext.User.Name;
            }
        }

        public static System.Collections.Generic.Dictionary<string,string> UserData
        {
            get
            {
                System.Collections.Generic.Dictionary<string,string> item = new System.Collections.Generic.Dictionary<string,string>();
                if (AuthenticationContext == null) return item;
                if (AuthenticationContext.User is ClaimsIdentity)
                    foreach (var i in (AuthenticationContext.User as ClaimsIdentity).Claims)
                    {
                        item.Add(i.Type, i.Value);
                    }
                return item;
            }
        }

    }
}




