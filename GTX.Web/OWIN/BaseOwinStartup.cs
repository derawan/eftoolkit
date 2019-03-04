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
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using System.Web;
using System.Web.SessionState;

namespace GTX.OWIN
{

    public abstract class BaseOwinStartup : BaseContext, IOwinStartup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseOwinStartup(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseOwinStartup()
            : this(Guid.NewGuid())
        {
        }

        public virtual void Configuration(IAppBuilder app)
        {
            ConfigureOwin(app);
        }

        public virtual void ConfigureOwin(IAppBuilder app)
        {
            try
            {
                app.Use<GTXOwinMiddleware>();
                /// IIS related
                RequireAspNetSession(app);
                app.UseRequestScopeContext();
                app.UseAutofacMiddleware(((Bootstrapper.AutofacBootstrap)Bootstrapper.BootStrapManager.Bootstrap).DependencyContainer);
                // enable Cors
                app.UseCors(CorsOptions.AllowAll);

                // if we use signalr this shoud be remark, it has to be done aftar mapSignalr
                // app.UseWebApi(GlobalConfiguration.Configuration);
                // if you used webapi and signalr the mvc path cannot be access - still don't know who to fixed that :(
            }
            catch(Exception error)
            {
                OnExceptionRaised(this, "ConfigureOwin", error);
                throw error;
            }
        }

        public IAppBuilder RequireAspNetSession(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                // Depending on the handler the request gets mapped to, session might not be enabled. Force it on.
                HttpContextBase httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
                return next();
            });
            // SetSessionStateBehavior must be called before AcquireState
            app.UseStageMarker(PipelineStage.PostResolveCache);
            return app;
        }

    }

    public class SelfHostedOwinStartup : BaseOwinStartup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public SelfHostedOwinStartup(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SelfHostedOwinStartup()
            : this(Guid.NewGuid())
        {
        }

        public override void ConfigureOwin(IAppBuilder app)
        {
            try
            {
                /// For self-hosted environtment we do not support MVC App right now
                //  RequireAspNetSession(app);
                app.UseRequestScopeContext();

                app.Use<GTXOwinMiddleware>();
                app.UseAutofacMiddleware(((Bootstrapper.AutofacBootstrap)Bootstrapper.BootStrapManager.Bootstrap).DependencyContainer);
                // enable Cors
                app.UseCors(CorsOptions.AllowAll);
                app.UseStageMarker(PipelineStage.MapHandler);
                // if we use signalr this should be called
                // app.MapSignalR();
                // if we use signalr this shoud be remark, it has to be done aftar mapSignalr
                // app.UseWebApi(GlobalConfiguration.Configuration);
                // if you used webapi and signalr the mvc path cannot be access - still don't know who to fixed that :(
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "ConfigureOwin", error);
                throw error;
            }
        }
    }
}
