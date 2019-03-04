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
///using Autofac.Integration.Web;
using GTX.Bootstrapper;
using GTX.Web.Models;
using System;
using System.IO;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using GTX.Models;

namespace GTX.Web
{
    public class GTXHttpApplication : HttpApplication, IGTXHttpApplication, IDisposableObjects
    {
        private Guid _contextId = Guid.Empty;
        private bool _isDisposed;

        public GTXHttpApplication() : this(Guid.NewGuid())
        {
            ContextId = Guid.NewGuid();
            OnInits += GTXHttpApplication_OnInits;
            AuthenticateRequest += GTXHttpApplication_AuthenticateRequest;
            PostAuthenticateRequest += GTXHttpApplication_PostAuthenticateRequest;
            AuthorizeRequest += GTXHttpApplication_AuthorizeRequest;
            PostAuthorizeRequest += GTXHttpApplication_PostAuthorizeRequest;
            BeginRequest += GTXHttpApplication_BeginRequest;
            EndRequest += GTXHttpApplication_EndRequest;
            ResolveRequestCache += GTXHttpApplication_ResolveRequestCache;
            PostResolveRequestCache += GTXHttpApplication_PostResolveRequestCache;
            MapRequestHandler += GTXHttpApplication_MapRequestHandler;
            PostMapRequestHandler += GTXHttpApplication_PostMapRequestHandler;
            PostAcquireRequestState += GTXHttpApplication_PostAcquireRequestState;
            PreRequestHandlerExecute += GTXHttpApplication_PreRequestHandlerExecute;
            PostRequestHandlerExecute += GTXHttpApplication_PostRequestHandlerExecute;
            ReleaseRequestState += GTXHttpApplication_ReleaseRequestState;
            PostReleaseRequestState += GTXHttpApplication_PostReleaseRequestState;
            UpdateRequestCache += GTXHttpApplication_UpdateRequestCache;
            PostUpdateRequestCache += GTXHttpApplication_PostUpdateRequestCache;
            Error += GTXHttpApplication_Error;
            LogRequest += GTXHttpApplication_LogRequest;
            PostLogRequest += GTXHttpApplication_PostLogRequest;
            PreSendRequestContent += GTXHttpApplication_PreSendRequestContent;
            PreSendRequestHeaders += GTXHttpApplication_PreSendRequestHeaders;
            RequestCompleted += GTXHttpApplication_RequestCompleted;
        }

        public GTXHttpApplication(Guid contextId) : base()
        {

            ApplicationId = GTX_Reflection_Utility.GetAssemblyId();
            ApplicationName = GTX_Reflection_Utility.GetAppName();
            ApplicationIpAddress = GTX_System_Utility.GetLocalIpAddress();
            ApplicationToken = GTX_String_Utility.RngCharacterMask(200);
            ApplicationStartedOn = DateTime.Now;

        }


        /// <summary>
        /// Application Id
        /// </summary>
        public virtual string ApplicationId
        {
            get;
            set;
        }

        /// <summary>
        /// The Name Of The Application
        /// </summary>
        public virtual string ApplicationName { get; set; }

        /// <summary>
        /// The Ip Address Of The Application
        /// </summary>
        public virtual string ApplicationIpAddress { get; set; }

        /// <summary>
        /// Application Token
        /// </summary>
        public virtual string ApplicationToken
        {
            get;
            set;
        }

        /// <summary>
        /// Application Started Time
        /// </summary>
        public virtual DateTime ApplicationStartedOn
        {
            get;
            set;
        }

        public static Website Website { get; set; }

        public override void Init()
        {
            base.Init();
        }

        protected virtual void Application_Start()
        {
            ApplicationId = GTX_Reflection_Utility.GetAssemblyId();
            ApplicationName = GTX_Reflection_Utility.GetAppName();
            ApplicationIpAddress = GTX_System_Utility.GetLocalIpAddress();
            ApplicationToken = GTX_String_Utility.CreateUniqueId;
            ApplicationStartedOn = DateTime.Now;
            BootStrapManager.ApplicationContext = this;
            BootStrapManager.Init<AutofacBootstrap>(this);
            Initialize();
        }

