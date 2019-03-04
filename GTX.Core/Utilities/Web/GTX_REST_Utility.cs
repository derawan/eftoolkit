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
using RestSharp;
using System.Net;

namespace System.Web
{
    public static class GTX_REST_Utility
    {


        public static string ExecuteRestRequest(
            string hostUrl,
            string path,
            Collections.Generic.Dictionary<string, string> headers,
            Collections.Generic.Dictionary<string, string> bodies,
            Collections.Generic.Dictionary<string, string> cookies,
            Collections.Generic.Dictionary<string, string> queries,
            object json_body = null,
            GTX_HttpMethod httpMethod = GTX_HttpMethod.POST,
            Action<IRestClient, IRestRequest> beforeExecuteAction = null,
            Action<IRestClient, IRestRequest, IRestResponse> afterExecuteAction = null
            )
        {
            var mtd = Method.GET;
            switch (httpMethod)
            {
                case GTX_HttpMethod.GET: mtd = Method.GET; break;
                case GTX_HttpMethod.POST: mtd = Method.POST; break;
                case GTX_HttpMethod.DELETE: mtd = Method.DELETE; break;
                case GTX_HttpMethod.HEAD: mtd = Method.HEAD; break;
                case GTX_HttpMethod.MERGE: mtd = Method.MERGE; break;
                case GTX_HttpMethod.OPTIONS: mtd = Method.OPTIONS; break;
                case GTX_HttpMethod.PATCH: mtd = Method.PATCH; break;
                /* Because Method  CONNECT, TRACE not Available we will use POST method instead*/
                case GTX_HttpMethod.TRACE: mtd = Method.POST; break;
                case GTX_HttpMethod.CONNECT: mtd = Method.POST; break;


            }
            var client = new RestClient(hostUrl);
            var request = new RestRequest(path, mtd);
            if (headers != null)
                foreach (var item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            if (bodies != null)
                foreach (var item in bodies)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            if (cookies != null)
                foreach (var item in cookies)
                {
                    request.AddCookie(item.Key, item.Value);
                }
            if (queries != null)
                foreach (var item in queries)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
            if (json_body != null)
            {
                request.AddJsonBody(json_body);
            }
            IRestResponse response = null;
            beforeExecuteAction?.Invoke(client, request);
            response = client.Execute(request);
            afterExecuteAction?.Invoke(client, request, response);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            else
            {
                throw response.ErrorException;
            }
        }


    }
}
