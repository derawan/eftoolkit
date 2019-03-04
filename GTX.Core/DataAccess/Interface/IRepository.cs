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

namespace GTX
{

    public interface IRepository<TModel, TModelKey> : IContext, IDisposableObjects where TModel : class
    {
        string TableName { get; set; }
        IRepositoryContext Context { get; set; }

        IResultData<TModel> Add(TModel newItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Add(ICommandParameters<TModel, TModelKey> newItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Edit(TModel updatedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Edit(ICommandParameters<TModel, TModelKey> updatedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Delete(TModel deletedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Delete(ICommandParameters<TModel, TModelKey> deletedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Remove(TModel removedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Remove(ICommandParameters<TModel, TModelKey> removedItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Restore(TModel restoredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Restore(ICommandParameters<TModel, TModelKey> restoredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);


        IResultData<TModel> RequestForApproval(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> RequestForApproval(ICommandParameters<TModel, TModelKey> approvalItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Approve(object id, string token, string notes, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Approve(ICommandParameters<TModel, TModelKey> approvalItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Reject(object id, string token, string reason, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> Reject(ICommandParameters<TModel, TModelKey> rejectItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> RequestReview(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> RequestReview(ICommandParameters<TModel, TModelKey> reviewItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Review(object id, string token, string review, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Review(ICommandParameters<TModel, TModelKey> reviewItem, Func<object, string, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Publish(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Publish(ICommandParameters<TModel, TModelKey> publishItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Unpublish(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Unpublish(ICommandParameters<TModel, TModelKey> unpublishItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);  

        IResultData<TModel> RequestConfirm(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> RequestConfirm(ICommandParameters<TModel, TModelKey> confirmItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Confirm(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> Confirm(ICommandParameters<TModel, TModelKey> confirmItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> RequestActivation(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> RequestActivation(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Activate(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Activate(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> RequestDeactivation(object id, string notes, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> RequestDeactivation(ICommandParameters<TModel, TModelKey> activationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Deactivate(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);
         
        IResultData<TModel> Deactivate(ICommandParameters<TModel, TModelKey> deactivationItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Register(TModel registeredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Register(ICommandParameters<TModel, TModelKey> registeredItem, Func<TModel, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Unregister(object id, string token, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Unregister(ICommandParameters<TModel, TModelKey> unregisteredItem, Func<object, string, IResultData<TModel>, IRepository<TModel, TModelKey>, IResultData<TModel>> action = null);

        IResultData<TModel> Query(
            Func<TModel, bool> query, 
            int                page, 
            int                rowcount, 
            string             orderby = "");

        IResultData<TModel> Query(
            int             page, 
            int             rowcount, 
            string          predicate, 
            string          orderby, 
            params object[] param);

        IResultData<T2> Query<T1, T2>(
            Func<T1, bool>   query, 
            Func<T1, T2>     selector, 
            int              page = 0, 
            int              rowcount = 0, 
            string           orderby = "")
            where T2 : class
            where T1 : class;

        IResultData<T2> Query<T1, T2>(
            int              page, 
            int              rowcount, 
            string           predicate, 
            string           orderby, 
            Func<T1, T2>     selector, 
            params object[]  param)
            where T2 : class
            where T1 : class;

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IResultData<TModel> Query(Func<TModel, bool> query);

        /// <summary>
        /// Finds the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<T> Find<T>(
            string entityName, 
            Func<T, bool> query) where T : class;
    }
}
