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
using GTX.Models;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace GTX.Web.Models
{
    [Table("m_roles")]
    public class BaseRole : IdentityRole, 
        IBaseModel<string>, 
        IDataModels, 
        IActivable
    {
        public BaseRole() : base() { }

        public BaseRole(string roleName) : base(roleName) { }

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

        /*********************** IActivable ************************************************/
        private DateTime? _requestActivationOn;
        [Display(Name = "Request Activation On"), Column("request_activation_on")]
        public virtual DateTime? RequestActivationOn
        {
            get { return _requestActivationOn; }
            set
            {
                var prev = _requestActivationOn;
                OnPropertyChanging("RequestActivationOn", value, _requestActivationOn);
                _requestActivationOn = value;
                OnPropertyChanged("RequestActivationOn", value, prev);
            }
        }

        private string _requestActivationBy;
        [Display(Name = "Request Activation By"), Column("request_activation_by")]
        public virtual string RequestActivationBy
        {
            get { return _requestActivationBy; }
            set
            {
                var prev = _requestActivationBy;
                OnPropertyChanging("RequestActivationBy", value, _requestActivationBy);
                _requestActivationBy = value;
                OnPropertyChanged("RequestActivationBy", value, prev);
            }
        }

        private string _requestActivationNotes;
        [Display(Name = "Request Activation Notes"), Column("request_activation_notes")]
        public virtual string RequestActivationNotes
        {
            get { return _requestActivationNotes; }
            set
            {
                var prev = _requestActivationNotes;
                OnPropertyChanging("RequestActivationNotes", value, _requestActivationNotes);
                _requestActivationNotes = value;
                OnPropertyChanged("RequestActivationNotes", value, prev);
            }
        }

        private string _requestActivationToken;
        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Activation Token"), Column("Request_Activation_Token")]
        public string RequestActivationToken
        {
            get { return _requestActivationToken; }
            set
            {
                var prev = _requestActivationToken;
                OnPropertyChanging("RequestActivationToken", value, _requestActivationToken);
                _requestActivationToken = value;
                OnPropertyChanged("RequestActivationToken", value, prev);
            }
        }


        private DateTime? _activationOn;
        [Display(Name = "Activated On"), Column("activated_on")]
        public virtual DateTime? ActivatedOn
        {
            get { return _activationOn; }
            set
            {
                var prev = _activationOn;
                OnPropertyChanging("ActivatedOn", value, _activationOn);
                _activationOn = value;
                OnPropertyChanged("ActivatedOn", value, prev);
            }
        }

        private string _activatedBy;
        [Display(Name = "Activated By"), Column("activated_by")]
        public virtual string ActivatedBy
        {
            get { return _activatedBy; }
            set
            {
                var prev = _activatedBy;
                OnPropertyChanging("ActivatedBy", value, _activatedBy);
                _activatedBy = value;
                OnPropertyChanged("ActivatedBy", value, prev);
            }
        }

        private string _activationToken;
        [Display(Name = "Activation Token"), Column("activation_token")]
        public virtual string ActivationToken
        {
            get { return _activationToken; }
            set
            {
                var prev = _activationToken;
                OnPropertyChanging("ActivationToken", value, _activationToken);
                _activationToken = value;
                OnPropertyChanged("ActivationToken", value, prev);
            }
        }

        private string _activationNotes;
        [Display(Name = "Activation Notes"), Column("activation_notes")]
        public virtual string ActivationNotes
        {
            get { return _activationNotes; }
            set
            {
                var prev = _activationNotes;
                OnPropertyChanging("ActivationNotes", value, _activationNotes);
                _activationNotes = value;
                OnPropertyChanged("ActivationNotes", value, prev);
            }
        }

        private DateTime? _requestDeactivationOn;
        [Display(Name = "Request De-Activation On"), Column("request_deactivation_on")]
        public virtual DateTime? RequestDeactivationOn
        {
            get { return _requestDeactivationOn; }
            set
            {
                var prev = _requestDeactivationOn;
                OnPropertyChanging("RequestDeactivationOn", value, _requestDeactivationOn);
                _requestDeactivationOn = value;
                OnPropertyChanged("RequestDeactivationOn", value, prev);
            }
        }

        private string _requestDeactivationBy;
        [Display(Name = "Request De-Activation By"), Column("request_deactivation_by")]
        public virtual string RequestDeactivationBy
        {
            get { return _requestDeactivationBy; }
            set
            {
                var prev = _requestDeactivationBy;
                OnPropertyChanging("RequestDeactivationBy", value, _requestDeactivationBy);
                _requestDeactivationBy = value;
                OnPropertyChanged("RequestDeactivationBy", value, prev);
            }
        }

        private string _requestDeactivationNotes;
        [Display(Name = "Request Activation Notes"), Column("request_deactivation_notes")]
        public virtual string RequestDeactivationNotes
        {
            get { return _requestDeactivationNotes; }
            set
            {
                var prev = _requestDeactivationNotes;
                OnPropertyChanging("RequestDeactivationNotes", value, _requestDeactivationNotes);
                _requestDeactivationNotes = value;
                OnPropertyChanged("RequestDeactivationNotes", value, prev);
            }
        }

        private string _requestDeactivationToken;
        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Activation Token"), Column("Request_Activation_Token")]
        public string RequestDeactivationToken
        {
            get { return _requestDeactivationToken; }
            set
            {
                var prev = _requestDeactivationToken;
                OnPropertyChanging("RequestDeactivationToken", value, _requestDeactivationToken);
                _requestDeactivationToken = value;
                OnPropertyChanged("RequestDeactivationToken", value, prev);
            }
        }


        private DateTime? _deactivatedOn;
        [Display(Name = "Deactivated On"), Column("deactivated_on")]
        public virtual DateTime? DeActivatedOn
        {
            get { return _deactivatedOn; }
            set
            {
                var prev = _deactivatedOn;
                OnPropertyChanging("DeActivatedOn", value, _deactivatedOn);
                _deactivatedOn = value;
                OnPropertyChanged("DeActivatedOn", value, prev);
            }
        }

        private string _deactivatedBy;
        [Display(Name = "Deactivated By"), Column("deactivated_by")]
        public virtual string DeActivatedBy
        {
            get { return _deactivatedBy; }
            set
            {
                var prev = _deactivatedBy;
                OnPropertyChanging("DeActivatedBy", value, _deactivatedBy);
                _deactivatedBy = value;
                OnPropertyChanged("DeActivatedBy", value, prev);
            }
        }
        /*********************** IActivable ************************************************/

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
