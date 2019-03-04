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
*/using System;
using System.Collections.Generic;
using GTX.Commands;

namespace GTX.Core
{

    public interface ICommandHandler<in TCommandParameter, TResult> : IHandler
        where TCommandParameter : ICommand
        where TResult : class
    {
        event EventHandler BeforeExecute;

        void OnBeforeExecute();

        event EventHandler Executed;

        void OnExecuted();

        ICommandBus CommandBus { get; set; }

        ICommandResult<TResult> Execute(TCommandParameter parameter);

        bool Validate<T1, T2, T3>(TCommandParameter parameter)
            where T2 : ICommandResult<TResult>
            where T3 : IErrorResultData<TResult>;

        void ConstructResult<T1, T2, T3>(
            IResultData<TResult> result, 
            TResult parameter, 
            TResult parameter2, 
            string message1, 
            string message2 = "")
            where T1 : ICommandResult<TResult>
            where T2 : IExecuteResultData<TResult>
            where T3 : IErrorExecuteResultData<TResult>;

        void ConstructQueryResult<T1, T2, T3, T4>(
            IResultData<TResult> result,
            IBaseCommandParameters<TResult, T4> parameter,
            int page,
            int rowCount,
            IEnumerable<TResult> rows,
            int total,
            string successMessage, string failMessage = "")
            where T1 : ICommandResult<TResult>
            where T2 : IFetchResultData<TResult>
            where T3 : IErrorResultData<TResult>;

        ICommandResult<TResult> ExecuteCommand<TValidationHandler, TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository, TModelKey>(
            TCommandParameter parameter, 
            string successMessage, 
            string failMessage = ""
        )
            where TCommandResult : ICommandResult<TResult>
            where TCommandResultFail : IErrorExecuteResultData<TResult>
            where TCommandResultSuccess : IExecuteResultData<TResult>
            where TRepository : IRepository<TResult, TModelKey>;

        ICommandResult<TResult> ExecuteQuery<TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository, TModelKey>(
            TCommandParameter parameter, 
            string successMessage, 
            string failMessage = ""
        )
            where TCommandResult : ICommandResult<TResult>
            where TCommandResultFail : IErrorFetchResultData<TResult>
            where TCommandResultSuccess : IFetchResultData<TResult>
            where TRepository : IRepository<TResult, TModelKey>;


        /*************************************************************************************************************************************/

    }
}
