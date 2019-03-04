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
using System.Collections.Generic;
using System.Linq;

namespace GTX
{
    public abstract class BaseRequestContext : BaseContext, IRequestContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseRequestContext(Guid contextId) : base(contextId)
        {
            Headers = new Dictionary<string, string>();
            Cookies = new Dictionary<string, string>();
            Forms = new Dictionary<string, string>();
            Files = new Dictionary<string, string>();
            QueryString = new Dictionary<string, string>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseRequestContext()
            : this(Guid.NewGuid())
        {
        }


        public virtual DateTime RequestTime { get; set; }
        public virtual DateTime RequestProcessed { get; set; }
        public virtual string User { get; set; }
        public virtual string UserIP { get; set; }
        public virtual string Uri { get; set; }
        public virtual string Host { get; set; }
        public virtual string RequestMethod { get; set; }
        public virtual string Query { get; set; }

        public virtual Dictionary<string, string> QueryString { get; protected set; }

        public virtual Dictionary<string, string> Headers { get; protected set; }

        public virtual Dictionary<string, string> Cookies { get; protected set; }

        public virtual Dictionary<string, string> Forms { get; protected set; }

        public virtual Dictionary<string, string> Files { get; protected set; }

        public virtual string UserAgent { get; set; }
        public virtual string Browser { get; set; }
        public virtual string BrowserVersion { get; set; }
        public virtual string OS { get; set; }
        public virtual bool   IsAMobileDevice { get; set; }
        public virtual string DeviceBrand { get; set; }
        public virtual string DeviceFamily { get; set; }
        public virtual string DeviceModel { get; set; }
        public virtual string UrlReferer { get; set; }
        public virtual bool   ReferrerIsSearchEngine { get; set; }
        public virtual string SearchEngine { get; set; }
        public virtual string SearchEngineKeywords { get; set; }
        public virtual string Culture { get; set; }
        public virtual int    StatusCode { get; set; }
        public virtual string AppContextId { get; set; }
        public virtual string ActivityContext { get; set; }
        public virtual string SessionId { get; set; }

        public virtual IApplicationContext ApplicationContext { get; set; }

        ///public virtual IAuthenticationContext AuthenticationContext { get; set; }

        public Exception Error { get; set; }

        public bool IsAjax()
        {
            if (Headers.Keys.Any(u => u.ToLower().Equals("x-requested-with")))
            {
                return Headers[Headers.Keys.First(u => u.ToLower().Equals("x-requested-with"))].ToLower().Equals("xmlhttprequest");
            }
            else
                return false;
        }
    }
}
