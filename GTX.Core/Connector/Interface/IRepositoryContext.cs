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
using GTX.Models;
using System;
using System.Collections.Generic;

namespace GTX
{
    public interface IRepositoryContext : IContext, IDisposableObjects
    {

        void InitConnector(string constring = "");
        //string DatabaseName { get; set; }

        bool InTransaction { get;}

        string TransactionId { get; set; }

        void StartTransaction();

        //void CommitTransaction();

        //void Rollback();

        void SaveChanges();

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T FindById<T>(string entityName, object id);

        /// <summary>
        /// Finds the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<T> Find<T>(string entityName, Func<T, bool> query) where T : class;

        T GetPreviousValues<T>(T item) where T : class;

        ///*01.CommandTypes.Add*/
        IResultData<T> Add<T>(string entityName, T newItem) where T : class;

        ///*02.CommandTypes.Add*/
        IResultData<T> Add<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> newItem) where T : class;

        ///*03.CommandTypes.Edit*/
        IResultData<T> Update<T>(string entityName, object id, T item) where T : class;

        ///*04.CommandTypes.Edit*/
        IResultData<T> Update<T, TModelKey>(string entityName, object id, ICommandParameters<T, TModelKey> item) where T : class;

        ///*05.CommandTypes.Delete*/
        IResultData<T> Delete<T>(string entityName, object id) where T : class;

        ///*06.CommandTypes.Delete*/
        IResultData<T> Delete<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*07.CommandTypes.Remove*/
        IResultData<T> Remove<T>(string entityName, object id) where T : class;

        ///*08.CommandTypes.Remove*/
        IResultData<T> Remove<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*09.CommandTypes.Restore*/
        IResultData<T> Restore<T>(string entityName, object id) where T : class;

        ///*10.CommandTypes.Restore*/
        IResultData<T> Restore<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*11.CommandTypes.RequestApproval*/
        IResultData<T> RequestApproval<T>(string entityName, object id, string notes) where T : class;

        ///*12.CommandTypes.RequestApproval*/
        IResultData<T> RequestApproval<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*13.CommandTypes.Approve*/
        IResultData<T> Approve<T>(string entityName, object id, string token, string notes) where T : class;

        ///*14.CommandTypes.Approve*/
        IResultData<T> Approve<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*15.CommandTypes.Reject*/
        IResultData<T> Reject<T>(string entityName, object id, string token, string reason) where T : class;

        ///*16.CommandTypes.Reject*/
        IResultData<T> Reject<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*17.CommandTypes.RequestReview*/
        IResultData<T> RequestReview<T>(string entityName, object id, string notes) where T : class;

        ///*18.CommandTypes.RequestReview*/
        IResultData<T> RequestReview<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*19.CommandTypes.Review*/
        IResultData<T> Review<T>(string entityName, object id, string token, string review) where T : class;

        ///*20.CommandTypes.Review*/
        IResultData<T> Review<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*21.CommandTypes.Publish*/
        IResultData<T> Publish<T>(string entityName, object id, string token) where T : class;

        ///*22.CommandTypes.Publish*/
        IResultData<T> Publish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*23.CommandTypes.Unpublish*/
        IResultData<T> Unpublish<T>(string entityName, object id, string token) where T : class;

        ///*24.CommandTypes.Unpublish*/
        IResultData<T> Unpublish<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*25.CommandTypes.RequestActivation*/
        IResultData<T> RequestActivation<T>(string entityName, object id, string notes) where T : class;

        ///*26.CommandTypes.RequestActivation*/
        IResultData<T> RequestActivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*27.CommandTypes.Activate*/
        IResultData<T> Activate<T>(string entityName, object id, string token) where T : class;

        ///*28.CommandTypes.Activate*/
        IResultData<T> Activate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*25.CommandTypes.RequestActivation*/
        IResultData<T> RequestDeactivation<T>(string entityName, object id, string notes) where T : class;

        ///*26.CommandTypes.RequestActivation*/
        IResultData<T> RequestDeactivation<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;
         
        ///*29.CommandTypes.Deactivate*/
        IResultData<T> DeActivate<T>(string entityName, object id, string token) where T : class;

        ///*30.CommandTypes.Deactivate*/
        IResultData<T> DeActivate<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*31.CommandTypes.RequestConfirmation*/
        IResultData<T> RequestConfirm<T>(string entityName, object id, string notes) where T : class;

        ///*32.CommandTypes.RequestConfirmation*/
        IResultData<T> RequestConfirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*33.CommandTypes.Confirm*/
        IResultData<T> Confirm<T>(string entityName, object id, string token) where T : class;

        ///*34.CommandTypes.Confirm*/
        IResultData<T> Confirm<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*35.CommandTypes.Register*/
        IResultData<T> Register<T>(string entityName, T newItem) where T : class;

        ///*36.CommandTypes.Register*/
        IResultData<T> Register<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        ///*37.CommandTypes.Unregister*/
        IResultData<T> Unregister<T>(string entityName, object id, string token) where T : class;

        ///*38.CommandTypes.Unregister*/
        IResultData<T> Unregister<T, TModelKey>(string entityName, ICommandParameters<T, TModelKey> item) where T : class;

        IResultData<T> Query<T>(string entityName, Func<T, bool> query, int page = 0, int rowcount = 0, string orderby = "") where T : class;

        IResultData<T> Query<T>(string entityName, int page, int rowcount, string predicate, string orderby, params object[] param) where T : class;

        IResultData<T1> Query<T, T1>(string entityName, Func<T, bool> query, Func<T, T1> selector, int page = 0, int rowcount = 0, string orderby = "")
            where T : class
            where T1 : class;

        IResultData<T1> Query<T, T1>(string entityName, int page, int rowcount, string predicate, string orderby, Func<T, T1> selector, params object[] param)
            where T : class
            where T1 : class;

        IResultData<T1> DoModifiedStatusEx<T1, T2, T3>(string entityName, object id, string status, CommandTypes ct, string token, string notes)
            where T2 : BaseExecuteResultData<T1>
            where T1 : class
            where T3 : FlowProcessItems;

        IResultData<T> ExecuteCommand<T, T2>(string commandName, T2 item) where T : class where T2 : class;

        IResultData<T> ExecuteQuery<T, T2>(string commandName, T2 item) where T : class where T2 : class;

    }



}

