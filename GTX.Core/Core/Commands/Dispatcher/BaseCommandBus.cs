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
//using Newtonsoft.Json;
//using KellermanSoftware.CompareNetObjects;
using GTX.Commands;
//using ServiceStack.Redis;
//using ServiceStack.Messaging.Redis;
//using ServiceStack.Messaging;
using GTX.Bootstrapper;

namespace GTX.Core
{
    public class BaseCommandBus : BaseContext, ICommandBus
    {
        //protected IRedisClientsManager RedisFactory;
        //protected RedisMqServer MqHost;
        //protected IMessageQueueClient Client;

        //public virtual void InitializeQueueServer(string serverAddress = "", int queueRetries = -1)
        //{
        //    if (string.IsNullOrEmpty(serverAddress))
        //        serverAddress = GlobalConfig.RedisQueueServerAddress;
        //    if (queueRetries == -1)
        //        queueRetries = GlobalConfig.QueueProcessRetries;
        //    RedisFactory = new PooledRedisClientManager(serverAddress);
        //    MqHost = new RedisMqServer(RedisFactory, queueRetries);
        //}

        //public ILogger Logger { get; set; }

        public BaseCommandBus() : this(Guid.NewGuid())
        {
        }

        public BaseCommandBus(Guid contextId)
            : base(contextId)
        {
            _items = new Dictionary<string, object>();
            try
            {
                //NLogger = BootStrapManager.AppLogger;
                //Logger = BootStrapManager.Logger;
                //InitializeQueueServer();
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Constructor", error);
            }
        }

        public virtual IApplicationContext ApplicationContext
        {
            get { return BootStrapManager.ApplicationContext; }
        }

        public virtual IRequestContext RequestContext
        {
            get { return BootStrapManager.RequestContext; }
        }

        public virtual IAuthenticationContext AuthenticationContext
        {
            get { return BootStrapManager.AuthenticationContext; }
        }

        public virtual ICommandResult<TModel> Executes<THandler, TCommandParameter, TModel, Tkey>(TCommandParameter parameter)
            where THandler : ICommandHandler<TCommandParameter, TModel>
            where TCommandParameter : IBaseCommandParameters<TModel, Tkey>
            where TModel : class
        {
            ICommandHandler<TCommandParameter, TModel> handler = null;
            try
            {
                handler = BootStrapManager.Resolve<THandler>();
                if (handler.IsNull())
                    throw new CommandHandlerNotFoundException(typeof(TCommandParameter), typeof(TModel));
                /* Do Authentication */
                handler.CommandBus = this;
                if (!BootStrapManager.IsAuthenticated<TCommandParameter, TModel, Tkey>(handler)) throw new AuthenticationException("USER UNAUTHORIZED");
                // InitPredefinedParameters(parameter);
                var result = ExecuteHandler(parameter, handler);
                return result;
            }
            catch (CommandHandlerNotFoundException ex)
            {
                HandleExecuteError(handler, ex);
                throw;
            }
            catch (AuthenticationException err1)
            {
                HandleExecuteError(handler, err1);
                throw;
            }
            catch (Exception error)
            {
                error = new CommandHandlerExecuteException(typeof(TCommandParameter), typeof(TModel), error);
                HandleExecuteError(handler, error);
                throw;
            }
            throw new NotImplementedException();
        }


