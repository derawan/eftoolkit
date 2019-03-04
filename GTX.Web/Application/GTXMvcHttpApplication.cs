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
using GTX.Bootstrapper;
using System;

namespace GTX.Web
{
    public class GTXMvcHttpApplication : GTXHttpApplication
    {
        public GTXMvcHttpApplication() : this(Guid.NewGuid())
        {

        }

        public GTXMvcHttpApplication(Guid contextId) : base(contextId)
        {
        }

        protected override void Application_Start()
        {
            ContextId = Guid.NewGuid();
            ApplicationId = GTX_Reflection_Utility.GetAssemblyId();
            ApplicationName = GTX_Reflection_Utility.GetAppName();
            ApplicationIpAddress = GTX_System_Utility.GetLocalIpAddress();
            ApplicationToken = GTX_String_Utility.CreateUniqueId;
            ApplicationStartedOn = DateTime.Now;
            BootStrapManager.ApplicationContext = this;
            BootStrapManager.Init<AutofacMvcBootstrapper>(this);
            Initialize();
        }


        protected override void GTXHttpApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            base.GTXHttpApplication_AuthenticateRequest(sender, e);
        }
    }
}
