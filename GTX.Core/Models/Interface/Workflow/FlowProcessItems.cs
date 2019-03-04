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

namespace GTX.Models
{
    public class FlowProcessItems : BaseEntityModel<string>, 
        IRegisterable, 
        IApprovable, 
        //IActivable,
        IConfirmable,
        IReviewable,
        IPublishable

    {
        /*Should be connected to flow process definitions id*/
        public string FlowGroupId { get; set; }
        public string FlowStepId { get; set; }
        public string PrevFlowId { get; set; }
        public string NextFlowId { get; set; }


        public virtual DateTime? RegisteredOn { get; set; }
        public virtual string RegisteredBy { get; set; }
        public virtual string RegistrationToken { get; set; }
        public virtual DateTime? UnregisteredOn { get; set; }
        public virtual string UnregisteredBy { get; set; }

        public virtual DateTime? RequestApprovedOn { get; set; }
        public virtual string RequestApprovedBy { get; set; }
        public virtual string RequestApprovedNotes { get; set; }
        public virtual DateTime? ApprovedOn { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovableToken { get; set; }
        public virtual string ApprovalNotes { get; set; }
        public virtual DateTime? RejectedOn { get; set; }
        public virtual string RejectedBy { get; set; }
        public virtual string RejectedReason { get; set; }

        public virtual DateTime? RequestActivationOn { get; set; }
        public virtual string RequestActivationBy { get; set; }
        public virtual string RequestActivationNotes { get; set; }
        public virtual DateTime? ActivatedOn { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivationToken { get; set; }
        public virtual string ActivationNotes { get; set; }
        public virtual DateTime? DeActivatedOn { get; set; }
        public virtual string DeActivatedBy { get; set; }

        public virtual DateTime? RequestConfirmationOn { get; set; }
        public virtual string RequestConfirmationBy { get; set; }
        public virtual string RequestConfirmationNotes { get; set; }
        public virtual DateTime? ConfirmedOn { get; set; }
        public virtual string ConfirmedBy { get; set; }
        public virtual string ConfirmationToken { get; set; }

        public virtual DateTime? RequestReviewOn { get; set; }
        public virtual string RequestReviewBy { get; set; }
        public virtual string RequestReviewNotes { get; set; }
        public virtual DateTime? ReviewedOn { get; set; }
        public virtual string ReviewedBy { get; set; }
        public virtual string ReviewedNotes { get; set; }
        public virtual DateTime? PublishedOn { get; set; }
        public virtual string PublishedBy { get; set; }
        public virtual DateTime? UnpublishedOn { get; set; }
        public virtual string UnpublishedBy { get; set; }

    }

    

}
