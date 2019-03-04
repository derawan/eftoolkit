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

namespace GTX
{
    public interface IRequestContext : IContext, IDisposableObjects
    {

        DateTime RequestTime { get; set; }

        DateTime RequestProcessed { get; set; }

        string User { get; set; }

        string UserIP { get; set; }

        string Uri { get; set; }

        string Host { get; set; }

        /// <summary>
        /// GET/POST/DELETE/CREATE/PUT ETC
        /// </summary>
        string RequestMethod { get; set; }


        string Query { get; set; }

        Dictionary<string, string> QueryString { get; }

        /// <summary>
        /// Request Headers Values
        /// </summary>
        Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Request Cookies Values
        /// </summary>
        Dictionary<string, string> Cookies { get; }

        Dictionary<string, string> Forms { get; }

        Dictionary<string, string> Files { get; }

        /// <summary>
        /// Browser User Agent
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Browser Name
        /// </summary>
        string Browser { get; set; }

        /// <summary>
        /// Browser Version
        /// </summary>
        string BrowserVersion { get; set; }

        /// <summary>
        /// Operating System
        /// </summary>
         
        string OS { get; set; }

        /// <summary>
        /// Is A Mobile Device
        /// </summary>
        bool IsAMobileDevice { get; set; }

        /// <summary>
        /// Mobile Device Brand
        /// </summary>
        string DeviceBrand { get; set; }

        /// <summary>
        /// Device Family
        /// </summary>
        string DeviceFamily { get; set; }

        /// <summary>
        /// Device Model
        /// </summary>
        string DeviceModel { get; set; }

        /// <summary>
        /// Url Referer
        /// </summary>
        string UrlReferer { get; set; }

        /// <summary>
        /// Referer is a search engine
        /// </summary>
        bool ReferrerIsSearchEngine { get; set; }

        /// <summary>
        /// Search Engine Name
        /// </summary>
        string SearchEngine { get; set; }

        /// <summary>
        /// Search Engine Query 
        /// </summary>
        string SearchEngineKeywords { get; set; }

        /// <summary>
        /// Current Request Culture
        /// </summary>
        string Culture { get; set; }

        /// <summary>
        /// Request Status Code
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Application Context Id
        /// </summary>
        string AppContextId { get; set; }

        /// <summary>
        /// Activity Context Id
        /// </summary>
        string ActivityContext { get; set; }

        /// <summary>
        /// Session Id
        /// </summary>
        string SessionId { get; set; }

        IApplicationContext ApplicationContext { get; set; }

        //////IAuthenticationContext AuthenticationContext { get; set; }

        Exception Error { get; set; }

        bool IsAjax();
    }

}
