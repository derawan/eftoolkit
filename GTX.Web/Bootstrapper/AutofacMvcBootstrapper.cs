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
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
//using Autofac.Integration.Web;
using System;
using System.Web.Mvc;
using System.Web.Http;

namespace GTX.Bootstrapper
{
    public class AutofacMvcBootstrapper : AutofacBootstrap
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public AutofacMvcBootstrapper(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AutofacMvcBootstrapper()
            : this(Guid.NewGuid())
        {
        }

        //protected virtual IContainerProvider GTXContainerProvider { get;set; }

        public override void RegisterTypes()
        {
            base.RegisterTypes();

            // Register your MVC controllers.
            //Builder.RegisterControllers(AppDomain.CurrentDomain.GetAssemblies());

            // OPTIONAL: Register model binders that require DI.
            //Builder.RegisterModelBinders(AppDomain.CurrentDomain.GetAssemblies());
            //Builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //Builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //Builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //Builder.RegisterFilterProvider();

            // Register your Web API controllers.
            //Builder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());

            // OPTIONAL: Register the Autofac filter provider.
            //Builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // OPTIONAL: Register the Autofac model binder provider.
            //Builder.RegisterWebApiModelBinderProvider();

            
        }

        public override void Initialize()
        {
            base.Initialize();

            // Web Forms
            //GTXContainerProvider = new ContainerProvider(DependencyContainer);

            // MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(DependencyContainer));

            // WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(DependencyContainer);
        }

        public override IRequestContext RequestContext {
            get
            {
                var httpContext = System.Web.HttpContext.Current;
                var owinContext = Owin.OwinRequestScopeContext.Current;
                IRequestContext requestContext = default(IRequestContext);
                if (httpContext!=null)
                {
                    requestContext = (IRequestContext)httpContext.Items["GTX.REQ_CTX"];
                }
                else
                {
                    requestContext = (IRequestContext)owinContext.Items["GTX.REQ_CTX"];
                }
                return requestContext;
            }
        }

        public override IAuthenticationContext AuthenticationContext
        {
            get
            {
                var httpContext = System.Web.HttpContext.Current;
                var owinContext = Owin.OwinRequestScopeContext.Current;
                IAuthenticationContext authContext = default(IAuthenticationContext);
                if (httpContext != null)
                {
                    authContext = (IAuthenticationContext)httpContext.Items["GTX.REQ_AUTH"];
                }
                else
                {
                    authContext = (IAuthenticationContext)owinContext.Items["GTX.REQ_AUTH"];
                }
                return authContext;
            }
        }
    }
}
