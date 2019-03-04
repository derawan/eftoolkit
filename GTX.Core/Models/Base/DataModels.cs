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
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GTX.Models
{
    public abstract class DataModels : IDataModels, ISoftDeletes
    {
        //protected string TransactionID = string.Empty;
        ///// <summary>
        ///// Transaction Id which process this data
        ///// </summary>
        //[Display(Name = "TransactionId", ShortName = "Trx Id"), Column("kode_transaksi"), Required, BsonRequired]
        //public virtual string TrxId
        //{
        //    get { return TransactionID; }
        //    set
        //    {
        //        var prev = TransactionID;
        //        OnPropertyChanging("TrxId", value, TransactionID);
        //        TransactionID = value;
        //        OnPropertyChanged("TrxId", value, prev);
        //    }
        //}

        //protected string AID = string.Empty;
        ///// <summary>
        ///// ActivityContext Id which process this data
        ///// </summary>
        //[Display(Name = "ActivityContextId", ShortName = "ActivityContext Id"), Column("kode_activityctx"), Required, BsonRequired]
        //public virtual string ActivityContextId
        //{
        //    get { return AID; }
        //    set
        //    {
        //        var prev = AID;
        //        OnPropertyChanging("ActivityContextId", value, AID);
        //        AID = value;
        //        OnPropertyChanged("ActivityContextId", value, prev);
        //    }
        //}
        /**************************** IDataModels ******************************************/

        protected DateTime createdOn = DateTime.Now;
        /// <summary>
        /// Data Of Creation : See CommandTypes : Add
        /// </summary>
        [Display(Name = "Created On"), Column("created_on")]
        public virtual DateTime CreatedOn
        {
            get { return createdOn; }
            set
            {
                var prev = createdOn;
                OnPropertyChanging("CreatedOn", value, createdOn);
                createdOn = value;
                OnPropertyChanged("CreatedOn", value, prev);
            }
        }


        protected string createdBy = string.Empty;
        /// <summary>
        /// This data is Created By : See CommandTypes : Add
        /// </summary>
        [Display(Name = "Created By"), Column("created_by")]
        public virtual string CreatedBy
        {
            get { return createdBy; }
            set
            {
                var prev = createdBy;
                OnPropertyChanging("CreatedBy", value, createdBy);
                createdBy = value;
                OnPropertyChanged("CreatedBy", value, prev);
            }
        }

        protected DateTime updatedOn = DateTime.Now;
        /// <summary>
        /// Last Modification Datetime / Timestamp : See CommandTypes : Edit
        /// </summary>
        [Display(Name = "Last Update On"), Column("updated_on")]
        public virtual DateTime UpdatedOn
        {
            get { return updatedOn; }
            set
            {
                var prev = updatedOn;
                OnPropertyChanging("UpdatedOn", value, updatedOn);
                updatedOn = value;
                OnPropertyChanged("UpdatedOn", value, prev);
            }
        }


        protected string updatedBy = string.Empty;
        /// <summary>
        /// Last Modification Done by : See CommandTypes : Edit
        /// </summary>
        [Display(Name = "Last Update By"), Column("updated_by")]
        public virtual string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                var prev = updatedBy;
                OnPropertyChanging("UpdatedBy", value, updatedBy);
                updatedBy = value;
                OnPropertyChanged("UpdatedBy", value, prev);
            }
        }

        private string _status = string.Empty; // Constant.EntityStatus.Inactived;
        /// <summary>
        /// Entity Status, if soft deleted then Status should be "*DELETED*" see Constant.EntityStatus
        /// if (Status is "*LOCKED*" you can't delete this data, you only able to modify the data but 
        /// GTX.Core.Constant.EntityStatus
        /// not delete the data
        /// </summary>
        [Display(Name = "Status"), Column("entity_status")]
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

        protected string _lastTransactionID = string.Empty;
        /// <summary>
        /// Transaction Id Journal which process this data 
        /// </summary>
        [Display(Name = "Last Transaction Id", ShortName = "Trx Id"), Column("lasttransaction_id"), Required, BsonRequired]
        public virtual string LastTrxId
        {
            get { return _lastTransactionID; }
            set
            {
                var prev = _lastTransactionID;
                OnPropertyChanging("LastTrxId", value, _lastTransactionID);
                _lastTransactionID = value;
                OnPropertyChanged("LastTrxId", value, prev);
            }
        }


        protected string _lastActivityContextId = string.Empty;
        /// <summary>
        /// ActivityContext Id Journal which process this data
        /// </summary>
        [Display(Name = "Last ActivityContext Id", ShortName = "Last Activity Id"), Column("lastactivitycontext_id"), Required]
        public virtual string LastActivityContextId
        {
            get { return _lastActivityContextId; }
            set
            {
                var prev = _lastActivityContextId;
                OnPropertyChanging("LastActivityContextId", value, _lastActivityContextId);
                _lastActivityContextId = value;
                OnPropertyChanged("LastActivityContextId", value, prev);
            }
        }

        /**************************** IDataModels ******************************************/


        /**************************** ISoftDeletes ******************************************/
        private DateTime? _deletedOn;
        /// <summary>
        /// Date when entity is soft deleted
        /// </summary>
        [Column("deleted_on")]
        public DateTime? DeletedOn
        {
            get { return _deletedOn; }
            set
            {
                var prev = _deletedOn;
                OnPropertyChanging("DeletedOn", value, _deletedOn);
                _deletedOn = value;
                OnPropertyChanged("DeletedOn", value, prev);
            }
        }

        private string _deletedBy;
        /// <summary>
        /// user who do the soft deletion
        /// </summary>
        [Column("deleted_by")]
        public string DeletedBy
        {
            get { return _deletedBy; }
            set
            {
                var prev = _deletedBy;
                OnPropertyChanging("DeletedBy", value, _deletedBy);
                _deletedBy = value;
                OnPropertyChanged("DeletedBy", value, prev);
            }
        }

        private string _deleteReason;
        /// <summary>
        /// soft deletion reason
        /// </summary>
        [Column("delete_reason")]
        public string DeletionReason
        {
            get { return _deleteReason; }
            set
            {
                var prev = _deleteReason;
                OnPropertyChanging("DeletionReason", value, _deleteReason);
                _deleteReason = value;
                OnPropertyChanged("DeletionReason", value, prev);
            }
        }

        private DateTime? _restoredOn;
        /// <summary>
        /// date when soft deleted data is restored
        /// </summary>
        [Column("restored_on")]
        public DateTime? RestoredOn
        {
            get { return _restoredOn; }
            set
            {
                var prev = _restoredOn;
                OnPropertyChanging("RestoredOn", value, _restoredOn);
                _restoredOn = value;
                OnPropertyChanged("RestoredOn", value, prev);
            }
        }

        private string _restoredBy;
        /// <summary>
        /// user who do data restoration
        /// </summary>
        [Column("restored_by")]
        public string RestoredBy
        {
            get { return _restoredBy; }
            set
            {
                var prev = _restoredBy;
                OnPropertyChanging("RestoredBy", value, _restoredBy);
                _restoredBy = value;
                OnPropertyChanged("RestoredBy", value, prev);
            }
        }

        private string _restoredNotes;
        /// <summary>
        /// restoration notes
        /// </summary>
        [Column("restored_notes")]
        public string RestoreNotes 
        {
            get { return _restoredNotes; }
            set
            {
                var prev = _restoredNotes;
                OnPropertyChanging("RestoreNotes", value, _restoredNotes);
                _restoredNotes = value;
                OnPropertyChanged("RestoreNotes", value, prev);
            }
        }


        private string _previousStatus;
        /// <summary>
        /// status before data is soft deleted
        /// </summary>
        [Column("previous_status")]
        public string PrevStatus
        {
            get { return _previousStatus; }
            set
            {
                var prev = _previousStatus;
                OnPropertyChanging("PrevStatus", value, _previousStatus);
                _previousStatus = value;
                OnPropertyChanged("PrevStatus", value, prev);
            }
        }
        /**************************** ISoftDeletes ******************************************/


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
}
