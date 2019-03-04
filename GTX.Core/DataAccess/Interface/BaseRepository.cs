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
using GTX.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GTX
{
    public class BaseRepository<TModel, TModelKey> : BaseContext, IRepository<TModel, TModelKey> where TModel : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseRepository(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseRepository()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TModel}"/> class.
        /// </summary>
        /// <param name="repositoryContext">The repository context.</param>
        public BaseRepository(IRepositoryContext repositoryContext)
            : this()
        {
            Context = repositoryContext;
        }


        private IRepositoryContext _context;

        public IRepositoryContext Context
        {
            get
            {
                if (!_context.IsNull())
                    return _context;
                throw new NullReferenceException(string.Format("{1}({0}) is null", typeof(IRepositoryContext),
                    "Context"));
            }
            set
            {
                _context = value;
            }
        }

        public string TableName { get; set; }

        public string GetTableName
        {
            get
            {
                var obj = typeof(TModel).GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), true).SingleOrDefault();
                if (obj == null)
                    return (typeof(TModel)).Name;
                else
                    return (obj as System.ComponentModel.DataAnnotations.Schema.TableAttribute).Name;
            }
        }

        public string RetrieveTableName<T1>()
        {
            var obj = typeof(T1).GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), true).SingleOrDefault();
            if (obj == null)
                return (typeof(T1)).Name;
            else
                return (obj as System.ComponentModel.DataAnnotations.Schema.TableAttribute).Name;
        }

        public virtual IResultData<TModel> Add(TModel newItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Add<TModel>(GetTableName, newItem);
            if (action.IsNotNull())
            {
                result = action(newItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Add(ICommandParameters<TModel, TModelKey> newItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Add<TModel,TModelKey>(GetTableName, newItem);
            if (action.IsNotNull())
            {
                result = action(newItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Edit(TModel updatedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Update<TModel>(GetTableName, GTX_Data_Utility.GetPrimaryKeyIdValue(updatedItem), updatedItem);
            if (action.IsNotNull())
            {
                result = action(updatedItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Edit(ICommandParameters<TModel, TModelKey> updatedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Update<TModel, TModelKey>(GetTableName, updatedItem.Id, updatedItem);
            if (action.IsNotNull())
            {
                result = action(updatedItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Delete(TModel deletedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (deletedItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Delete<TModel>(GetTableName, GTX_Data_Utility.GetPrimaryKeyIdValue(deletedItem));
            if (action.IsNotNull())
            {
                result = action(deletedItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Delete(ICommandParameters<TModel, TModelKey> deletedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (deletedItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Delete<TModel, TModelKey>(GetTableName, deletedItem);
            if (action.IsNotNull())
            {
                result = action(deletedItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Remove(TModel removedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (removedItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Remove<TModel>(GetTableName, GTX_Data_Utility.GetPrimaryKeyIdValue(removedItem));
            if (action.IsNotNull())
            {
                result = action(removedItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Remove(ICommandParameters<TModel, TModelKey> removedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (removedItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Remove<TModel>(GetTableName, removedItem.Id);
            if (action.IsNotNull())
            {
                result = action(removedItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Restore(TModel restoredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (restoredItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Restore<TModel>(GetTableName, GTX_Data_Utility.GetPrimaryKeyIdValue(restoredItem));
            if (action.IsNotNull())
            {
                result = action(restoredItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Restore(ICommandParameters<TModel, TModelKey> restoredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (restoredItem == null) return null;
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Restore<TModel>(GetTableName, restoredItem.Id);
            if (action.IsNotNull())
            {
                result = action(restoredItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestForApproval(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.RequestApproval<TModel>(GetTableName,id, notes);
            if (action.IsNotNull())
            {
                result = action(id, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestForApproval(ICommandParameters<TModel, TModelKey> approvalItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return RequestForApproval(approvalItem.Id, approvalItem.Notes, action);
        }


        public virtual IResultData<TModel> Approve(object id, string token, string notes, Func<object, string,string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Approve<TModel>(GetTableName, id, token, notes);
            if (action.IsNotNull())
            {
                result = action(id, token, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Approve(ICommandParameters<TModel, TModelKey> approvalItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Approve(approvalItem.Id, approvalItem.Token, approvalItem.Notes, action);
        }


        public virtual IResultData<TModel> Reject(object id, string token, string reason, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Reject<TModel>(GetTableName, id, token, reason);
            if (action.IsNotNull())
            {
                result = action(id, token, reason, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Reject(ICommandParameters<TModel, TModelKey> rejectItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Reject(rejectItem.Id, rejectItem.Token, rejectItem.Notes, action);
        }

        public virtual IResultData<TModel> RequestReview(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.RequestReview<TModel>(GetTableName, id, notes);
            if (action.IsNotNull())
            {
                result = action(id, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestReview(ICommandParameters<TModel, TModelKey> reviewItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return RequestReview(reviewItem.Id, reviewItem.Notes, action);
        }

        public virtual IResultData<TModel> Review(object id, string token, string review, Func<object, string, string , IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Review<TModel>(GetTableName, id, token, review);
            if (action.IsNotNull())
            {
                result = action(id, token, review, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Review(ICommandParameters<TModel, TModelKey> reviewItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Review(reviewItem.Id, reviewItem.Token, reviewItem.Notes);
        }

        public virtual IResultData<TModel> Publish(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Publish<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Publish(ICommandParameters<TModel, TModelKey> publishItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Publish(publishItem.Id, publishItem.Token, action);
        }

        public virtual IResultData<TModel> Unpublish(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Unpublish<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Unpublish(ICommandParameters<TModel, TModelKey> unpublishItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Unpublish(unpublishItem.Id, unpublishItem.Token, action);
        }

        public virtual IResultData<TModel> RequestConfirm(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.RequestConfirm<TModel>(GetTableName, id, notes);
            if (action.IsNotNull())
            {
                result = action(id, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestConfirm(ICommandParameters<TModel, TModelKey> confirmItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return RequestConfirm(confirmItem.Id, confirmItem.Notes, action);
        }

        public virtual IResultData<TModel> Confirm(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Unpublish<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Confirm(ICommandParameters<TModel, TModelKey> confirmItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Confirm(confirmItem.Id, confirmItem.Token, action);
        }

        public virtual IResultData<TModel> RequestActivation(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.RequestActivation<TModel>(GetTableName, id, notes);
            if (action.IsNotNull())
            {
                result = action(id, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestActivation(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return RequestActivation(activationItem.Id, activationItem.Notes);
        }

        public virtual IResultData<TModel> Activate(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Activate<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Activate(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Activate(activationItem.Id, activationItem.Token, action);
        }

        public virtual IResultData<TModel> RequestDeactivation(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.RequestDeactivation<TModel>(GetTableName, id, notes);
            if (action.IsNotNull())
            {
                result = action(id, notes, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> RequestDeactivation(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        { 
            return RequestDeactivation(activationItem.Id, activationItem.Notes);
        } 

        public IResultData<TModel> Deactivate(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.DeActivate<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Deactivate(ICommandParameters<TModel, TModelKey> deactivationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Deactivate(deactivationItem.Id, deactivationItem.Token, action); 
        }

        public virtual IResultData<TModel> Register(TModel registeredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Register<TModel>(GetTableName, registeredItem);
            if (action.IsNotNull())
            {
                result = action(registeredItem, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Register(ICommandParameters<TModel, TModelKey> registeredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if(!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Register<TModel, TModelKey>(GetTableName, registeredItem);
            if (action.IsNotNull())
            {
                result = action(registeredItem.Data, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Unregister(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            if (!Context.InTransaction)
                Context.StartTransaction();
            var result = Context.Unregister<TModel>(GetTableName, id, token);
            if (action.IsNotNull())
            {
                result = action(id, token, result, this);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            return result;
        }

        public virtual IResultData<TModel> Unregister(ICommandParameters<TModel, TModelKey> unregisteredItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null)
        {
            return Unregister(unregisteredItem.Id, unregisteredItem.Token, action);
        }

        public virtual IResultData<TModel> Query(Func<TModel, bool> query, int page, int rowcount, string orderby = "")
        {
            return query == null ? null : Context.Query(GetTableName, query, page, rowcount, orderby);
        }

        public virtual IResultData<TModel> Query(int page, int rowcount, string predicate, string orderby, params object[] param)
        {
            return Context.Query<TModel>(GetTableName, page, rowcount, predicate, orderby, param);
        }

        public virtual IResultData<T2> Query<T1, T2>(Func<T1, bool> query, Func<T1, T2> selector, int page = 0, int rowcount = 0, string orderby = "")
            where T2 : class
            where T1 : class
        {
            return query == null ? null : Context.Query<T1, T2>(RetrieveTableName<T1>(), query, selector, page, rowcount, orderby);
        }

        public virtual IResultData<T2> Query<T1, T2>(int page, int rowcount, string predicate, string orderby, Func<T1, T2> selector, params object[] param)
            where T2 : class
            where T1 : class
        {
            var name = RetrieveTableName<T1>();
            return Context.Query<T1, T2>(name, page, rowcount, predicate, orderby, selector, param);
        }


        public virtual IEnumerable<T> Find<T>(string entityName, Func<T, bool> query) where T : class
        {
            return Context.Find(entityName, query);
        }

        public virtual IResultData<TModel> Query(Func<TModel, bool> query)
        {
            return Query(query);
        }


        


        

        
    }
}
