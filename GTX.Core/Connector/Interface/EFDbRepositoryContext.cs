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
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using GTX.Commands;
using System.ComponentModel.DataAnnotations;
using Omu.ValueInjecter;
using GTX.Models;
using GTX.Bootstrapper;

namespace GTX
{
    public class EFDbRepositoryContext<TDbContext> : BaseRepositoryContext, IEntityFrameworkRepositoryContext
    {
        /**************************************************************************************************************************************/
        public virtual GTXDbContext _DBContext { get; set; }
        /**************************************************************************************************************************************/
        protected virtual DbContextTransaction _DbTransaction { get; set; }
        /**************************************************************************************************************************************/
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public EFDbRepositoryContext(Guid contextId) : base(contextId)
        {

        }
        /**************************************************************************************************************************************/
        /// <summary>
        /// Constructor
        /// </summary>
        public EFDbRepositoryContext()
            : this(Guid.NewGuid())
        {
        }
        /**************************************************************************************************************************************/
        public override void Initialize()
        {
            try
            {
                dynamic x = Activator.CreateInstance<TDbContext>();
                _DBContext = (GTXDbContext)x;
                base.Initialize();
            }
            catch (Exception errors)
            {
                OnExceptionRaised(this, "Initialize", errors);
                throw;
            }
        }
        /**************************************************************************************************************************************/
        public override void InitConnector(string constring = "")
        {

        }
        /**************************************************************************************************************************************/
        public override void StartTransaction()
        {
            _DbTransaction = _DBContext.Database.BeginTransaction();
            TransactionId = GTX_String_Utility.CreateUniqueId;
            InTransaction = true;
            var trans = new AuditTransaction
            {
                TransactionId = TransactionId,
                TransactionDate = DateTime.Now,
                TransactionUser = BootStrapManager.User
            };
           // _DBContext.Set<AuditTransaction>().Add(trans);
        }
        /**************************************************************************************************************************************/
        public override void SaveChanges()
        {
            try
            {
                
                _DBContext.SaveChanges(TransactionId, this);
                if (InTransaction)
                    _DbTransaction?.Commit();
            }
            catch (Exception errors)
            {
                OnExceptionRaised(this, "SaveChanges", errors);
                if (InTransaction)
                    _DbTransaction.Rollback();
                throw;
            }
            InTransaction = false;
        }
        /**************************************************************************************************************************************/
        public override IEnumerable<T> Find<T>(string entityName, Func<T, bool> query)  //where T : class
        {
            var dataset = _DBContext.Set<T>(); // _DBContext.Set(typeof(T)) as System.Data.Entity.IDbSet<T>;
            return dataset.AsNoTracking().Where(query).ToList();
        }
        /**************************************************************************************************************************************/
        public override T FindById<T>(string entityName, object id)
        {
            var dbset = _DBContext.Set(typeof(T));

            return dbset.AsNoTracking().AsQueryable().Where("Id=@0", id).OfType<T>().FirstOrDefault();
        }
        /**************************************************************************************************************************************/
        public override T GetPreviousValues<T>(T item)
        {
            var tp = typeof(T);
            var p = tp.GetProperties().Single(u => u.CustomAttributes.Any(v => v.AttributeType == typeof(KeyAttribute)));
            var result = FindById<T>("", p.GetValue(item));
            return result;
        }
        /**************************************************************************************************************************************/
        protected virtual void CheckIfNotIntransactionCreateIt<T>(T item)
        {
            if (!InTransaction)
                StartTransaction();
        }
        /**************************************************************************************************************************************/
        public enum EnValidCommand
        {
            ValidCommand,
            InvalidCommand,
            NotSupportedCommand
        }
        /**************************************************************************************************************************************/
        public override IResultData<T1> DoModifiedStatus<T1, T2>(string entityName, object id, string status, CommandTypes ct, string token, string notes)
        {
            var currentTime = DateTime.Now;
            var user = BootStrapManager.User;
            var validCommand = EnValidCommand.ValidCommand;
            var dataset = _DBContext.Set<T1>();

            var newEntity = dataset.Find(id);
            if (newEntity == null)
                return GenerateExecuteErrorResult<T1>(GlobalErrorCode.EntityNotFoundException, new EntityNotFoundException(id, "Entity Not Found"));
            CheckIfNotIntransactionCreateIt(newEntity);
            var transaction_record = _DBContext.ChangeTracker.Entries<AuditTransaction>().SingleOrDefault(ent => (ent.Entity as AuditTransaction).TransactionId.Equals(TransactionId));

            var prevEntity = Activator.CreateInstance<T1>();
            prevEntity = (T1)prevEntity.InjectFrom(newEntity);
            (newEntity as IDataModels).Status = status;
            (newEntity as IDataModels).UpdatedBy = user;
            (newEntity as IDataModels).UpdatedOn = currentTime;
            if (newEntity is IJournalEntity)
                (newEntity as IJournalEntity).EntryDate = currentTime;
            switch (ct)
            {
                /******************************************************/
                case CommandTypes.Add:
                    validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Edit:
                    validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Delete:
                    validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Remove:
                    if (newEntity is ISoftDeletes)
                    {
                        (newEntity as ISoftDeletes).DeletedBy = user;
                        (newEntity as ISoftDeletes).DeletedOn = currentTime;
                        (newEntity as ISoftDeletes).DeletionReason = notes;
                        (newEntity as ISoftDeletes).PrevStatus = (prevEntity as DataModels).Status;
                        (transaction_record.Entity as AuditTransaction).CommandTypes = CommandTypes.Remove;
                    }
                    else
                        validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Fetch:
                    validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Restore:
                    if (newEntity is ISoftDeletes)
                    {
                        (newEntity as ISoftDeletes).RestoredBy = user;
                        (newEntity as ISoftDeletes).RestoredOn = currentTime;
                        (newEntity as ISoftDeletes).RestoreNotes = notes;
                        (newEntity as DataModels).Status = (newEntity as ISoftDeletes).PrevStatus;
                        (newEntity as ISoftDeletes).PrevStatus = Constant.EntityStatus.NoStatus;
                        (transaction_record.Entity as AuditTransaction).CommandTypes = CommandTypes.Restore;
                    }
                    else
                        validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Register:
                    if (newEntity is IRegisterable)
                    {
                        (newEntity as IRegisterable).RegisteredBy = user;
                        (newEntity as IRegisterable).RegisteredOn = currentTime;
                        (newEntity as IRegisterable).RegistrationToken = GTX_String_Utility.RngCharacterMask(8);
                    }
                    else
                        validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                case CommandTypes.Unregister:
                    if (newEntity is IRegisterable)
                    {
                        if ((newEntity as IRegisterable).RegistrationToken.Equals(token))
                        {
                            (newEntity as IRegisterable).UnregisteredBy = user;
                            (newEntity as IRegisterable).UnregisteredOn = currentTime;
                        }
                        else
                            return GenerateExecuteErrorResult<T1>(GlobalErrorCode.InvalidToken, new InvalidTokenException(id, "Token does not match"));
                    }
                    else
                        validCommand = EnValidCommand.NotSupportedCommand;
                    break;
                /******************************************************/
                default:
                    throw new ArgumentOutOfRangeException("ct", ct, null);
            }
            switch (validCommand)
            {
                case EnValidCommand.ValidCommand:
                    //dataset.Attach(newEntity);
                    _DBContext.Entry<T1>(newEntity).State = EntityState.Modified;
                    var result = Activator.CreateInstance<T2>();
                    result.PreviousDataItem = prevEntity;
                    result.NewDataItem = newEntity;
                    return result;
                case EnValidCommand.InvalidCommand:
                    return GenerateExecuteErrorResult<T1>(GlobalErrorCode.UnsupportedMediaType, new Exception("Command Types out of Range"));
                case EnValidCommand.NotSupportedCommand:
                    return GenerateExecuteErrorResult<T1>(GlobalErrorCode.UnsupportedMediaType, new Exception("Command Not Supported"));
            }
            return null;
        }
        /**************************************************************************************************************************************/
        private BaseErrorExecuteResultData<T> GenerateExecuteErrorResult<T>(long? errorCode, Exception error) where T : class
        {
            return new BaseErrorExecuteResultData<T>
            {
                ErrorCode = errorCode,
                Exception = error
            };
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Add<T>(string entityName, T newItem)  //where T : class
        {
            var dbSet = _DBContext.Set(typeof(T));
            BaseExecuteResultData<T> result = new BaseExecuteAddResultData<T> { NewDataItem = newItem };
            try
            {
                /* Prepare */
                CheckIfNotIntransactionCreateIt(newItem);
                var transaction_record = _DBContext.ChangeTracker.Entries<AuditTransaction>().SingleOrDefault(ent => (ent.Entity as AuditTransaction).TransactionId.Equals(TransactionId));
                (transaction_record.Entity as AuditTransaction).CommandTypes = CommandTypes.Add;
                if (string.IsNullOrEmpty((newItem as DataModels).Status))
                    (newItem as DataModels).Status = Constant.EntityStatus.Inactived;
               
                result.NewDataItem = newItem;
                result.PreviousDataItem = default(T);
                
                dbSet.Add(newItem);

                return result;
            }
            catch (Exception error)
            {
                var err = new ExecuteCommandException(error.Message, error)
                {
                    NewItem = result.NewDataItem,
                    PreviousItem = result.PreviousDataItem,
                    CommandType = CommandTypes.Add,
                    Parameters = new { EntityName = entityName, NewItem = newItem }
                };
                OnExceptionRaised(this, "Add", err);
                throw;
            }
        }

        public override IResultData<T> Add<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> newItem)
        {
            return Add(entityName, newItem.Data); // base.Add(entityName, newItem);
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Update<T>(string entityName, object id, T item) //where T : class
        {
            try
            {
                /* Prepare */
                CheckIfNotIntransactionCreateIt(item);
                var transaction_record = _DBContext.ChangeTracker.Entries<AuditTransaction>().SingleOrDefault(ent => (ent.Entity as AuditTransaction).TransactionId.Equals(TransactionId));
                (transaction_record.Entity as AuditTransaction).CommandTypes = CommandTypes.Edit;
                var result = new BaseExecuteUpdateResultData<T> { NewDataItem = item };
                var dataset = _DBContext.Set<T>(); // _DBContext.Set(typeof(T)) as System.Data.Entity.IDbSet<T>;
                
                var t = typeof(T);
                var p = t.GetProperties().Single(u => u.CustomAttributes.Any(v => v.AttributeType == typeof(KeyAttribute)));
                var entity = FindById<T>(entityName, p.GetValue(item));
                if (entity == null)
                    return new BaseErrorExecuteResultData<T>
                    {
                        ErrorCode = GlobalErrorCode.EntityNotFoundException,
                        Exception = new EntityNotFoundException(item, "Entity Not Found")
                    };
                dataset.Attach(item);
                _DBContext.Entry<T>(item).State = EntityState.Modified;
                result.NewDataItem = item;
                result.PreviousDataItem = entity;
                return result;
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Update", error);
                throw;
            }
        }

        public override IResultData<T> Update<T, TModelKey>(string entityName, object id, ICommandParameters<T, TModelKey> item)
        {
            return Update(entityName, id, item.Data);
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Delete<T>(string entityName, object id) //where T : class
        {
            try
            {
                var result = new BaseExecuteDeleteResultData<T>();
                var collection = _DBContext.Set<T>();
                var entity = FindById<T>(entityName, id);
                if (entity != null)
                {
                    CheckIfNotIntransactionCreateIt(entity);
                    var transaction_record = _DBContext.ChangeTracker.Entries<AuditTransaction>().SingleOrDefault(ent => (ent.Entity as AuditTransaction).TransactionId.Equals(TransactionId));
                    (transaction_record.Entity as AuditTransaction).CommandTypes = CommandTypes.Delete;

                    result.NewDataItem = entity;
                    result.PreviousDataItem = entity;
                    collection.Remove(collection.Find(id));
                    return result;
                }
                else
                {
                    return new BaseErrorExecuteResultData<T>
                    {
                        ErrorCode = GlobalErrorCode.EntityNotFoundException,
                        Exception = new EntityNotFoundException(null, string.Format("Entity {0} Not Found", id))
                    };
                }
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Delete", error);
                throw;
            }

        }

        public override IResultData<T> Delete<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Delete<T>(entityName, item.Id);
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Remove<T>(string entityName, object id)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Deleted, CommandTypes.Remove, string.Empty, string.Empty);
        }

