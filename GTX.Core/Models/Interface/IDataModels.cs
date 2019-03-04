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
    public interface IDataModels : INotifyPropertyChanging,  INotifyPropertyChanged
    {
        ///// <summary>
        ///// Transaction Id which process this data
        ///// </summary>
        //[Display(Name = "Transaction Id", ShortName = "Trx Id"), Column("Transaction_Id"), Required]
        //string TrxId { get; set; }

        ///// <summary>
        ///// ActivityContext Id which process this data
        ///// </summary>
        //[Display(Name = "ActivityContext Id", ShortName = "Activity Id"), Column("ActivityContext_Id"), Required]
        //string ActivityContextId { get; set; }


        /// <summary>
        /// Data Of Creation : See CommandTypes : Add
        /// </summary>
        [Display(Name = "Created On"), Column("created_on")]
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// This data is Created By : See CommandTypes : Add
        /// </summary>
        [Display(Name = "Created By"), Column("created_by")]
        string CreatedBy { get; set; }

        /// <summary>
        /// Last Modification Datetime / Timestamp : See CommandTypes : Edit
        /// </summary>
        [Display(Name = "Last Update On"), Column("updated_on"), Required]
        DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Last Modification Done by : See CommandTypes : Edit
        /// </summary>
        [Display(Name = "Last Update By"), Column("updated_by"), Required]
        string UpdatedBy { get; set; }

        /// <summary>
        /// Entity Status, if soft deleted then Status should be "*DELETED*" see Constant.EntityStatus
        /// if (Status is "*LOCKED*" you can't delete this data, you only able to modify the data but 
        /// OS.Core.Constant.EntityStatus
        /// not delete the data
        /// </summary>
        [Display(Name = "Status"), Column("entity_status")]
        string Status { get; set; }

        /// <summary>
        /// Transaction Id Journal which process this data 
        /// </summary>
        [Display(Name = "Last Transaction Id", ShortName = "Trx Id"), Column("lasttransaction_id"), Required, BsonRequired]
        string LastTrxId { get; set; }

        /// <summary>
        /// ActivityContext Id Journal which process this data
        /// </summary>
        [Display(Name = "Last ActivityContext Id", ShortName = "Last Activity Id"), Column("lastactivitycontext_id"), Required, BsonRequired]
        string LastActivityContextId { get; set; }

    }
}
