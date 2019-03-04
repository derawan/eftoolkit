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
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace GTX.Models
{
    /* Entity */
    public abstract class BaseEntityModel<TKey> : DataModels, IBaseModel<TKey> 
        
    {
        /**************************** IModel<TKey> ******************************************/
        private TKey _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier / primary key
        /// </value>
        [BsonId, BsonRequired]
        [Key, Required, Column("id")]
        public virtual TKey Id
        {
            get { return _id; }
            set
            {
                var prev = _id;
                OnPropertyChanging("Id", value, _id);
                _id = value;
                OnPropertyChanged("Id", value, prev);
            }
        }
        /**************************** IModel<TKey> ******************************************/

        /**************************** IBaseModel<TKey> ******************************************/
        private string _name = string.Empty;
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [StringLength(150), Required, BsonRequired, Column("nama")]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                var prev = _name;
                OnPropertyChanging("Name", value, _name);
                _name = value;
                OnPropertyChanged("Name", value, prev);
            }
        }
        /**************************** IBaseModel<TKey> ******************************************/

    }

    /* Transaksi */
    public abstract class BaseTransactionJournal : ITransactionJournal, IBaseModel<string>, IModel<string>, IJournalEntity,
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        /**************************** IModel<TKey> ******************************************/
        protected string _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier / primary key this will be used to record lasttransaction_id on BaseEntityModel
        /// </value>
        [BsonId, BsonRequired]
        [Key, Required, Column("transaction_id")]
        public virtual string Id
        {
            get { return _id; }
            set
            {
                var prev = _id;
                OnPropertyChanging("Id", value, _id);
                _id = value;
                OnPropertyChanged("Id", value, prev);
            }
        }
        /**************************** IModel<TKey> ******************************************/

        /**************************** IBaseModel<TKey> ******************************************/
        private string _name = string.Empty;
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [StringLength(150), Required, BsonRequired, Column("transaction_name")]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                var prev = _name;
                OnPropertyChanging("Name", value, _name);
                _name = value;
                OnPropertyChanged("Name", value, prev);
            }
        }
        /**************************** IBaseModel<TKey> ******************************************/

        /**************************** IJournalEntity ******************************************/
        protected DateTime _entryDate;
        [Required, Column("transaction_date")]
        public virtual DateTime EntryDate
        {
            get { return _entryDate; }
            set
            {
                var prev = _entryDate;
                OnPropertyChanging("EntryDate", value, _entryDate);
                _entryDate = value;
                OnPropertyChanged("EntryDate", value, prev);
            }
        }

        protected string _entryUser;
        [Required, Column("transaction_user")]
        public virtual string EntryUser
        {
            get { return _entryUser; }
            set
            {
                var prev = _entryUser;
                OnPropertyChanging("EntryUser", value, _entryUser);
                _entryUser = value;
                OnPropertyChanged("EntryUser", value, prev);
            }
        }

        protected string _status = Constant.EntityStatus.Inactived;
        /// <summary>
        /// Entity Status, if soft deleted then Status should be "*DELETED*" see Constant.EntityStatus
        /// if (Status is "*LOCKED*" you can't delete this data, you only able to modify the data but 
        /// GTX.Core.Constant.EntityStatus
        /// not delete the data
        /// </summary>
        [Display(Name = "Status"), Column("transaction_status")]
        public virtual string Status
        {
            get { return _status; }
            set
            {
                var prev = _status;
                OnPropertyChanging("Status", value, _status);
                _status = value;
                OnPropertyChanged("Status", value, prev);
            }
        }

        protected string _transactionNotes;
        [Display(Name = "Transaction Notes"), Column("transaction_notes")]
        public string Notes
        {
            get { return _transactionNotes; }
            set
            {
                var prev = _transactionNotes;
                OnPropertyChanging("Notes", value, _transactionNotes);
                _transactionNotes = value;
                OnPropertyChanged("Notes", value, prev);
            }
        }
        /**************************** IJournalEntity ******************************************/



        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanging(string propertyName, object newValue, object prevValue)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void OnPropertyChanged(string propertyName, object newValue, object prevValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /* Activity */
    public abstract class BaseActivityJournal<TModelKey> :
        IActivityJournal<TModelKey>,
        IModel<string>,
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        
        /**************************** IModel<TKey> ******************************************/
        protected string _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier / primary key this will be used to record lastactivitycontext_id on BaseEntityModel
        /// </value>
        [BsonId, BsonRequired]
        [Key, Required, Column("journal_id")]
        public virtual string Id
        {
            get { return _id; }
            set
            {
                var prev = _id;
                OnPropertyChanging("Id", value, _id);
                _id = value;
                OnPropertyChanged("Id", value, prev);
            }
        }
        /**************************** IModel<TKey> ******************************************/

        /**************************** IActivityJournal<TModelKey> ******************************************/
        protected TModelKey _modelReferenceId;
        /// <summary>
        /// Primary Key of the Main Data
        /// </summary>
        [Column("datareference_id")]
        public TModelKey ReferenceId
        {
            get { return _modelReferenceId; }
            set
            {
                var prev = _modelReferenceId;
                OnPropertyChanging("ReferenceId", value, _modelReferenceId);
                _modelReferenceId = value;
                OnPropertyChanged("ReferenceId", value, prev);
            }
        }

        protected CommandTypes _commandType;
        [Required, Column("command_type")]
        public virtual CommandTypes CommandType
        {
            get { return _commandType; }
            set
            {
                var prev = _commandType;
                OnPropertyChanging("CommandType", value, _commandType);
                _commandType = value;
                OnPropertyChanged("CommandType", value, prev);
            }
        }
        /**************************** IActivityJournal<TModelKey> ******************************************/

        /**************************** IJournalEntity ******************************************/
        protected DateTime _entryDate;
        [Required, Column("activity_date")]
        public DateTime EntryDate
        {
            get { return _entryDate; }
            set
            {
                var prev = _entryDate;
                OnPropertyChanging("EntryDate", value, _entryDate);
                _entryDate = value;
                OnPropertyChanged("EntryDate", value, prev);
            }
        }

        protected string _entryUser;
        [Required, Column("activity_user")]
        public virtual string EntryUser
        {
            get { return _entryUser; }
            set
            {
                var prev = _entryUser;
                OnPropertyChanging("EntryUser", value, _entryUser);
                _entryUser = value;
                OnPropertyChanged("EntryUser", value, prev);
            }
        }

        protected string _status = string.Empty; // Constant.EntityStatus.Inactived;
        /// <summary>
        /// Entity Status, if soft deleted then Status should be "*DELETED*" see Constant.EntityStatus
        /// if (Status is "*LOCKED*" you can't delete this data, you only able to modify the data but 
        /// GTX.Core.Constant.EntityStatus
        /// not delete the data
        /// </summary>
        [Display(Name = "Status"), Column("activity_status")]
        public virtual string Status
        {
            get { return _status; }
            set
            {
                var prev = _status;
                OnPropertyChanging("Status", value, _status);
                _status = value;
                OnPropertyChanged("Status", value, prev);
            }
        }

        protected string _activityNotes;
        [Display(Name = "Activity Notes"), Column("activity_notes")]
        public string Notes
        {
            get { return _activityNotes; }
            set
            {
                var prev = _activityNotes;
                OnPropertyChanging("Notes", value, _activityNotes);
                _activityNotes = value;
                OnPropertyChanged("Notes", value, prev);
            }
        }

        /**************************** IJournalEntity ******************************************/




        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanging(string propertyName, object newValue, object prevValue)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void OnPropertyChanged(string propertyName, object newValue, object prevValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /* Audit Trail */
    public abstract class BaseAuditTrailEntry : IAuditTrailJournal, IModel<long>, IJournalEntity,
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        /**************************** IModel<TKey> ******************************************/
        protected long _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier / primary key
        /// </value>
        [BsonId, BsonRequired]
        [Key, Required, Column("id")]
        public virtual long Id
        {
            get { return _id; }
            set
            {
                var prev = _id;
                OnPropertyChanging("Id", value, _id);
                _id = value;
                OnPropertyChanged("Id", value, prev);
            }
        }
        /**************************** IModel<TKey> ******************************************/

        /**************************** IAuditTrailJournal ******************************************/
        protected string _transactionID = string.Empty;
        /// <summary>
        /// Transaction Id Journal which process this data 
        /// </summary>
        [Display(Name = "Transaction Id", ShortName = "Trx Id"), Column("transaction_id"), Required, BsonRequired]
        public virtual string TransactionId
        {
            get { return _transactionID; }
            set
            {
                var prev = _transactionID;
                OnPropertyChanging("TransactionId", value, _transactionID);
                _transactionID = value;
                OnPropertyChanged("TransactionId", value, prev);
            }
        }

        protected string _activityContextId = string.Empty;
        /// <summary>
        /// ActivityContext Id Journal which process this data
        /// </summary>
        [Display(Name = "ActivityContext Id", ShortName = "Activity Id"), Column("activitycontext_id"), Required]
        public virtual string ActivityContextId
        {
            get { return _activityContextId; }
            set
            {
                var prev = _activityContextId;
                OnPropertyChanging("ActivityContextId", value, _activityContextId);
                _activityContextId = value;
                OnPropertyChanged("ActivityContextId", value, prev);
            }
        }

        protected string _eventType;
        public string EventType
        {
            get { return _eventType; }
            set
            {
                var prev = _eventType;
                OnPropertyChanging("EventType", value, _eventType);
                _eventType = value;
                OnPropertyChanged("EventType", value, prev);
            }
        }
        protected CommandTypes _commandType;
        [Required, Column("command_type")]
        public virtual CommandTypes CommandType
        {
            get { return _commandType; }
            set
            {
                var prev = _commandType;
                OnPropertyChanging("CommandType", value, _commandType);
                _commandType = value;
                OnPropertyChanged("CommandType", value, prev);
            }
        }

        protected string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set
            {
                var prev = _tableName;
                OnPropertyChanging("TableName", value, _tableName);
                _tableName = value;
                OnPropertyChanged("TableName", value, prev);
            }
        }

        protected string _className;
        public string ClassName
        {
            get { return _className; }
            set
            {
                var prev = _className;
                OnPropertyChanging("ClassName", value, _className);
                _className = value;
                OnPropertyChanged("ClassName", value, prev);
            }
        }

        protected string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set
            {
                var prev = _columnName;
                OnPropertyChanging("ColumnName", value, _columnName);
                _columnName = value;
                OnPropertyChanged("ColumnName", value, prev);
            }
        }

        protected string _recordIdType;
        public string RecordIdType
        {
            get { return _recordIdType; }
            set
            {
                var prev = _recordIdType;
                OnPropertyChanging("RecordIdType", value, _recordIdType);
                _recordIdType = value;
                OnPropertyChanged("RecordIdType", value, prev);
            }
        }

        protected string _recordId;
        public string RecordId
        {
            get { return _recordId; }
            set
            {
                var prev = _recordId;
                OnPropertyChanging("RecordId", value, _recordId);
                _recordId = value;
                OnPropertyChanged("RecordId", value, prev);
            }
        }

        protected string _valueType;
        public string ValueType
        {
            get { return _valueType; }
            set
            {
                var prev = _valueType;
                OnPropertyChanging("ValueType", value, _valueType);
                _valueType = value;
                OnPropertyChanged("ValueType", value, prev);
            }
        }

        protected string _newValue;
        public string NewValue
        {
            get { return _newValue; }
            set
            {
                var prev = _newValue;
                OnPropertyChanging("NewValue", value, _newValue);
                _newValue = value;
                OnPropertyChanged("NewValue", value, prev);
            }
        }

        protected string _originalValue;
        public string OriginalValue
        {
            get { return _originalValue; }
            set
            {
                var prev = _originalValue;
                OnPropertyChanging("OriginalValue", value, _originalValue);
                _originalValue = value;
                OnPropertyChanged("OriginalValue", value, prev);
            }
        }
        /**************************** IAuditTrailJournal ******************************************/

        /**************************** IJournalEntity ******************************************/
        protected DateTime _entryDate;
        [Required, Column("audittrail_date")]
        public virtual DateTime EntryDate
        {
            get { return _entryDate; }
            set
            {
                var prev = _entryDate;
                OnPropertyChanging("EntryDate", value, _entryDate);
                _entryDate = value;
                OnPropertyChanged("EntryDate", value, prev);
            }
        }

        protected string _entryUser;
        [Required, Column("audittrail_user")]
        public virtual string EntryUser
        {
            get { return _entryUser; }
            set
            {
                var prev = _entryUser;
                OnPropertyChanging("EntryUser", value, _entryUser);
                _entryUser = value;
                OnPropertyChanged("EntryUser", value, prev);
            }
        }

        protected string _status = string.Empty; // Constant.EntityStatus.Inactived;
        /// <summary>
        /// Entity Status, if soft deleted then Status should be "*DELETED*" see Constant.EntityStatus
        /// if (Status is "*LOCKED*" you can't delete this data, you only able to modify the data but 
        /// GTX.Core.Constant.EntityStatus
        /// not delete the data
        /// </summary>
        [Display(Name = "Status"), Column("audittrail_status")]
        public virtual string Status
        {
            get { return _status; }
            set
            {
                var prev = _status;
                OnPropertyChanging("Status", value, _status);
                _status = value;
                OnPropertyChanged("Status", value, prev);
            }
        }
        protected string _audittrailNotes;
        [Display(Name = "Audit Trail Notes"), Column("audittrail_notes")]
        public string Notes
        {
            get { return _audittrailNotes; }
            set
            {
                var prev = _audittrailNotes;
                OnPropertyChanging("Notes", value, _audittrailNotes);
                _audittrailNotes = value;
                OnPropertyChanged("Notes", value, prev);
            }
        }
        /**************************** IJournalEntity ******************************************/

        


        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanging(string propertyName, object newValue, object prevValue)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void OnPropertyChanged(string propertyName, object newValue, object prevValue)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [Table("log_transaction")]
    public class AuditTransaction
    {
        // No Transaksi
        [Key,Column("transaction_id")]
        public string TransactionId { get; set; }

        // Tanggal Transaksi
        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; }

        // User Transaksi
        [Column("transaction_user")]
        public string TransactionUser { get; set; }

        // Command Transaksi
        [Column("command_type")]
        public CommandTypes CommandTypes { get; set; }
    }


    public class DeleteLogTransaction
    {
        [Key,Column("id")]
        public string Id { get; set; }

        [Column("deletion_date")]
        public DateTime DeletionDate { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("record_id")]
        public string RecordId { get; set; }

        [Column("command_type")]
        public CommandTypes CommandTypes { get; set; }

        [Column("table_name")]
        public string TableName { get; set; }

        [Column("class_name")]
        public string ClassName { get; set; }

        [Column("record_data")]
        public string RecordData { get; set; }

        [Column("record_status")]
        public string RecordStatus { get; set; }

    }

    [Table("log_audittrail")]
    public class AuditTrailLog
    {
        [Key]
        public long         Id { get; set; }
        public string       TransactionId { get; set; }
        public string       ActivityContextId { get; set; }
        public DateTime     EventDate { get; set; }
        public string       EventType { get; set; }
        public CommandTypes CommandTypes { get; set; }
        public string       UserId { get; set; }
        public string       TableName { get; set; }
        public string       ClassName { get; set; }
        public string       ColumnName { get; set; }
        public string       RecordId { get; set; }
        public string       NewValue { get; set; }
        public string       OriginalValue { get; set; }
    }



    public class BaseEntityFlowModel
    {

    }
}
