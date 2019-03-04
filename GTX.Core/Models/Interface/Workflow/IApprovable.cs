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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 
namespace GTX.Models
{
    public interface IApprovable
    {
        /// <summary>
        /// This data approval requested on
        /// </summary>
        [Display(Name = "Request Approval On"), Column("Request_Approval_On")]
        DateTime? RequestApprovedOn { get; set; }

        /// <summary>
        /// This data approval requested by
        /// </summary>
        [Display(Name = "Request Approval By"), Column("Request_Approval_By")]
        string RequestApprovedBy { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Approval Notes"), Column("Request_Approval_Notes")]
        string RequestApprovedNotes { get; set; }



        /// <summary>
        /// This data is approved on
        /// </summary>
        [Display(Name = "Approved On"), Column("Approved_On")]
        DateTime? ApprovedOn { get; set; }

        /// <summary>
        /// This data is approved by 
        /// </summary>
        [Display(Name = "Approved By"), Column("Approved_By")]
        string ApprovedBy { get; set; }

        /// <summary>
        /// Approvable Token
        /// </summary>
        [Display(Name = "Approval Token"), Column("Approval_Token")]
        string ApprovableToken { get; set; }

        [Display(Name = "Approval Notes"), Column("Approval_Notes")]
        string ApprovalNotes { get; set; }

        /// <summary>
        /// This data is Reject on
        /// </summary>
        [Display(Name = "Reject On"), Column("Rejected_On")]
        DateTime? RejectedOn { get; set; }

        /// <summary>
        /// This data is Reject by 
        /// </summary>
        [Display(Name = "Reject By"), Column("Rejected_By")]
        string RejectedBy { get; set; }

        [Display(Name = "Reason of Rejection"), Column("Rejection_Notes")]
        string RejectedReason { get; set; }
    }
}