        protected virtual void Application_End()
        {
            BootStrapManager.StopApplication();
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
        }

        protected virtual void GTXHttpApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;
        }

        protected virtual void GTXHttpApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_AuthorizeRequest(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostAuthorizeRequest(object sender, EventArgs e)
        {
            
        }

        protected virtual void GTXHttpApplication_ResolveRequestCache(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostResolveRequestCache(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_MapRequestHandler(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostMapRequestHandler(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostAcquireRequestState(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PreRequestHandlerExecute(object sender, EventArgs e)
        {

        }


        /********************************************************************************/
        /* Create Controller */

        /* Release Controller */
        /********************************************************************************/

        protected virtual void GTXHttpApplication_PostRequestHandlerExecute(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_ReleaseRequestState(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostReleaseRequestState(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_UpdateRequestCache(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostUpdateRequestCache(object sender, EventArgs e)
        {

        }

        protected virtual void Application_Error()
        {

        }

        protected virtual void GTXHttpApplication_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            OnExceptionRaised(this, "GTXHttpApplication_Error", error);
        }

        protected virtual void GTXHttpApplication_LogRequest(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PostLogRequest(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_EndRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected virtual void GTXHttpApplication_PreSendRequestContent(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_PreSendRequestHeaders(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_RequestCompleted(object sender, EventArgs e)
        {

        }

        protected virtual void GTXHttpApplication_OnInits(object sender, EventDynamicArgs e)
        {

        }

        protected void GTXHttpApplication_BeginRequest(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Context Id
        /// </summary>
        public virtual Guid ContextId
        {
            get { return _contextId; }
            set { _contextId = value; }
        }

        public string TenantId { get; set; }

        public event EventHandler<EventDynamicArgs> OnInits;

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

        /// <summary>
        /// override this
        /// </summary>
        public virtual void DisposeCore()
        {

        }

        public override void Dispose()
        {
            base.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            var args = new EventDynamicArgs();
            dynamic arg = args.Parameters;
            arg.ClassName = GetType().Name;
            OnBeforeDisposed(this, args);
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }
            OnAfterDisposed(this, args);
            _isDisposed = true;
        }

        ~GTXHttpApplication()
        {
            Dispose(false);
        }

        public event EventHandler<EventDynamicArgs> BeforeDisposed;

        /// <summary>
        /// OnBeforeDispose 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnBeforeDisposed(object sender, EventDynamicArgs args)
        {
            BeforeDisposed?.Invoke(this, args);
        }

        public event EventHandler<EventDynamicArgs> AfterDisposed;

        /// <summary>
        /// OnAfterDisposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnAfterDisposed(object sender, EventDynamicArgs args)
        {
            AfterDisposed?.Invoke(this, args);
        }

        public virtual ITenant GetTenant(string domainname)
        {
            Website website = null;
            //switch (domainname)
            //{
            //    case "localhost":
            //        website = new Website
            //        {
            //            Id = "201806A9E49",
            //            Name = "ONstore",
            //            DomainName = "localhost",
            //            Url = "http://localhost:44386/",
            //            CompanyName = "PT ONstore Indonesia Mandiri",
            //            ViewPath = "onstore_metronic"
            //        };
            //        break;
            //    case "dev.onst.co.id":
            //        website = new Website
            //        {
            //            Id = "201806A9E41",
            //            Name = "ONstore2",
            //            DomainName = "dev.onst.co.id",
            //            Url = "http://dev.onst.co.id:44386/",
            //            CompanyName = "PT ONstore Indonesia Mandiri",
            //            ViewPath = "onstore_metronic"
            //        };
            //        break;
            //}
            return website;
        }
    }
}
