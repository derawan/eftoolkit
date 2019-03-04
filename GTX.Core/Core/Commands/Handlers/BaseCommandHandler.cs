//using ServiceStack.Messaging;
//using ServiceStack.Messaging.Redis;
//using ServiceStack.Redis;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using GTX.Commands;
using GTX.Bootstrapper;
//using OS.Core.Connector;

namespace GTX.Core
{
    public class BaseCommandHandler<TCommandParameter, TResult>: BaseContext, ICommandHandler<TCommandParameter, TResult>
        where TCommandParameter : ICommand
        where TResult : class
    {

        public BaseCommandHandler(ICommandBus commandBus) : this()
        {
            CommandBus = commandBus;
        }

        public BaseCommandHandler()
            : this(Guid.NewGuid())
        {
        }

        public BaseCommandHandler(Guid contextid)
            : base(contextid)
        {
            //NLogger = BootStrapManager.AppLogger;
            //try
            //{
            //    InitializeQueueServer(GlobalConfig.RedisQueueServerAddress, GlobalConfig.QueueProcessRetries);
            //}
            //catch (Exception error)
            //{
            //    OnExceptionRaised(this, "Constructor", error);
            //    throw;
            //}
        }

        //public virtual void InitializeQueueServer(string serverAddress = "", int queueRetries = -1 )
        //{ 
        //    if (string.IsNullOrEmpty(serverAddress))
        //        serverAddress = GlobalConfig.RedisQueueServerAddress;
        //    if (queueRetries == -1)
        //        queueRetries = GlobalConfig.QueueProcessRetries;
        //    RedisFactory = new PooledRedisClientManager(serverAddress);
        //    MqHost = new RedisMqServer(RedisFactory, queueRetries);
        //}

        public virtual bool Validate<T1,T2,T3>(TCommandParameter parameter)
            where T2 : ICommandResult<TResult>
            where T3 : IErrorResultData<TResult> // BaseErrorResultData<TResult>
        {
            try
            {
                var validationHandler = BootStrapManager.Resolve<T1>();
                var mtdInfo = validationHandler.GetType().GetMethod("Validate");
                var validationResult = (IEnumerable<ValidationResult>)mtdInfo.Invoke(validationHandler, new object[] { parameter });
                var ex = new ParameterValidationException(validationResult);
                Result = Activator.CreateInstance<T2>();
                Result.Result = (T3)Activator.CreateInstance(typeof(T3), ex, GlobalErrorCode.ParameterValidationException, parameter.Parameters, null);
                Result.Message.MessageType = MessageType.Error;
                Result.Message.Text = ex.Message;
                Result.Success = false;
                if ((validationResult == null) || (!validationResult.Any()))
                {
                    Result.Success = true;
                    return true;
                }
                Result.Success = false;
                return false;
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Validate", error);
                return false;
            }
        }

        public virtual void ConstructResult<T1,T2, T3>(IResultData<TResult> result, TResult parameter, TResult parameter2, string message1, string message2 = "")
            where T1 : ICommandResult<TResult>
            where T2 : IExecuteResultData<TResult>
            where T3 : IErrorExecuteResultData<TResult>
        {
            try
            {
                if (!(result is BaseErrorResultData<TResult>))
                {
                    Result = Activator.CreateInstance<T1>();
                    Result.Success = true;
                    Result.Result = (T2)Activator.CreateInstance(typeof(T2), parameter, parameter2);
                    Result.Message.MessageType = MessageType.Success;
                    Result.Message.Text = message1;
                }
                else
                {
                    Result = Activator.CreateInstance<T1>();
                    Result.Success = false;
                    if (result is BaseErrorExecuteResultData<TResult> baseErrorExecuteResultData)
                        Result.Result = (T3)Activator.CreateInstance(typeof(T3),
                            baseErrorExecuteResultData.Exception,
                            baseErrorExecuteResultData.ErrorCode,
                            parameter,
                            parameter2);
                    Result.Message.MessageType = MessageType.Error;
                    Result.Message.Text = string.Format(message2, ((BaseErrorResultData<TResult>)result).Exception.Message);
                    /*
                    var baseErrorExecuteResultData = result as BaseErrorExecuteResultData<TResult>;
                    if (baseErrorExecuteResultData != null)
                        Result.Result = (T3)Activator.CreateInstance(typeof(T3),
                            baseErrorExecuteResultData.Exception,
                            baseErrorExecuteResultData.ErrorCode,
                            parameter,
                            parameter2);
                    Result.Message.MessageType = MessageType.Error;
                    Result.Message.Text = string.Format(message2, ((BaseErrorResultData<TResult>)result).Exception.Message);
                     */
                }
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "ConstructResult", error);
                throw;
            }
        }

