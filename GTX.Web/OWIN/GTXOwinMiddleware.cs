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
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Linq;
using System.Web;

namespace GTX.OWIN
{
    public class GTXOwinMiddleware : OwinMiddleware, IContext
    {
        private Guid _contextId = Guid.Empty;


        public GTXOwinMiddleware(OwinMiddleware next) : base(next)
        {
            ContextId = Guid.NewGuid();
            OnInits += GTXOwinMiddleware_OnInits;
            Initialize(); 
        }

        protected virtual void GTXOwinMiddleware_OnInits(object sender, EventDynamicArgs e)
        {
           
        }

        public override async Task Invoke(IOwinContext context)
        {
            #region Before Execute The Api
            IRequestContext requestContext = Bootstrapper.BootStrapManager.Resolve<IRequestContext>();
            try
            {
                
                requestContext.ApplicationContext = Bootstrapper.BootStrapManager.ApplicationContext;
                requestContext.AppContextId = requestContext.ApplicationContext.ApplicationId;
                var x = Bootstrapper.BootStrapManager.AuthenticationContext;
                InitRequestContext(context, requestContext);
                
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Invoke-Before", error);
            }
            #endregion
            var result = Next.Invoke(context);
            #region After Execute The Api
            if (context.Authentication.User.IsNotNull())
            {
                requestContext.User = context.Authentication.User.Identity.Name;
            }
            try
            {
                var authContext = Bootstrapper.BootStrapManager.Resolve<IAuthenticationContext>();
                authContext.User = context.Authentication.User.Identity;
                authContext.LastAccessOn = DateTime.Now;
                var httpContext = System.Web.HttpContext.Current;
                if (httpContext != null)
                {
                    httpContext.Items.SetDictionary("GTX.REQ_AUTH", authContext);
                }
                
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Invoke-After", error);
            }

            #endregion

            await result;
        }

        public void InitRequestContext(IOwinContext context, IRequestContext requestContext)
        {
            var httpContext = System.Web.HttpContext.Current;
            var owinContext = Owin.OwinRequestScopeContext.Current;
            if (httpContext != null)
            {
                httpContext.Items.SetDictionary("GTX.REQ_CTX", requestContext);
            }
            if (owinContext != null)
            {
                owinContext.Items.SetDictionary("GTX.REQ_CTX", requestContext);
            }
            var headers = InitializeRequestHeader(context, requestContext);
            var remoteIp = context.Request.RemoteIpAddress;
            
            var hosts = context.Request.Host;
            var path = context.Request.Path;
            var acceptable_language = headers["Accept-Language"];
            requestContext.UserIP = remoteIp;
            //requestContext.Host = context.Request.Uri.AbsoluteUri.Replace(context.Request.Uri.AbsolutePath,"").Replace("/","").Replace("https://","");
            var domain = context.Request.Uri.DnsSafeHost;
            Bootstrapper.BootStrapManager.ApplicationContext.TenantId = domain;
            string sessionid = string.Empty;
            InitSession(context, ref sessionid, requestContext);
            var userAgent = InitializeUserAgents(headers, requestContext.RequestMethod, path, sessionid, requestContext);
        }

        public void InitSession(IOwinContext context, ref string sessionid, IRequestContext requestContext)
        {
            var httpContext = System.Web.HttpContext.Current;
            var owinContext = Owin.OwinRequestScopeContext.Current;
            if (httpContext != null)
            {
                //requestContext.SessionId = httpContext.Session.IsNull()?"": httpContext.Session.SessionID;
            }

            sessionid = requestContext.SessionId;
        }

        public IHeaderDictionary InitializeRequestHeader(IOwinContext context, IRequestContext requestContext)
        {
            try
            {
                var headers = InitizializeRequestParameters(context, requestContext);
                var method = context.Request.Method;
                if (requestContext.IsNotNull())
                    requestContext.RequestMethod = method;
                var referer = headers["Referer"];
                if (requestContext.IsNotNull())
                    requestContext.UrlReferer = referer;
                return headers;
            }
            catch (Exception errors)
            {
                OnExceptionRaised(this, "InitializeRequestHeader", errors);
                throw;
            }
        }

        public IHeaderDictionary InitizializeRequestParameters(IOwinContext context, IRequestContext requestContext)
        {
            try
            {
                var scheme = context.Request.Scheme;
                var headers = context.Request.Headers;
                if (requestContext.IsNotNull())
                    foreach (var item in headers)
                    {
                        try { requestContext.Headers.Add(item.Key, item.Value.SingleOrDefault()); } catch { }
                    }
                var queries = context.Request.Query;
                if (requestContext.IsNotNull())
                    foreach (var item in queries)
                    {
                        try
                        { requestContext.QueryString.Add(item.Key, item.Value.SingleOrDefault()); }
                        catch { }
                    }
                requestContext.Query = context.Request.QueryString.Value;

                var cookies = context.Request.Cookies;
                if (requestContext.IsNotNull())
                    foreach (var item in cookies)
                    {
                        try
                        { requestContext.Cookies.Add(item.Key, item.Value); }
                        catch { }
                    }
                if (headers.Keys.Any(u=>u.ToLower().Equals("x-requested-with")))
                {
                    // is ajax
                    var test = requestContext.IsAjax();
                }
                return headers;
            }
            catch (Exception errors)
            {
                OnExceptionRaised(this, "InitizializeReqeuestParameters", errors);
                throw;
            }
        }

        public UAParser.ClientInfo InitializeUserAgents(IHeaderDictionary headers, string method, PathString path, string sessid, IRequestContext requestContext)
        {
            try
            {
                var userAgent = UAParser.Parser.GetDefault().Parse(headers["User-Agent"]);
                if (requestContext.IsNotNull())
                {
                    requestContext.OS = userAgent.OS.Family;
                    requestContext.RequestMethod = method;
                    requestContext.Browser = userAgent.UA.Family;
                    requestContext.BrowserVersion = string.Format("{0}.{1}.{2}", userAgent.UA.Major, userAgent.UA.Minor, userAgent.UA.Patch);
                    requestContext.IsAMobileDevice = false;
                    requestContext.StatusCode = 0;
                    requestContext.Uri = path.Value;
                    requestContext.SessionId = sessid;
                    requestContext.DeviceBrand = userAgent.Device.Brand;
                    requestContext.DeviceFamily = userAgent.Device.Family;
                    requestContext.DeviceModel = userAgent.Device.Model;
                    requestContext.RequestTime = DateTime.Now;
                    requestContext.UserAgent = userAgent.String;
                }
                return userAgent;
            }
            catch (Exception errors)
            {
                OnExceptionRaised(this, "InitializeUserAgents", errors);
                throw errors;
            }
        }
        /// <summary>
        /// Context Id
        /// </summary>
        public virtual Guid ContextId
        {
            get { return _contextId; }
            set { _contextId = value; }
        }

        public event EventHandler<EventDynamicArgs> OnInits;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BaseContext_OnInit(object sender, EventDynamicArgs e)
        {

        }

        protected virtual void OnInitialize(object sender, EventDynamicArgs args)
        {
            OnInits?.Invoke(this, args);
        }

        public virtual void Initialize()
        {
            var args = new EventDynamicArgs();
            OnInitialize(this, args);
        }

        /// <summary>
        /// On Exception Event Handler
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnExceptionHandler;


        /// <summary>
        /// On Exception Event Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="methodName"></param>
        /// <param name="error"></param>
        public virtual void OnExceptionRaised(object sender, string methodName, Exception error)
        {
            OnExceptionHandler?.Invoke(this, new ErrorEventArgs(error));
            var context = sender as IContext;
        }
    }
}
