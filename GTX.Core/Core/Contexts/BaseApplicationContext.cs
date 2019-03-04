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
using GTX.Models;
using System;

namespace GTX
{
    public class BaseApplicationContext : BaseContext, IApplicationContext
    {
        public BaseApplicationContext() : this(Guid.NewGuid())
        {
        }

        public BaseApplicationContext(Guid contextId) :
            base(contextId)
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

        public virtual string TenantId
        {
            get; set;
        }

        public virtual ITenant GetTenant(string domainname)
        {
            return default(ITenant);
        }

        public override string ToString()
        {
            return string.Format("{0}::{1}:{2}",ContextId.ToString(), ApplicationId, ApplicationName);
        }

    }
}
