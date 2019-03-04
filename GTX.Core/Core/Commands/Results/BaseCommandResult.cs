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
    public class BaseCommandResult<TModel> : BaseResult, ICommandResult<TModel> where TModel : class
    {
        public BaseCommandResult()
            : this(CommandTypes.Fetch, MessageType.Default)
        {
        }

        public BaseCommandResult(
            CommandTypes commandType, 
            MessageType messageType, 
            Exception error = null, 
            long errorCode = GlobalErrorCode.DefaultSystemError) :
            base(commandType, messageType, error, errorCode)
        {

            if (error != null)
            {
                var tipe = typeof(TModel);
                switch (commandType)
                {
                    case CommandTypes.Add:
                    case CommandTypes.Edit:
                    case CommandTypes.Delete:
                    case CommandTypes.Remove:
                    case CommandTypes.Restore:

                    case CommandTypes.Approve:
                    case CommandTypes.Reject:
                    case CommandTypes.Review:

                    case CommandTypes.Activate:
                    case CommandTypes.Deactivate:

                    case CommandTypes.Register:
                    case CommandTypes.Unregister:

                    case CommandTypes.Publish:
                    case CommandTypes.Unpublish:

                    case CommandTypes.Commit:

                    case CommandTypes.Login:
                    case CommandTypes.Logout:

                    case CommandTypes.Download:
                    case CommandTypes.Upload:

                    case CommandTypes.Unknown:

                    case CommandTypes.System:

                    case CommandTypes.Confirm:
                        tipe = typeof(BaseErrorExecuteResultData<TModel>);
                        break;

                    case CommandTypes.Fetch:
                        tipe = typeof(BaseErrorFetchResultData<TModel>);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("commandType", commandType,GlobalErrorCode.InvalidCommandTypeMessage);
                }
                 
                Result = (BaseErrorResultData<TModel>)Activator.CreateInstance(tipe, error,errorCode);
                CommandResultType = CommandResultTypes.Error;
                
            }
            else
            {
                Type tipe = null; CommandResultTypes crType = CommandResultTypes.Executing;
                switch (commandType)
                {
                    case CommandTypes.Add:
                        tipe = typeof(BaseExecuteAddResultData<TModel>);
                        crType = CommandResultTypes.Inserted;
                        break;
                    case CommandTypes.Edit:
                        tipe = typeof(BaseExecuteUpdateResultData<TModel>);
                        crType = CommandResultTypes.Updated;
                        break;
                    case CommandTypes.Delete:
                        tipe = typeof(BaseExecuteDeleteResultData<TModel>);
                        crType = CommandResultTypes.Deleted;
                        break;
                    case CommandTypes.Remove:
                        tipe = typeof(BaseExecuteRemoveResultData<TModel>);
                        crType = CommandResultTypes.Removed;
                        break;
                    case CommandTypes.Restore:
                        tipe = typeof(BaseExecuteRestoreResultData<TModel>);
                        crType = CommandResultTypes.Restored;
                        break;

                    case CommandTypes.Approve:
                        tipe = typeof(BaseExecuteApproveResultData<TModel>);
                        crType = CommandResultTypes.Approved;
                        break;
                    case CommandTypes.Reject:
                        tipe = typeof(BaseExecuteRejectResultData<TModel>);
                        crType = CommandResultTypes.Rejected;
                        break;
                    case CommandTypes.Review:
                        tipe = typeof(BaseExecuteReviewResultData<TModel>);
                        crType = CommandResultTypes.Reviewed;
                        break;

                    case CommandTypes.Activate:
                        tipe = typeof(BaseExecuteActivateResultData<TModel>);
                        crType = CommandResultTypes.Activated;
                        break;
                    case CommandTypes.Deactivate:
                        tipe = typeof(BaseExecuteDeactivateResultData<TModel>);
                        crType = CommandResultTypes.Deactivated;
                        break;

                    case CommandTypes.Register:
                        tipe = typeof(BaseExecuteRegisterResultData<TModel>);
                        crType = CommandResultTypes.Registered;
                        break;
                    case CommandTypes.Unregister:
                        tipe = typeof(BaseExecuteUnregisterResultData<TModel>);
                        crType = CommandResultTypes.Unregistered;
                        break;

                    case CommandTypes.Publish:
                        tipe = typeof(BaseExecutePublishResultData<TModel>);
                        crType = CommandResultTypes.Published;
                        break;
                    case CommandTypes.Unpublish:
                        tipe = typeof(BaseExecuteUnpublishResultData<TModel>);
                        crType = CommandResultTypes.Unpublished;
                        break;

                    case CommandTypes.Confirm:
                        tipe = typeof(BaseExecuteConfirmResultData<TModel>);
                        crType = CommandResultTypes.Confirmed;
                        break;

                    case CommandTypes.Fetch:
                        tipe = typeof(BaseFetchResultData<TModel>);
                        crType = CommandResultTypes.Fetched;
                        break;
                    case CommandTypes.Commit:
                        crType = CommandResultTypes.Commited;
                        break;
                    case CommandTypes.Login:
                        crType = CommandResultTypes.Login;
                        break;
                    case CommandTypes.Logout:
                        crType = CommandResultTypes.Logout;
                        break;
                    case CommandTypes.Download:
                        crType = CommandResultTypes.Downloaded;
                        break;
                    case CommandTypes.Upload:
                        crType = CommandResultTypes.Uploaded;
                        break;
                    case CommandTypes.Unknown:
                        crType = CommandResultTypes.Executed;
                        break;
                    case CommandTypes.System:
                        crType = CommandResultTypes.Executed;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("commandType", commandType, GlobalErrorCode.InvalidCommandTypeMessage);
                }
                crType = CommandResultTypes.Executing;
                crType = SetCommandResultType(commandType);

                if (tipe != null) Result = (BaseResultData<TModel>)Activator.CreateInstance(tipe);

                CommandResultType = crType;
            }
            CommandType = commandType;
            
        }

        public virtual CommandResultTypes SetCommandResultType(CommandTypes cmdType)
        {
            switch (cmdType)
            {
                case CommandTypes.Add:
                    CommandResultType = CommandResultTypes.Inserted;
                    break;
                case CommandTypes.Edit:
                    CommandResultType = CommandResultTypes.Updated;
                    break;
                case CommandTypes.Delete:
                    CommandResultType = CommandResultTypes.Deleted;
                    break;
                case CommandTypes.Remove:
                    CommandResultType = CommandResultTypes.Removed;
                    break;
                case CommandTypes.Restore:
                    CommandResultType = CommandResultTypes.Restored;
                    break;

                case CommandTypes.Approve:
                    CommandResultType = CommandResultTypes.Approved;
                    break;
                case CommandTypes.Reject:
                    CommandResultType = CommandResultTypes.Rejected;
                    break;
                case CommandTypes.Review:
                    CommandResultType = CommandResultTypes.Reviewed;
                    break;

                case CommandTypes.Activate:
                    CommandResultType = CommandResultTypes.Activated;
                    break;
                case CommandTypes.Deactivate:
                    CommandResultType = CommandResultTypes.Deactivated;
                    break;

                case CommandTypes.Register:
                    CommandResultType = CommandResultTypes.Registered;
                    break;
                case CommandTypes.Unregister:
                    CommandResultType = CommandResultTypes.Unregistered;
                    break;

                case CommandTypes.Publish:
                    CommandResultType = CommandResultTypes.Published;
                    break;
                case CommandTypes.Unpublish:
                    CommandResultType = CommandResultTypes.Unpublished;
                    break;

                case CommandTypes.Confirm:
                    CommandResultType = CommandResultTypes.Confirmed;
                    break;

                case CommandTypes.Fetch:
                    CommandResultType = CommandResultTypes.Fetched;
                    break;
                case CommandTypes.Commit:
                    CommandResultType = CommandResultTypes.Commited;
                    break;
                case CommandTypes.Login:
                    CommandResultType = CommandResultTypes.Login;
                    break;
                case CommandTypes.Logout:
                    CommandResultType = CommandResultTypes.Logout;
                    break;
                case CommandTypes.Download:
                    CommandResultType = CommandResultTypes.Downloaded;
                    break;
                case CommandTypes.Upload:
                    CommandResultType = CommandResultTypes.Uploaded;
                    break;
                case CommandTypes.Unknown:
                    CommandResultType = CommandResultTypes.Executed;
                    break;
                case CommandTypes.System:
                    CommandResultType = CommandResultTypes.Executed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("commandType", cmdType, GlobalErrorCode.InvalidCommandTypeMessage);
            }
            return CommandResultType;


        }

        public virtual IResultData<TModel> Result
        {
            get;
            set;
        }
    }




}