        public override IResultData<T> Remove<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Remove<T>(entityName, item.Id);
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Restore<T>(string entityName, object id)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.NoStatus, CommandTypes.Restore, string.Empty, string.Empty);
        }

        public override IResultData<T> Restore<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Restore<T>(entityName, item.Id);
        }
        /**************************************************************************************************************************************/

        public override IResultData<T> RequestApproval<T>(string entityName, object id, string notes)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.WaitingApproval, CommandTypes.RequestApproval,string.Empty, notes);
        }

        public override IResultData<T> RequestApproval<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return RequestApproval<T>(entityName, item.Id, item.Notes);
        }

        public override IResultData<T> Approve<T>(string entityName, object id, string token, string notes)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Approved, CommandTypes.Approve, token, notes);
        }

        public override IResultData<T> Approve<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Approve<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public override IResultData<T> Reject<T>(string entityName, object id, string token, string reason)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Rejected, CommandTypes.Reject, token, reason);
        }

        public override IResultData<T> Reject<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Reject<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public override IResultData<T> RequestReview<T>(string entityName, object id, string notes)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.WaitingAReview, CommandTypes.RequestReview, string.Empty, notes);
        }

        public override IResultData<T> RequestReview<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return RequestReview<T>(entityName, item.Id, item.Notes);
        }

        public override IResultData<T> Review<T>(string entityName, object id, string token, string review)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Reviewed, CommandTypes.Review, token, review);
        }

        public override IResultData<T> Review<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Review<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public override IResultData<T> Publish<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Published, CommandTypes.Publish, token, string.Empty);
        }

        public override IResultData<T> Publish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Publish<T>(entityName, item.Id, item.Token);
        }

        public override IResultData<T> Unpublish<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Actived, CommandTypes.Unpublish, token, string.Empty);
        }

        public override IResultData<T> Unpublish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Unpublish<T>(entityName, item.Id, item.Token);
        }

        public override IResultData<T> RequestConfirm<T>(string entityName, object id, string notes)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.WaitingConfirmation, CommandTypes.RequestConfirmation, string.Empty, notes);
        }

        public override IResultData<T> RequestConfirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return RequestConfirm<T>(entityName, item.Id, item.Notes);
        }

        public override IResultData<T> Confirm<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Confirmed, CommandTypes.Confirm, token, string.Empty);
        }

        public override IResultData<T> Confirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Confirm<T>(entityName, item.Id,item.Token);
        }

        public override IResultData<T> RequestActivation<T>(string entityName, object id, string notes)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.WaitingActivation, CommandTypes.RequestActivation, string.Empty, notes);
        }

        public override IResultData<T> RequestActivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return RequestActivation<T>(entityName, item.Id, item.Notes);
        }

        public override IResultData<T> Activate<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Actived, CommandTypes.Activate, token, string.Empty);
        }

        public override IResultData<T> Activate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Activate<T>(entityName, item.Id, item.Token);
        }

        public override IResultData<T> RequestDeactivation<T>(string entityName, object id, string notes)
        { 
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.WaitingDeactivation, CommandTypes.RequestDeactivation, string.Empty, notes);
        }

        public override IResultData<T> RequestDeactivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return RequestDeactivation<T>(entityName, item.Id, item.Notes);
        }

        public override IResultData<T> DeActivate<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.Inactived, CommandTypes.Deactivate, token, string.Empty);
        }

        public override IResultData<T> DeActivate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return DeActivate<T>(entityName, item.Id, item.Token);
        }

        public override IResultData<T> Register<T>(string entityName, T newItem)
        {
            Add(entityName, newItem);
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, GTX_Data_Utility.GetPrimaryKeyIdValue(newItem), Constant.EntityStatus.NewRegistration, CommandTypes.Register, string.Empty, string.Empty);
        }

        public override IResultData<T> Register<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Register<T>(entityName, item.Data);
        }


        public override IResultData<T> Unregister<T>(string entityName, object id, string token)
        {
            return DoModifiedStatus<T, BaseExecuteResultData<T>>(entityName, id, Constant.EntityStatus.NewRegistration, CommandTypes.Register, token, string.Empty);
        }

        public override IResultData<T> Unregister<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item)
        {
            return Unregister<T>(entityName, item.Id, item.Token);
        }

        /**************************************************************************************************************************************/
        /// <summary>
        /// Queries the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="rowcount">The rowcount.</param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override IResultData<T> Query<T>(string entityName,
            Func<T, bool> query,
            int page = 1,
            int rowcount = 0, string orderby = "") //where T : class
        {
            var result = new BaseFetchResultData<T> { Page = page, RowCount = rowcount, Total = 0, TotalPages = 0 };

            var dbset = _DBContext.Set<T>();
            var qry = dbset.AsQueryable().Where(query);
            if (!string.IsNullOrEmpty(orderby))
                qry = qry.AsQueryable().OrderBy(orderby);
            var jumlahData = qry.Count();
            var cursor = qry;
            double totalPages;
            if (rowcount > 0)
            {
                result.Rows = cursor.Skip((page - 1) * rowcount).Take(rowcount).ToList();
                totalPages = (double)jumlahData / rowcount;
            }
            else
            {
                result.Rows = cursor.ToList();
                totalPages = (double)result.Rows.Count() / rowcount;
            }
            result.Total = jumlahData;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            return result;
        }
        /**************************************************************************************************************************************/
        public override IResultData<T> Query<T>(string entityName,
            int page,
            int rowcount,
            string predicate, string orderby,
            params object[] param) //where T : class
        {
            var result = new BaseFetchResultData<T> { Page = page, RowCount = rowcount, Total = 0, TotalPages = 0 };

            var dbset = _DBContext.Set<T>();
            var qry = dbset.AsQueryable().Where(predicate, param).AsQueryable();
            if (!string.IsNullOrEmpty(orderby))
                qry = qry.AsQueryable().OrderBy(orderby);
            var jumlahData = qry.Count();
            IQueryable<T> cursor = qry;
            double totalPages;
            if (rowcount > 0)
            {
                result.Rows = cursor.Skip<T>((page - 1) * rowcount).Take<T>(rowcount).ToList<T>();
                totalPages = (double)jumlahData / rowcount;
            }
            else
            {
                result.Rows = cursor.ToList();
                totalPages = (double)result.Rows.Count() / rowcount;
            }
            result.Total = jumlahData;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            return result;

        }
        /**************************************************************************************************************************************/
        public override IResultData<T1> Query<T, T1>(string entityName, Func<T, bool> query, Func<T, T1> selector, int page = 0, int rowcount = 0, string orderby = "")
        {
            var result = new BaseFetchResultData<T1> { Page = page, RowCount = rowcount, Total = 0, TotalPages = 0 };
            var dbset = _DBContext.Set<T>();
            var qry = dbset.AsQueryable().Where(query);
            if (!string.IsNullOrEmpty(orderby))
                qry = qry.AsQueryable().OrderBy(orderby);
            var jumlahData = qry.Count();
            var cursor = qry;
            double totalPages;
            if (rowcount > 0)
            {
                result.Rows = cursor.Skip((page - 1) * rowcount).Take(rowcount).Select<T, T1>(selector).ToList();
                totalPages = (double)jumlahData / rowcount;
            }
            else
            {
                result.Rows = cursor.Select<T, T1>(selector).ToList();
                totalPages = (double)result.Rows.Count() / rowcount;
            }
            result.Total = jumlahData;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            return result;

        }
        /**************************************************************************************************************************************/
        public override IResultData<T1> Query<T, T1>(string entityName, int page, int rowcount, string predicate, string orderby, Func<T, T1> selector, params object[] param)
        {
            var result = new BaseFetchResultData<T1>();
            var dbset = _DBContext.Set<T>();
            var qry = dbset.AsQueryable().Where(predicate, param);
            if (!string.IsNullOrEmpty(orderby))
                qry = qry.AsQueryable().OrderBy(orderby);
            var jumlahData = qry.Count();
            var cursor = qry;
            double totalPages;
            if (rowcount > 0)
            {
                result.Rows = cursor.Skip<T>((page - 1) * rowcount).Take<T>(rowcount).Select<T, T1>(selector).ToList();
                totalPages = (double)jumlahData / rowcount;
            }
            else
            {
                result.Rows = cursor.Select<T, T1>(selector).ToList();
                totalPages = (double)result.Rows.Count() / rowcount;
            }
            result.Total = jumlahData;
            result.TotalPages = (int)Math.Ceiling(totalPages);
            return result;
        }
        /*********************************************S*****************************************************************************************/

        public override IResultData<T> ExecuteCommand<T, T2>(string commandName, T2 item)
        {
            var commandNameStr = commandName + " {0}";
            var sqlparams = item.ToSqlParamsList();
            var sqlparameters = sqlparams.Select(u => string.Format("@{0}", u.ParameterName)).ToArray();
            commandNameStr = string.Format(commandNameStr, string.Join(",", sqlparameters));
            return base.ExecuteCommand<T, T2>(commandNameStr, item);
        }

        public override IResultData<T> ExecuteQuery<T, T2>(string commandName, T2 item)
        {
            var commandNameStr = commandName + " {0}";
            var sqlparams = item.ToSqlParamsList();
            var sqlparameters = sqlparams.Select(u => string.Format("@{0}", u.ParameterName)).ToArray();
            commandNameStr = string.Format(commandNameStr, string.Join(",", sqlparameters));
            var arr = item.ToSqlParamsArray();
            var hasil = (_DBContext.Database.SqlQuery<T>(commandNameStr, arr)).ToList();
            var result = new BaseFetchResultData<T>
            {
                Page = 1,
                RowCount = hasil.Count,
                Rows = hasil,
                Total = hasil.Count,
                TotalPages = 1
            };

            return result;
        }

        public IResultData<T> ExecuteQuerySQL<T>(string commandName, params System.Data.SqlClient.SqlParameter[] item) where T : class
        {
            var hasil = (_DBContext.Database.SqlQuery<T>(commandName, item)).ToList();
            var result = new BaseFetchResultData<T>
            {
                Page = 1,
                RowCount = hasil.Count,
                Rows = hasil,
                Total = hasil.Count,
                TotalPages = 1
            };

            return result;
        }
    }

}

