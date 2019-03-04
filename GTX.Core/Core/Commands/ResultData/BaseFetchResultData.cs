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
using System.Collections.Generic;
using System.Linq;


namespace GTX.Commands
{
    public class BaseFetchResultData<TModel> : BaseResultData<TModel>, IFetchResultData<TModel> where TModel : class 
    {
        public BaseFetchResultData() : this(1,1)
        {
        }

        public BaseFetchResultData(int page = 1, int total = 0) 
        {
            Total = total;
            Page = page;
            Rows = new List<TModel>();
        }

        public BaseFetchResultData(int page = 1, int rowCount = 0, int total = 0, IEnumerable<TModel> rows = null) : this(page, total)
        {
            RowCount = rowCount;
            Rows = rows;
        }
        public int Total
        {
            get;
            set;
        }

        public int Page
        {
            get;
            set;
        }

        public IEnumerable<TModel> Rows
        {
            get;
            set;
        }


        public int RowCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; set; }

        public static implicit operator List<TModel>(BaseFetchResultData<TModel> value)
        {
            return value?.Rows.ToList<TModel>();
        }

        public static implicit operator TModel[](BaseFetchResultData<TModel> value)
        {
            return value?.Rows.ToArray<TModel>();
        }

    }
}
