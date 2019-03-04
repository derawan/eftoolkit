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

namespace GTX.Commands
{

    public class BaseCommandFetchParameters<TModel, TModelKey> : BaseModelCommandParameters<TModel, TModelKey>, ICommandFetchParameters<TModel, TModelKey> where TModel : class
    {
        /// <summary>
        /// Page Number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of Rows per Page
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// if you want to use string expression as query
        /// </summary>
        public bool UseStringParameters { get; set; }

        /// <summary>
        /// Query
        /// </summary>
        public Func<TModel, bool> Query { get; set; }

        private string _predicate = string.Empty;

        public BaseCommandFetchParameters()
        {
            UseStringParameters = false;
            RowCount = 0;
            Page = 1;
        }

        /// <summary>
        /// string expression as query
        /// </summary>
        public string Predicate {
            get { return _predicate;  }
            set
            {
                _predicate = value;
                if ((string.IsNullOrEmpty(_predicate) || string.IsNullOrWhiteSpace(_predicate)) && Query.IsNotNull())
                {
                    UseStringParameters = true;
                }
            }
        }

        /// <summary>
        /// string expression parameters
        /// </summary>
        public object[] PredicateParameters { get; set; }

        public string OrderBy { get; set; }
    }
}
