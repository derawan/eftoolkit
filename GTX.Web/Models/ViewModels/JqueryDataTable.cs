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

using GTX.Commands;
using System.Collections.Generic;

namespace GTX.Web.Models
{
    public class JqueryDataTable<T> : IJqueryDataTables<T>
        where T : class 
    {
        public JqueryDataTable()
        {
            data = new List<T>();
        }

#pragma warning disable IDE1006 // Naming Styles
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<T> data { get; set; }
        public string error { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public static implicit operator JqueryDataTable<T>(BaseFetchCommandResult<T> d) 
        {
            var result = new JqueryDataTable<T>
            {
                draw = ((BaseFetchResultData<T>)d.Result).Page,
                recordsFiltered = ((BaseFetchResultData<T>)d.Result).Total,
                recordsTotal = ((BaseFetchResultData<T>)d.Result).Total,
                data = ((BaseFetchResultData<T>)d.Result).Rows
            };
            return result;
        }
    }
}
