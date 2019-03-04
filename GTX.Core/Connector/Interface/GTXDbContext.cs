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
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.ComponentModel.DataAnnotations;
using GTX.Models;
using GTX.Bootstrapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTX
{
    public class GTXDbContext : DbContext
    {
        public GTXDbContext()
        {

        }

        public GTXDbContext(string nameOrConnectionstring) : base(nameOrConnectionstring)
        {

        }

        ///public DbSet<AuditTrailLog> AuditTrailLogs { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public virtual int SaveChanges(string transactionid, IContext context)
        {
            //List<AuditTrailLog> result = new List<AuditTrailLog>();
            var currentTime = DateTime.Now;
            var transact = ChangeTracker.Entries().Where(e => (e.Entity is AuditTransaction) && ((e.Entity as AuditTransaction).TransactionId.Equals(transactionid))).SingleOrDefault();
            foreach (var entity in ChangeTracker.Entries().Where(p => (p.State == EntityState.Added) || (p.State == EntityState.Deleted) || (p.State == EntityState.Modified)))
            {

                // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
                string tableName = entity.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() is TableAttribute tableAttr ? tableAttr.Name : entity.Entity.GetType().Name;

                // Get primary key value (If you have more than one key column, this will need to be adjusted)
                var keyNames = entity.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).ToList();
                string keyName = keyNames[0].Name;

                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is DataModels)
                    {
                        (entity.Entity as DataModels).CreatedBy = BootStrapManager.User;
                        (entity.Entity as DataModels).CreatedOn = currentTime;
                    }
                }
                if (entity.Entity is IJournalEntity)
                    (entity.Entity as IJournalEntity).EntryDate = currentTime;
                if (entity.Entity is DataModels)
                {
                    (entity.Entity as DataModels).UpdatedBy = BootStrapManager.User;
                    (entity.Entity as DataModels).UpdatedOn = currentTime;
                    (entity.Entity as DataModels).LastTrxId = transactionid; //.TrxId = transactionid;
                    try
                    {
                        if (BootStrapManager.RequestContext != null)
                            (entity.Entity as DataModels).LastActivityContextId = BootStrapManager.RequestContext.ContextId.ToString();
                        else
                            (entity.Entity as DataModels).LastActivityContextId = GTX_String_Utility.CreateUniqueId;
                    }
                    catch
                    {
                    }
                }
                /// make auditlog if necessary
                //if (entity.Entity is DataModels)
                //{
                //    if (entity.State == EntityState.Added)
                //    {
                //        foreach (var propName in entity.CurrentValues.PropertyNames)
                //        {
                //            var audit = new AuditTrailLog
                //            {
                //                ActivityContextId = (entity.Entity as DataModels).ActivityContextId,
                //                TransactionId = (entity.Entity as DataModels).TrxId,
                //                ClassName = entity.Entity.GetType().FullName,
                //                CommandTypes = (transact.Entity as AuditTransaction).CommandTypes,
                //                EventDate = currentTime,
                //                EventType = "add",
                //                TableName = tableName,
                //                UserId = BootStrapManager.User,
                //                ColumnName = propName,
                //                RecordId = entity.CurrentValues.GetValue<object>(keyName).ToString(),
                //                NewValue = entity.CurrentValues.GetValue<object>(propName)?.ToString()
                //            };
                //            AuditTrailLogs.Add(audit);
                //        }
                //    }
                //    else if (entity.State == EntityState.Deleted)
                //    {
                //        foreach (var propName in entity.CurrentValues.PropertyNames)
                //        {
                //            var audit = new AuditTrailLog
                //            {
                //                ActivityContextId = (entity.Entity as DataModels).ActivityContextId,
                //                TransactionId = (entity.Entity as DataModels).TrxId,
                //                ClassName = entity.Entity.GetType().FullName,
                //                CommandTypes = (transact.Entity as AuditTransaction).CommandTypes,
                //                EventDate = currentTime,
                //                EventType = "delete",
                //                TableName = tableName,
                //                UserId = BootStrapManager.User,
                //                ColumnName = propName,
                //                RecordId = entity.CurrentValues.GetValue<object>(keyName).ToString(),
                //                NewValue = entity.OriginalValues.GetValue<object>(propName)?.ToString()
                //            };
                //            AuditTrailLogs.Add(audit);
                //        }
                //    }
                //    else if (entity.State == EntityState.Modified)
                //    {
                //        foreach (var propName in entity.CurrentValues.PropertyNames)
                //        {
                //            var audit = new AuditTrailLog
                //            {
                //                ActivityContextId = (entity.Entity as DataModels).ActivityContextId,
                //                TransactionId = (entity.Entity as DataModels).TrxId,
                //                ClassName = entity.Entity.GetType().FullName,
                //                CommandTypes = (transact.Entity as AuditTransaction).CommandTypes,
                //                EventDate = currentTime,
                //                EventType = "modified",
                //                TableName = tableName,
                //                UserId = BootStrapManager.User,
                //                ColumnName = propName,
                //                RecordId = entity.CurrentValues.GetValue<object>(keyName).ToString(),
                //                NewValue = entity.CurrentValues.GetValue<object>(propName)?.ToString(),
                //                OriginalValue = entity.OriginalValues.GetValue<object>(propName)?.ToString()
                //            };
                //            AuditTrailLogs.Add(audit);
                //        }
                //    }

                //}
                

            }
            return base.SaveChanges();
        }
    }
    
}

