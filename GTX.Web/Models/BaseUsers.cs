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
    [Table("m_users")]
    public class BaseUsers : IdentityUser, 
        IBaseModel<string>, 
        IDataModels, 
        IRegisterable, 
        IActivable, 
        IConfirmable
    {
        /* Identity User */
        //
        // Summary:
        //     Constructor which creates a new Guid for the Id
        public BaseUsers() : base()
        {

        }
        //
        // Summary:
        //     Constructor that takes a userName
        //
        // Parameters:
        //   userName:
        public BaseUsers(string userName) : base()
        {
            UserName = userName;
        }

        //
        // Summary:
        //     User name
        public override string UserName
        {
            get { return base.UserName; }
            set
            {
                var prev = base.UserName;
                OnPropertyChanging("UserName", value, base.UserName);
                base.UserName = value;
                OnPropertyChanged("UserName", value, prev);
            }
        }

        public override string Email
        {
            get { return base.Email; }
            set
            {
                var prev = base.UserName;
                OnPropertyChanging("Email", value, base.Email);
                base.Email = value;
                OnPropertyChanged("Email", value, prev);
            }
        }

        [Required, Display(Name = "Email Is Confirmed"), Column("email_is_confirmed")]
        public override bool EmailConfirmed
        {
            get { return base.EmailConfirmed; }
            set
            {
                var prev = base.EmailConfirmed;
                OnPropertyChanging("EmailConfirmed", value, base.EmailConfirmed);
                base.EmailConfirmed = value;
                OnPropertyChanged("EmailConfirmed", value, prev);
            }
        }

        [Required, Display(Name = "Password"), Column("user_password")]
        public override string PasswordHash
        {
            get { return base.PasswordHash; }
            set
            {
                var prev = base.PasswordHash;
                OnPropertyChanging("PasswordHash", value, base.PasswordHash);
                base.PasswordHash = value;
                OnPropertyChanged("PasswordHash", value, prev);
            }
        } 

        public override string SecurityStamp
        {
            get { return base.SecurityStamp; }
            set
            {
                var prev = base.SecurityStamp;
                OnPropertyChanging("SecurityStamp", value, base.SecurityStamp);
                base.SecurityStamp = value;
                OnPropertyChanged("SecurityStamp", value, prev);
            }
        } 

        [Required, Display(Name = "Mobile Number"), Column("mobile_number")]
        public override string PhoneNumber
        {
            get { return base.PhoneNumber; }
            set
            {
                var prev = base.PhoneNumber;
                OnPropertyChanging("PhoneNumber", value, base.PhoneNumber);
                base.PhoneNumber = value;
                OnPropertyChanged("PhoneNumber", value, prev);
            }
        } 

        [Required, Display(Name = "Mobile Number Confirmed"), Column("mobile_number_confirmed")]
        public override bool PhoneNumberConfirmed
        {
            get { return base.PhoneNumberConfirmed; }
            set
            {
                var prev = base.PhoneNumberConfirmed;
                OnPropertyChanging("PhoneNumberConfirmed", value, base.PhoneNumberConfirmed);
                base.PhoneNumberConfirmed = value;
                OnPropertyChanged("PhoneNumberConfirmed", value, prev);
            }
        } 

        public override bool TwoFactorEnabled
        {
            get { return base.TwoFactorEnabled; }
            set
            {
                var prev = base.TwoFactorEnabled;
                OnPropertyChanging("TwoFactorEnabled", value, base.TwoFactorEnabled);
                base.TwoFactorEnabled = value;
                OnPropertyChanged("TwoFactorEnabled", value, prev);
            }
        }

        public override DateTime? LockoutEndDateUtc
        {
            get { return base.LockoutEndDateUtc; }
            set
            {
                var prev = base.LockoutEndDateUtc;
                OnPropertyChanging("LockoutEndDateUtc", value, base.LockoutEndDateUtc);
                base.LockoutEndDateUtc = value;
                OnPropertyChanged("LockoutEndDateUtc", value, prev);
            }
        }

        public override bool LockoutEnabled
        {
            get { return base.LockoutEnabled; }
            set
            {
                var prev = base.LockoutEnabled;
                OnPropertyChanging("LockoutEnabled", value, base.LockoutEnabled);
                base.LockoutEnabled = value;
                OnPropertyChanged("LockoutEnabled", value, prev);
            }
        }

        public override int AccessFailedCount
        {
            get { return base.AccessFailedCount; }
            set
            {
                var prev = base.AccessFailedCount;
                OnPropertyChanging("AccessFailedCount", value, base.AccessFailedCount);
                base.AccessFailedCount = value;
                OnPropertyChanged("AccessFailedCount", value, prev);
            }
        }

        private string _firstName;
        [Required, Display(Name = "First Name"), Column("first_name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                var prev = _firstName;
                OnPropertyChanging("FirstName", value, _firstName);
                _firstName = value;
                OnPropertyChanged("FirstName", value, prev);
            }
        }

        private string _lastName;
        [Required, Display(Name = "Last Name"), Column("last_name")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                var prev = _lastName;
                OnPropertyChanging("LastName", value, _lastName);
                _lastName = value;
                OnPropertyChanged("LastName", value, prev);
            }
        }

        private string _codeRefAssociate;
        [Required, Display(Name = "Associate Reference Id"), Column("code_ref_associate"), DefaultValue("0000000000")]
        public string CodeRefAssociate
        {
            get { return _codeRefAssociate; }
            set
            {
                var prev = _codeRefAssociate;
                OnPropertyChanging("CodeRefAssociate", value, _codeRefAssociate);
                _codeRefAssociate = value;
                OnPropertyChanged("CodeRefAssociate", value, prev);
            }
        }

        private string _codeWhiteLabel;
        [Required, Display(Name = "Associate WhiteLabel Id"), Column("code_ref_whitelabel"), DefaultValue("0000000000")]
        public string CodeWhiteLabel
        {
            get { return _codeWhiteLabel; }
            set
            {
                var prev = _codeWhiteLabel;
                OnPropertyChanging("CodeWhiteLabel", value, _codeWhiteLabel);
                _codeWhiteLabel = value;
                OnPropertyChanged("CodeWhiteLabel", value, prev);
            }
        }



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

        /**************************** IModel<TKey> ******************************************/
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier / primary key
        /// </value>
        [BsonId, BsonRequired]
        [Key, Required, Column("user_id")]
        public override string Id
        {
            get { return base.Id; }
            set
            {
                var prev = base.Id;
                OnPropertyChanging("Id", value, base.Id);
                base.Id = value;
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

        /************************* IRegisterable ************************************************/
        private DateTime? _registeredOn;
        [Display(Name = "Registered On"), Column("registered_on")]
        public virtual DateTime? RegisteredOn
        {
            get { return _registeredOn; }
            set
            {
                var prev = _registeredOn;
                OnPropertyChanging("RegisteredOn", value, _registeredOn);
                _registeredOn = value;
                OnPropertyChanged("RegisteredOn", value, prev);
            }
        }

        private string _registeredBy;
        [Display(Name = "Registered By"), Column("registered_by")]
        public virtual string RegisteredBy
        {
            get { return _registeredBy; }
            set
            {
                var prev = _registeredBy;
                OnPropertyChanging("RegisteredBy", value, _registeredBy);
                _registeredBy = value;
                OnPropertyChanged("RegisteredBy", value, prev);
            }
        }

        private string _registrationToken;
        [Display(Name = "Registration Token"), Column("registration_token")]
        public virtual string RegistrationToken
        {
            get { return _registrationToken; }
            set
            {
                var prev = _registrationToken;
                OnPropertyChanging("RegistrationToken", value, _registrationToken);
                _registrationToken = value;
                OnPropertyChanged("RegistrationToken", value, prev);
            }
        }

        private DateTime? _unregisteredOn;
        [Display(Name = "Unregistered On"), Column("unregistered_on")]
        public virtual DateTime? UnregisteredOn
        {
            get { return _unregisteredOn; }
            set
            {
                var prev = _unregisteredOn;
                OnPropertyChanging("UnregisteredOn", value, _unregisteredOn);
                _unregisteredOn = value;
                OnPropertyChanged("UnregisteredOn", value, prev);
            }
        }

        private string _unregisteredBy;
        [Display(Name = "Unregistered By"), Column("unregistered_by")]
        public virtual string UnregisteredBy
        {
            get { return _unregisteredBy; }
            set
            {
                var prev = _unregisteredBy;
                OnPropertyChanging("UnregisteredBy", value, _unregisteredBy);
                _unregisteredBy = value;
                OnPropertyChanged("UnregisteredBy", value, prev);
            }
        }
        /************************* IRegisterable ************************************************/

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

        /*********************** IConfirmable ************************************************/
        private DateTime? _requestConfirmationOn;
        [Display(Name = "Request Confirmation On"), Column("request_confirmation_on")]
        public DateTime? RequestConfirmationOn
        {
            get { return _requestConfirmationOn; }
            set
            {
                var prev = _requestConfirmationOn;
                OnPropertyChanging("RequestConfirmationOn", value, _requestConfirmationOn);
                _requestConfirmationOn = value;
                OnPropertyChanged("RequestConfirmationOn", value, prev);
            }
        }

        private string _requestConfirmationBy;
        [Display(Name = "Request Confirmation By"), Column("request_confirmation_by")]
        public string RequestConfirmationBy
        {
            get { return _requestConfirmationBy; }
            set
            {
                var prev = _requestConfirmationBy;
                OnPropertyChanging("RequestConfirmationBy", value, _requestConfirmationBy);
                _requestConfirmationBy = value;
                OnPropertyChanged("RequestConfirmationBy", value, prev);
            }
        }

        private string _requestConfirmationNotes;
        [Display(Name = "Request Confirmation Notes"), Column("request_confirmation_notes")]
        public string RequestConfirmationNotes
        {
            get { return _requestConfirmationNotes; }
            set
            {
                var prev = _requestConfirmationNotes;
                OnPropertyChanging("RequestConfirmationNotes", value, _requestConfirmationNotes);
                _requestConfirmationNotes = value;
                OnPropertyChanged("RequestConfirmationNotes", value, prev);
            }
        }

        private DateTime? _confirmedOn;
        [Display(Name = "Confirmed On"), Column("confirmed_on")]
        public DateTime? ConfirmedOn
        {
            get { return _confirmedOn; }
            set
            {
                var prev = _confirmedOn;
                OnPropertyChanging("ConfirmedOn", value, _confirmedOn);
                _confirmedOn = value;
                OnPropertyChanged("ConfirmedOn", value, prev);
            }
        }

        private string _confirmedBy;
        [Display(Name = "Confirmed By"), Column("confirmed_by")]
        public string ConfirmedBy
        {
            get { return _confirmedBy; }
            set
            {
                var prev = _confirmedBy;
                OnPropertyChanging("ConfirmedBy", value, _confirmedBy);
                _confirmedBy = value;
                OnPropertyChanged("ConfirmedBy", value, prev);
            }
        }

        private string _confirmationToken;
        [Display(Name = "Confirmation Token"), Column("confirmation_token")]
        public string ConfirmationToken
        {
            get { return _confirmationToken; }
            set
            {
                var prev = _confirmationToken;
                OnPropertyChanging("ConfirmationToken", value, _confirmationToken);
                _confirmationToken = value;
                OnPropertyChanged("ConfirmationToken", value, prev);
            }
        }
        /*********************** IConfirmable ************************************************/


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
