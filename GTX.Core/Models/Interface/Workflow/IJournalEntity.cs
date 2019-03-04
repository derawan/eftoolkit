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
using System.ComponentModel;

namespace GTX.Models
{
    public interface IJournalEntity 
    {
        DateTime EntryDate { get; set; }
        string EntryUser { get; set; }
        string Status { get; set; }
        string Notes { get; set; }
    }

    public interface ITransactionJournal : IModel<string>, IJournalEntity,
       INotifyPropertyChanging, INotifyPropertyChanged
    {

    }

    public interface IActivityJournal<TModelKey> : IModel<string>,
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        TModelKey ReferenceId { get; set; }
        CommandTypes CommandType { get; set; }
    }

    public interface IAuditTrailJournal : IModel<long>, IJournalEntity,
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        string TransactionId { get; set; }
        string ActivityContextId { get; set; }
        string EventType { get; set; }
        CommandTypes CommandType { get; set; }
        string TableName { get; set; }
        string ClassName { get; set; }
        string ColumnName { get; set; }
        string RecordIdType { get; set; }
        string RecordId { get; set; }
        string ValueType { get; set; }
        string NewValue { get; set; }
        string OriginalValue { get; set; }
    }



}
