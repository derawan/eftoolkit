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
using GTX.Models;

namespace GTX
{
    public abstract class BaseRepositoryContext : BaseContext, IRepositoryContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseRepositoryContext(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseRepositoryContext()
            : this(Guid.NewGuid())
        {
        }

        public virtual void InitConnector(string constring = "")
        {
            
        }

        public virtual bool InTransaction { get; protected set; }

        public virtual string TransactionId { get; set; }

        public virtual void StartTransaction()
        {
            InTransaction = true;
        }

        public virtual void SaveChanges()
        {
            InTransaction = false;
        }

        public virtual T FindById<T>(string entityName, object id)
        {
            return default(T);
        }

        public virtual IEnumerable<T> Find<T>(string entityName, Func<T, bool> query) where T : class
        {
            return default(IEnumerable<T>);
        }

        public virtual T GetPreviousValues<T>(T item) where T : class
        {
            return default(T);
        }

        public virtual IResultData<T> Add<T>(string entityName, T newItem) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Add<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> newItem)
            where T : class 
        {
            return Add(entityName, newItem.Data);
        }


        public virtual IResultData<T> Update<T>(string entityName, object id, T item) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Update<T, TModelKey>(string entityName, object id, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Update(entityName, id, item.Data);
        }


        public virtual IResultData<T> Delete<T>(string entityName, object id) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Delete<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Delete<T>(entityName, item.Id);
        }

        public virtual IResultData<T> Remove<T>(string entityName, object id) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Remove<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Remove<T>(entityName, item.Id);
        }

        public virtual IResultData<T> Restore<T>(string entityName, object id) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Restore<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Restore<T>(entityName, item.Id);
        }

        public virtual IResultData<T> RequestApproval<T>(string entityName, object id, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> RequestApproval<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return RequestApproval<T>(entityName, item.Id, item.Notes);
        }


        public virtual IResultData<T> Approve<T>(string entityName, object id, string token, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Approve<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Approve<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public virtual IResultData<T> Reject<T>(string entityName, object id, string token, string reason) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Reject<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Reject<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public virtual IResultData<T> RequestReview<T>(string entityName, object id, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> RequestReview<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return RequestReview<T>(entityName, item.Id, item.Notes);
        }

        public virtual IResultData<T> Review<T>(string entityName, object id, string token, string review) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Review<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        { 
            return Review<T>(entityName, item.Id, item.Token, item.Notes);
        }

        public virtual IResultData<T> Publish<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Publish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Publish<T>(entityName, item.Id, item.Token);
        }

        public virtual IResultData<T> Unpublish<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Unpublish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Unpublish<T>(entityName, item.Id, item.Token);
        }

        public virtual IResultData<T> RequestConfirm<T>(string entityName, object id, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> RequestConfirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return RequestConfirm<T>(entityName, item.Id, item.Notes);
        }

        public virtual IResultData<T> Confirm<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Confirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Confirm<T>(entityName, item.Id, item.Token);
        }

        public virtual IResultData<T> RequestActivation<T>(string entityName, object id, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> RequestActivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return RequestActivation<T>(entityName, item.Id, item.Notes);
        }


        public virtual IResultData<T> Activate<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Activate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Activate<T>(entityName, item.Id, item.Token);
        }

        public virtual IResultData<T> RequestDeactivation<T>(string entityName, object id, string notes) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> RequestDeactivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        { 
            return RequestDeactivation<T>(entityName, item.Id, item.Notes); 
        }


        public virtual IResultData<T> DeActivate<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> DeActivate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return DeActivate<T>(entityName, item.Id, item.Token);
        }


        public virtual IResultData<T> Register<T>(string entityName, T newItem) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Register<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Register<T>(entityName, item.Data);
        }

        public virtual IResultData<T> Unregister<T>(string entityName, object id, string token) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Unregister<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class
        {
            return Unregister<T>(entityName, item.Id, item.Token);
        }


        public virtual IResultData<T> Query<T>(string entityName, Func<T, bool> query, int page = 0, int rowcount = 0, string orderby = "") where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T> Query<T>(string entityName, int page, int rowcount, string predicate, string orderby, params object[] param) where T : class
        {
            return default(IResultData<T>);
        }

        public virtual IResultData<T1> Query<T, T1>(string entityName, Func<T, bool> query, Func<T, T1> selector, int page = 0, int rowcount = 0, string orderby = "")
            where T : class
            where T1 : class
        {
            return default(IResultData<T1>);
        }

        public virtual IResultData<T1> Query<T, T1>(string entityName, int page, int rowcount, string predicate, string orderby, Func<T, T1> selector, params object[] param)
            where T : class
            where T1 : class
        {
            return default(IResultData<T1>);
        }

        public virtual IResultData<T1> DoModifiedStatus<T1, T2>(string entityName, object id, string status, CommandTypes ct, string token, string notes)
            where T2 : BaseExecuteResultData<T1>
            where T1 : class
        {
            return null; 
        }
        public virtual IResultData<T1> DoModifiedStatusEx<T1, T2, T3>(string entityName, object id, string status, CommandTypes ct, string token, string notes)
            where T2 : BaseExecuteResultData<T1>
            where T1 : class
            where T3 : FlowProcessItems
        {
            return null;
        }

        public virtual IResultData<T> ExecuteCommand<T, T2>(string commandName, T2 item) where T : class where T2  : class 
        {
            return null;
        }

        public virtual IResultData<T> ExecuteQuery<T, T2>(string commandName, T2 item) where T : class where T2 : class
        {
            return null;
        }

    }

    
}