        public virtual ICommandResult<TDataModel> ExecuteCommand<TDataModel, TVDataModel, TCommandParameter, THandler, TResult, TCommandResultFail, TCommandResultSuccess, TKey>
                (TCommandParameter parameter)
                where TCommandParameter : IBaseCommandParameters<TVDataModel, TKey>
                where THandler : ICommandHandler<TCommandParameter, TDataModel>
                where TResult : ICommandResult<TVDataModel>
                where TCommandResultFail : BaseErrorExecuteResultData<TVDataModel>
                where TCommandResultSuccess : BaseExecuteResultData<TVDataModel>
                where TDataModel : class
                where TVDataModel : class
        {
            //NLogger = BootStrapManager.AppLogger;
            //LogDebug(this, "ExecuteCommand", "STRING Executing ....", this);
            //BootStrapManager.RepositoryContext.StartTransaction();
            //ICommandHandler<TCommandParameter, TDataModel> handler = null;
            //try
            //{   
            //    handler = BootStrapManager.Resolve<THandler>();
            //    if (handler.IsNull())
            //        throw new CommandHandlerNotFoundException(typeof(TCommandParameter), typeof(TVDataModel));
            //    /* set handler commandbus */
            //    handler.CommandBus = this;
            //    /* authenticate */
            //    // if (!BootStrapManager.IsAuthenticated(handler) throw new AuthenticationException("USER UNAUTHORIZED");
            //    var result = ExecuteHandlerEx<TCommandParameter, TVDataModel, TDataModel>(parameter, handler);
            //    BootStrapManager.RepositoryContext.SaveChanges();
            //    LogDebug(this, "ExecuteCommand", "STRING Executed ...", this);
            //    return result;
            //}
            //catch (CommandHandlerNotFoundException ex)
            //{
            //    HandleExecuteError(handler, ex);
            //    throw;
            //}
            //catch (AuthenticationException err1)
            //{
            //    HandleExecuteError(handler, err1);
            //    throw;
            //}
            //catch (Exception error)
            //{
            //    error = new CommandHandlerExecuteException(typeof(TCommandParameter), typeof(TVDataModel), error);
            //    HandleExecuteError(handler, error);
            //    throw;
            //}
            throw new NotImplementedException();
        }

        public virtual ICommandResult<TDataModel> ExecuteQuery<TDataModel, TVDataModel, TCommandParameter, THandler, TResult, TCommandResultFail, TCommandResultSuccess, TKey>(TCommandParameter parameter)
            where TCommandParameter : IBaseCommandParameters<TVDataModel, TKey>
                where THandler : ICommandHandler<TCommandParameter, TDataModel>
                where TResult : ICommandResult<TVDataModel>
                where TCommandResultFail : BaseErrorExecuteResultData<TVDataModel>
                where TCommandResultSuccess : BaseExecuteResultData<TVDataModel>
                where TDataModel : class
                where TVDataModel : class
        {
            //NLogger = BootStrapManager.AppLogger;
            //LogDebug(this, "ExecuteQuery", "STRING Executing ....", this);
            //ICommandHandler<TCommandParameter, TDataModel> handler = null;
            //try
            //{
            //    handler = BootStrapManager.Resolve<THandler>();
            //    if (handler.IsNull())
            //        throw new CommandHandlerNotFoundException(typeof(TCommandParameter), typeof(TVDataModel));
            //    /* set handler commandbus */
            //    handler.CommandBus = this;
            //    /* authenticate */
            //    // if (!BootStrapManager.IsAuthenticated(handler) throw new AuthenticationException("USER UNAUTHORIZED");
            //    var result = ExecuteHandlerEx<TCommandParameter, TVDataModel, TDataModel>(parameter, handler);
            //    LogDebug(this, "ExecuteQuery", "STRING Executed ...", this);
            //    return result;
            //}
            //catch (CommandHandlerNotFoundException ex)
            //{
            //    HandleExecuteError(handler, ex);
            //    throw;
            //}
            //catch (AuthenticationException err1)
            //{
            //    HandleExecuteError(handler, err1);
            //    throw;
            //}
            //catch (Exception error)
            //{
            //    error = new CommandHandlerExecuteException(typeof(TCommandParameter), typeof(TVDataModel), error);
            //    HandleExecuteError(handler, error);
            //    throw;
            //}
            throw new NotImplementedException();

        }




        protected virtual void HandleExecuteError<TCommandParameter, TResult>(ICommandHandler<TCommandParameter, TResult> handler, Exception error)
            where TCommandParameter : ICommand
            where TResult : class
        {
            //var str = "Executes";
            //if (error is CommandHandlerNotFoundException)
            //{
            //    str = "Handler Not Registered";
            //}
            //else if (error is AuthenticationException)
            //{
            //    str = "Unauthorized";
            //}
            //if (handler.IsNotNull())
            //{
            //    handler.OnExceptionRaised(handler, str, error);
            //    handler.OnExecuted();
            //}
            //OnExceptionRaised(this, str, error);
            throw new NotImplementedException();
        }

