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
using GTX.Commands;

namespace GTX.Core
{

    public interface ICommandBus : IContext, IDisposableObjects
    {
        IApplicationContext ApplicationContext { get;}
        IRequestContext RequestContext { get; }
        IAuthenticationContext AuthenticationContext { get; }
        //ISessionContext SessionContext { get; }
        Dictionary<string, object> Items { get; }

        ICommandResult<TModel> Executes<THandler, TCommandParameter, TModel, TKey>(TCommandParameter parameter)
            where THandler : ICommandHandler<TCommandParameter, TModel>
            where TCommandParameter : IBaseCommandParameters<TModel, TKey>//ICommand
            where TModel : class;

        ICommandResult<TDataModel> ExecuteCommand<TDataModel, TVDataModel, TCommandParameter, THandler, TResult, TCommandResultFail, TCommandResultSuccess, TKey>
                (TCommandParameter parameter)
                where TCommandParameter : IBaseCommandParameters<TVDataModel, TKey>
                where THandler : ICommandHandler<TCommandParameter, TDataModel>
                where TResult : ICommandResult<TVDataModel>
                where TCommandResultFail : BaseErrorExecuteResultData<TVDataModel>
                where TCommandResultSuccess : BaseExecuteResultData<TVDataModel>
                where TDataModel : class
                where TVDataModel : class;

        ICommandResult<TDataModel> ExecuteQuery<TDataModel, TVDataModel, TCommandParameter, THandler, TResult, TCommandResultFail, TCommandResultSuccess, TKey>(TCommandParameter parameter)
            where TCommandParameter : IBaseCommandParameters<TVDataModel, TKey>
                where THandler : ICommandHandler<TCommandParameter, TDataModel>
                where TResult : ICommandResult<TVDataModel>
                where TCommandResultFail : BaseErrorExecuteResultData<TVDataModel>
                where TCommandResultSuccess : BaseExecuteResultData<TVDataModel>
                where TDataModel : class
                where TVDataModel : class;

    }


    

}
