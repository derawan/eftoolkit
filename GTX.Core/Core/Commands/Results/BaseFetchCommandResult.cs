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


namespace GTX.Commands
{
    
    public class BaseFetchCommandResult<TModel> : BaseCommandResult<TModel>, IFetchCommandResult<TModel> where TModel : class
    {
        public BaseFetchCommandResult()
            : base(CommandTypes.Fetch, MessageType.Success)
        {
            base.Result = new BaseFetchResultData<TModel>();
            CommandType = CommandTypes.Fetch;
        }

        public BaseFetchCommandResult(CommandTypes commandType, MessageType messageType, Exception error = null, long errorCode = GlobalErrorCode.DefaultSystemError)
            : base(commandType, messageType, error, errorCode)
        {
            if (error == null)
            {
                 
                Result = new BaseFetchResultData<TModel>();
                CommandType = commandType;
                CommandResultType = CommandResultTypes.Fetched;
            }
            else
            {
                 
                Result = new BaseErrorFetchResultData<TModel>(error, errorCode);
                CommandType = commandType;
                CommandResultType = CommandResultTypes.Error;
            }
        }

        public static implicit operator List<TModel>(BaseFetchCommandResult<TModel> value)
        {
            if (value == null) return null;
           return (value.Result as BaseFetchResultData<TModel>).Rows.ToList<TModel>();
        }

        public static implicit operator TModel[](BaseFetchCommandResult<TModel> value)
        {
            if (value == null) return null;
            return (value.Result as BaseFetchResultData<TModel>).Rows.ToArray<TModel>();
        }
    }
}