        protected virtual ICommandResult<TResult> ExecuteHandler<TCommandParameter, TResult>(TCommandParameter parameter,
            ICommandHandler<TCommandParameter, TResult> handler)
            where TCommandParameter : ICommand
            where TResult : class
        {
            //NLogger = BootStrapManager.AppLogger;
            //ICommandResult<TResult> result;
            //var transactionId = Guid.NewGuid().ToString();
            //result = null;
            //IResultData<TResult> c = default(IResultData<TResult>);
            //try
            //{
            //    handler.CommandBus = this;

            //    //InitPredefinedParameters(parameter);

            //    handler.OnBeforeExecute();
            //    result = handler.Execute(parameter);
            //    if (result.CommandResultType.Equals(CommandResultTypes.Error))
            //    {
            //        var baseErrorResultData = result.Result as BaseErrorResultData<TResult>;
            //        if (baseErrorResultData != null)
            //        {
            //            var error = baseErrorResultData.Exception;
            //            throw error;
            //        }
            //    }
            //    c = result.Result;
            //    /* if success do commit transaction*/
            //    if (parameter.CommandType != CommandTypes.Fetch)
            //    {
            //        CreateAuditTrailLog(parameter, result, c, transactionId);
            //    }
            //    handler.OnExecuted();
            //}
            //catch (Exception errors)
            //{
            //    OnExceptionRaised(this, "ExecuteHandler", errors);
            //    if (parameter.CommandType != CommandTypes.Fetch)
            //    {
            //        CreateAuditTrailLog(parameter, result, c, transactionId);
            //        //var model = CreateAuditTrailData(parameter, result, c, transactionId);
            //        // not yet used
            //    }
            //    throw;
            //}
            //return result;
            throw new NotImplementedException();
        }

        protected virtual ICommandResult<TResult> ExecuteHandlerEx<TCommandParameter, TVDataModel, TResult, TKey>(TCommandParameter parameter,
            ICommandHandler<TCommandParameter, TResult> handler)
            where TCommandParameter : IBaseCommandParameters<TVDataModel, TKey>
            where TVDataModel : class
            where TResult : class
        {
            //NLogger = BootStrapManager.AppLogger;
            //ICommandResult<TResult> result;
            //var transactionId = Guid.NewGuid().ToString();
            //result = null;
            //IResultData<TResult> c = default(IResultData<TResult>);
            //try
            //{
            //    handler.CommandBus = this;

            //    //InitPredefinedParameters(parameter);

            //    handler.OnBeforeExecute();
            //    result = handler.Execute(parameter);
            //    if (result.CommandResultType.Equals(CommandResultTypes.Error))
            //    {
            //        var baseErrorResultData = result.Result as BaseErrorResultData<TResult>;
            //        if (baseErrorResultData != null)
            //        {
            //            var error = baseErrorResultData.Exception;
            //            throw error;
            //        }
            //    }
            //    c = result.Result;
            //    /* if success do commit transaction*/
            //    if (parameter.CommandType != CommandTypes.Fetch)
            //    {
            //        CreateAuditTrailLog(parameter, result, c, transactionId);
            //    }
            //    handler.OnExecuted();
            //}
            //catch (Exception errors)
            //{
            //    OnExceptionRaised(this, "ExecuteHandlerEx", errors);
            //    if (parameter.CommandType != CommandTypes.Fetch)
            //    {
            //        CreateAuditTrailLog(parameter, result, c, transactionId);
            //        //var model = CreateAuditTrailData(parameter, result, c, transactionId);
            //        // not yet used
            //    }
            //    throw;
            //}
            //return result;
            throw new NotImplementedException();
        }

        //private AuditData CreateAuditTrailData<TCommandParameter, TResult>(TCommandParameter parameter, ICommandResult<TResult> result, IResultData<TResult> c, string transactionId, Exception errors = null)
        //    where TCommandParameter : ICommand
        //    where TResult : class
        //{
        //    var commandType = parameter.CommandType;
        //    var issuccess = (result as ICommandResult<TResult>).Success;
        //    var msgs = (result as ICommandResult<TResult>).Message.Text;
        //    var executeResultData = c as IExecuteResultData<TResult>;
        //    if (executeResultData == null) return null;
        //    var newItem = executeResultData.IsNotNull() ? executeResultData.NewDataItem : null;
        //    var prevItem = executeResultData.IsNotNull() ? executeResultData.PreviousDataItem : null;
        //    var param = parameter;