        public virtual void ConstructQueryResult<T1, T2, T3, T4>(
            IResultData<TResult> result, 
            IBaseCommandParameters<TResult, T4> parameter, 
            int page, 
            int rowCount,
            IEnumerable<TResult> rows,
            int total,
            string message1, string message2 = "")
            where T1 : ICommandResult<TResult>
            where T2 : IFetchResultData<TResult>
            where T3 : IErrorResultData<TResult>
        {
            try
            {
                if (!(result is BaseErrorResultData<TResult>))
                {
                    Result = Activator.CreateInstance<T1>();
                    Result.Success = true;
                    Result.Result = (T2)Activator.CreateInstance(typeof(T2), page, rowCount, total, rows); // (T2)Activator.CreateInstance(typeof(T2), parameter, Page, RowCount, Rows,Total);
                    Result.Message.MessageType = MessageType.Success;
                    Result.Message.Text = message1;
                }
                else
                {
                    Result = Activator.CreateInstance<T1>();
                    Result.Success = false;
                    Result.Result = (T3)Activator.CreateInstance(typeof(T3),
                        ((BaseErrorResultData<TResult>)result).Exception,
                        ((BaseErrorResultData<TResult>)result).ErrorCode,
                        page, rowCount, total);
                    Result.Message.MessageType = MessageType.Error;
                    Result.Message.Text = ((BaseErrorResultData<TResult>)result).Exception.Message;
                }
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "ConstructQueryResult", error);
                throw;
            }
        }
        
        protected ICommandResult<TResult> Result;

        public virtual ICommandResult<TResult> Execute(TCommandParameter parameter)
        {
            try
            {
                parameter.ThrowIfParameterNull<TCommandParameter>();
                return DoExecute(parameter, null);
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Execute", error);
                throw;
            }
        }

        /// <summary>
        /// Override this 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        public virtual ICommandResult<TResult> DoExecute(TCommandParameter parameter, IEnumerable<ValidationResult> validationResult)
        {
            if (parameter.CommandType == CommandTypes.Fetch)
            {
                //return ICommandResult<TResult> ExecuteQuery<TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository>(TCommandParameter parameter, string msg)
            }
            else
            {
                //return ICommandResult<TResult> ExecuteCommand<TValidationHandler, TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository>(TCommandParameter parameter, string msg)
            }
            return null;
        }

        public virtual ICommandResult<TResult> ExecuteCommand<TValidationHandler, TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository, TModelKey>(TCommandParameter parameter, string msg1, string msg2 = "")
            where TCommandResult : ICommandResult<TResult>
            where TCommandResultFail : IErrorExecuteResultData<TResult>
            where TCommandResultSuccess : IExecuteResultData<TResult>
            where TRepository : IRepository<TResult, TModelKey>
        {
            IResultData<TResult> result = null;
            var state = false;
            try
            {
                parameter.ThrowIfParameterNull<TCommandParameter>();
                // initialize
                InitializeParameter<TRepository, TCommandParameter, TResult, TModelKey>(parameter, typeof(TResult));
                // validate
                if (!Validate<TValidationHandler, TCommandResult, TCommandResultFail>(parameter)) return Result;
                var newData = parameter.GetPropertyValue<TResult>("Data");
                var repository = BootStrapManager.Resolve<TRepository>();
                switch (parameter.CommandType)
                {
                    case CommandTypes.Add:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Inactived);
                        result = repository.Add((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Edit:
                        result = repository.Edit((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Delete:
                        result = repository.Delete((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Remove:
                        result = repository.Remove((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Restore:
                        result = repository.Restore((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;
                    case CommandTypes.RequestApproval:
                        result = repository.RequestForApproval((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Approve:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Approved);
                        result = repository.Approve((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Reject:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Rejected);
                        result = repository.Reject((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.RequestReview:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Rejected);
                        result = repository.RequestReview((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Review:
                        result = repository.Review((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Publish:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Published);
                        result = repository.Publish((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Unpublish:
                        result = repository.Unpublish((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.RequestConfirmation:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Confirmed);
                        result = repository.RequestConfirm((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Confirm:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Confirmed);
                        result = repository.Confirm((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.RequestActivation:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.Confirmed);
                        result = repository.RequestActivation((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Register:
                        //((ICommandParameters<TResult>)parameter).Data.SetPropertyValue("Status",Constant.EntityStatus.NewRegistration);
                        result = repository.Register((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Unregister:
                        result = repository.Unregister((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Activate:
                        ((ICommandParameters<TResult, TModelKey>)parameter).Data.SetPropertyValue("Status", Constant.EntityStatus.Actived);
                        ((ICommandParameters<TResult, TModelKey>)parameter).Data.SetPropertyValue("ActivatedOn", DateTime.Now);
                        ((ICommandParameters<TResult, TModelKey>)parameter).Data.SetPropertyValue("AcivatedBy", BootStrapManager.User);
                        result = repository.Activate((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;

                    case CommandTypes.Deactivate:
                        result = repository.Deactivate((ICommandParameters<TResult, TModelKey>)parameter);//(newData);
                        break;


                    
                }
                var baseExecuteResultData = (BaseExecuteResultData<TResult>)result;
                if (baseExecuteResultData == null) return Result;
                var prevData = baseExecuteResultData.PreviousDataItem;
                if (result.IsResultSuccess())
                {
                    ExecuteOnSuccess(result, parameter, msg1);
                }
                else
                {
                    ExecuteOnFailed(result, parameter, msg2);
                    state = true;
                }
                ConstructResult<TCommandResult, TCommandResultSuccess, TCommandResultFail>(result, newData, prevData, string.Format(msg1, typeof(TResult).Name));

                return Result;
            }
            catch (Exception error)
            {
                /* if executeonfailed not yet executed then execute it */
                if (!state)
                    ExecuteOnFailed(result, parameter, error.Message);
                ExecuteOnException(result, parameter, msg2, error);
                OnExceptionRaised(this, "ExecuteCommand", error);
                throw;
            }
        }

        public virtual ICommandResult<TResult> ExecuteQuery<TCommandResult, TCommandResultSuccess, TCommandResultFail, TRepository, TModelKey>(TCommandParameter parameter, string msg1, string msg2 = "")
            where TCommandResult : ICommandResult<TResult>
            where TCommandResultFail : IErrorFetchResultData<TResult>
            where TCommandResultSuccess : IFetchResultData<TResult>
            where TRepository : IRepository<TResult, TModelKey>
        {
            IResultData<TResult> result = null;
            var state = false;
            try
            {
                parameter.ThrowIfParameterNull<TCommandParameter>();

                // ReSharper disable once UnusedVariable
                //var commandType = parameter.CommandType;
                var repository = BootStrapManager.Resolve<TRepository>();
                var param = (ICommandFetchParameters<TResult, TModelKey>)parameter;
                result = !param.UseStringParameters ? repository.Query(param.Query, param.Page, param.RowCount, param.OrderBy) : repository.Query(param.Page, param.RowCount, param.Predicate, param.OrderBy, param.PredicateParameters);
                if (result.IsResultSuccess())
                {
                    ExecuteOnSuccess(result, parameter, msg1);
                }
                else
                    ExecuteOnFailed(result, parameter, msg2);
                if (result is BaseFetchResultData<TResult> baseFetchResultData)
                    ConstructQueryResult<TCommandResult, TCommandResultSuccess, TCommandResultFail, TModelKey>(result, param,
                        baseFetchResultData.Page,
                        baseFetchResultData.RowCount,
                        baseFetchResultData.Rows,
                        baseFetchResultData.Total, msg1, msg2);
                return Result;
            }
            catch (Exception error)
            {
                /* if executeonfailed not yet executed then execute it */
                if (!state)
                    ExecuteOnFailed(result, parameter, error.Message);
                ExecuteOnException(result, parameter, msg2, error);
                OnExceptionRaised(this, "ExecuteQuery", error);
                throw;
            }
        }

        protected virtual void ExecuteOnFailed(IResultData<TResult> result, TCommandParameter parameter, string msg)
        {
            // override this
        }

        protected virtual void ExecuteOnSuccess(IResultData<TResult> result, TCommandParameter parameter, string msg)
        {
            // override this
        }

        protected virtual void ExecuteOnException(IResultData<TResult> result, TCommandParameter parameter, string msg, Exception error)
        {
            // override this
        }

        protected virtual void InitializeParameter<T1,T2,T3, T4>(TCommandParameter parameter, Type t)
            where T3 : class
        {
            try
            {
                parameter.ThrowIfParameterNull<TCommandParameter>();
                // assign unique id.
                if (string.IsNullOrEmpty((string)GTX_Data_Utility.GetPrimaryKeyIdValue(parameter.Parameters)))
                {
                    parameter.Parameters.SetPropertyValue(GTX_Data_Utility.GetPrimaryKeyIdName(parameter.Parameters), string.Empty.NewId());
                }

                var ctx = BootStrapManager.RequestContext.IsNull() ? System.Guid.NewGuid().ToString() : BootStrapManager.RequestContext.ContextId.ToString();
                var ctx1 = BootStrapManager.RequestContext.IsNull() ? System.Guid.NewGuid().ToString() : BootStrapManager.RequestContext.ActivityContext;
                var usr1 = BootStrapManager.RequestContext.IsNull() ? "" : BootStrapManager.RequestContext.User;

                var tstamp = DateTime.Now;
                var dataModels = parameter.Parameters as Models.IDataModels;
                if (dataModels != null)
                    dataModels.LastTrxId = BootStrapManager.RequestContext.IsNull() ? Guid.NewGuid().ToString() : BootStrapManager.RequestContext.ActivityContext;
                dataModels.SetPropertyValue("ActivityContextId", ctx1);

                if (parameter.CommandType == CommandTypes.Add)
                {
                    if (parameter.Parameters is Models.IDataModels models)
                    {
                        models.CreatedBy = BootStrapManager.User;
                        models.UpdatedBy = BootStrapManager.User;
                        models.CreatedOn = tstamp;
                        models.UpdatedOn = tstamp;
                    }
                    if (!(parameter.Parameters is Models.IRegisterable)) return;
                    (parameter.Parameters as Models.IRegisterable).RegisteredBy = BootStrapManager.User;
                    (parameter.Parameters as Models.IRegisterable).RegisteredOn = tstamp;
                    (parameter.Parameters as Models.IRegisterable).RegistrationToken = Guid.NewGuid().ToString();
                }
                else if (parameter.CommandType != CommandTypes.Fetch)
                {
                    var repos1 = (IRepository<T3,T4>)BootStrapManager.Resolve<T1>();
                    dynamic x = parameter;
                    var items1 = (repos1.Query(1, 1, "Id=@0", "", x.Data.Id).Rows as IEnumerable<T3>).SingleOrDefault();
                    if (parameter.Parameters is Models.IDataModels models)
                    {
                        models.UpdatedBy = BootStrapManager.User;
                        models.UpdatedOn = tstamp;
                    }
                    switch (parameter.CommandType)
                    {
                        case CommandTypes.Publish:
                            ((Models.IPublishable)parameter.Parameters).PublishedBy = BootStrapManager.User;
                            ((Models.IPublishable)parameter.Parameters).PublishedOn = tstamp;
                            break;
                        case CommandTypes.Activate:
                            ((Models.IActivable)parameter.Parameters).ActivatedBy = BootStrapManager.User;
                            ((Models.IActivable)parameter.Parameters).ActivatedOn = tstamp;
                            ((Models.IActivable)parameter.Parameters).ActivationToken = Guid.NewGuid().ToString();
                            break;
                        case CommandTypes.Approve:
                            ((Models.IApprovable)parameter.Parameters).ApprovedBy = BootStrapManager.User;
                            ((Models.IApprovable)parameter.Parameters).ApprovedOn = tstamp;
                            ((Models.IApprovable)parameter.Parameters).ApprovableToken = Guid.NewGuid().ToString();
                            break;
                        case CommandTypes.Confirm:
                            ((Models.IConfirmable)parameter.Parameters).ConfirmedBy = BootStrapManager.User;
                            ((Models.IConfirmable)parameter.Parameters).ConfirmedOn = tstamp;
                            ((Models.IConfirmable)parameter.Parameters).ConfirmationToken = Guid.NewGuid().ToString();
                            break;
                        case CommandTypes.Deactivate:
                            ((Models.IActivable)parameter.Parameters).DeActivatedBy = BootStrapManager.User;
                            ((Models.IActivable)parameter.Parameters).DeActivatedOn = tstamp;
                            break;
                        case CommandTypes.Reject:
                            ((Models.IApprovable)parameter.Parameters).RejectedBy = BootStrapManager.User;
                            ((Models.IApprovable)parameter.Parameters).RejectedOn = tstamp;
                            ((Models.IApprovable)parameter.Parameters).RejectedReason = Guid.NewGuid().ToString();
                            break;
                        case CommandTypes.Unpublish:
                            ((Models.IPublishable)parameter.Parameters).UnpublishedBy = BootStrapManager.User;
                            ((Models.IPublishable)parameter.Parameters).UnpublishedOn = tstamp;
                            break;
                        case CommandTypes.Add:
                            break;
                        case CommandTypes.Edit:
                            break;
                        case CommandTypes.Delete:
                            break;
                        case CommandTypes.Remove:
                            break;
                        case CommandTypes.Restore:
                            break;
                        case CommandTypes.Fetch:
                            break;
                        case CommandTypes.Review:
                            break;
                        case CommandTypes.Register:
                            break;
                        case CommandTypes.Unregister:
                            break;
                        case CommandTypes.Commit:
                            break;
                        case CommandTypes.Login:
                            break;
                        case CommandTypes.Logout:
                            break;
                        case CommandTypes.Download:
                            break;
                        case CommandTypes.Upload:
                            break;
                        case CommandTypes.Unknown:
                            break;
                        case CommandTypes.System:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    ((Models.IDataModels)parameter.Parameters).UpdatedBy = BootStrapManager.User;
                    ((Models.IDataModels)parameter.Parameters).UpdatedOn = tstamp;
                }
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "InitializeParameter", error);
                throw;
            }
            throw new NotImplementedException();
        }

        public event EventHandler BeforeExecute;

        public virtual void OnBeforeExecute()
        {
            if (BeforeExecute.IsNotNull())
                BeforeExecute(this, new EventArgs());
        }

        public event EventHandler Executed;

        public virtual void OnExecuted()
        {
            if (Executed.IsNotNull())
                Executed(this, new EventArgs());
        }

        public virtual ICommandBus CommandBus { get; set; }

       
    }

}
