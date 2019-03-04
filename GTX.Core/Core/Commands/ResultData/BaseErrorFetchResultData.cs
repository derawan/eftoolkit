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

namespace GTX.Commands
{


    public class BaseErrorFetchResultData<TModel> : 
        BaseErrorResultData<TModel>,
        IErrorFetchResultData<TModel>
        where TModel : class
    {
        public BaseErrorFetchResultData()
            : this(null, GlobalErrorCode.DefaultSystemError)
        {

        }

        public BaseErrorFetchResultData(Exception error, long errorCode = GlobalErrorCode.DefaultSystemError)
            : base(error, errorCode)
        {
            Page = 1;
            RowCount = -1;
            Total = -1;
            TotalPages = 1;
            Rows = null;
        }

        public BaseErrorFetchResultData(Exception error, long errorCode = GlobalErrorCode.DefaultSystemError, int page = 1, int rowCount = 0, int total = 0)
            : this(error, errorCode)
        {
            Page = page;
            RowCount = rowCount;
            Total = total;
            TotalPages = 1;
            Rows = null;
        }


        public int Total
        {
            get;
            set;
        }

        /// <summary>
        /// Page Number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of Rows per Page
        /// </summary>
        public int RowCount { get; set; }

        public IEnumerable<TModel> Rows
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


    }
}