        //    var actionName = "";
        //    switch (param.CommandType)
        //    {
        //        case CommandTypes.Add: actionName = "ADD"; break;
        //        case CommandTypes.Edit: actionName = "EDIT"; break;
        //        case CommandTypes.Delete: actionName = "DELETE"; break;
        //        case CommandTypes.Remove: actionName = "REMOVE"; break;
        //        case CommandTypes.Restore: actionName = "RESTORE"; break;
        //        case CommandTypes.Approve: actionName = "APPROVE"; break;
        //        case CommandTypes.Reject: actionName = "REJECT"; break;
        //        case CommandTypes.Review: actionName = "REVIEW"; break;
        //        case CommandTypes.Confirm: actionName = "CONFIRM"; break;
        //        case CommandTypes.Publish: actionName = "PUBLISH"; break;
        //        case CommandTypes.Unpublish: actionName = "UNPUBLISH"; break;
        //        case CommandTypes.Register: actionName = "REGISTER"; break;
        //        case CommandTypes.Unregister: actionName = "UNREGISTER"; break;
        //        case CommandTypes.Activate: actionName = "ACTIVATE"; break;
        //        case CommandTypes.Deactivate: actionName = "DEACTIVATE"; break;
        //        case CommandTypes.Login: actionName = "LOGIN"; break;
        //        case CommandTypes.Logout: actionName = "LOGOUT"; break;
        //        /*case CommandTypes.Fetch: 
        //            break;
        //        case CommandTypes.Download:
        //            break;
        //        case CommandTypes.Commit:
        //            break;
        //        */
        //        case CommandTypes.Upload:
        //            break;
        //        case CommandTypes.Unknown:
        //            break;
        //        case CommandTypes.System:
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //    var user = BootStrapManager.AuthenticatedUser();

        //    var diff = "";
        //    var comparelogic = new CompareLogic { Config = { MaxDifferences = 5000 } };
        //    var compareresult = comparelogic.Compare(prevItem, newItem);

        //    if (!compareresult.AreEqual)
        //    {

        //        diff = compareresult.DifferencesString;
        //    }
        //    var ctx = BootStrapManager.RequestContext.IsNull() ? Guid.NewGuid().ToString() : BootStrapManager.RequestContext.ContextId.ToString();
        //    var model = new AuditData
        //    {
        //        TimeStampFrom = DateTime.UtcNow,
        //        TimeStampTo = DateTime.UtcNow,
        //        TimeStamp = DateTime.UtcNow.AddHours(7),
        //        EntityName = (typeof(TResult)).Name,
        //        Username = user,
        //        ActionName = actionName,
        //        HotelName = "",
        //        Differences = diff,
        //        LogId = string.Format("{0:yyyyMMddhhmmss}::{1}", DateTime.Now, Guid.NewGuid()).Replace("-", ""), // ContextId.ToString(),
        //        LogLevel = "AUDIT TRAIL",
        //        LogTime = DateTime.UtcNow,
        //        PreviousData = JsonConvert.SerializeObject(prevItem),
        //        NewData = JsonConvert.SerializeObject(newItem),
        //        IsSuccess = issuccess,
        //        Message = msgs,
        //        Context = ctx,
        //        TransactionId = transactionId
        //    };
        //    if (errors.IsNotNull())
        //        model.ErrorMessage = errors.Message;

        //    return model;
        //}

        //private void CreateAuditTrailLog<TCommandParameter, TResult>(TCommandParameter parameter, ICommandResult<TResult> result, IResultData<TResult> c, string transactionId, Exception _errors = null)
        //    where TCommandParameter : ICommand
        //    where TResult : class
        //{
        //    NLogger = BootStrapManager.AppLogger;
        //    try
        //    {
        //        // Create Audit Trail Logs
        //        var model = CreateAuditTrailData<TCommandParameter, TResult>(parameter, result, c, transactionId, _errors);
        //        if (!GlobalConfig.EnableAuditTrail) return;
        //        if (BootStrapManager.Logger.IsNotNull()) BootStrapManager.Logger.LogAuditTrail(model);
        //    }
        //    catch (Exception errors)
        //    {
        //        OnExceptionRaised(this, "CreateAuditTrailLog", errors);
        //        // we don't need to rethrow this exception because it's just logging throw;
        //    }


        //}

        Dictionary<string, object> _items;

        public virtual Dictionary<string, object> Items
        {
            get { return _items; }
        }

    }
}
