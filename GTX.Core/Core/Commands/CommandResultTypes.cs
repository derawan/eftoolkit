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

namespace GTX
{
    public enum CommandResultTypes
    {
        #region CRUD
        /// <summary>
        /// Add - Inserted
        /// </summary>
        Inserted,

        /// <summary>
        /// Edit - Updated
        /// </summary>
        Updated,

        /// <summary>
        /// Delete - Deleted
        /// </summary>
        Deleted,

        /// <summary>
        /// Remove - Removed
        /// </summary>
        Removed,

        /// <summary>
        /// Restore - Restored
        /// </summary>
        Restored,
        #endregion


        #region Approval

        /// <summary>
        /// RequestApproval - Waiting4Approval
        /// </summary>
        Waiting4Approval,

        /// <summary>
        /// Approve - Approved
        /// </summary>
        Approved,

        /// <summary>
        /// Reject - Rejected
        /// </summary>
        Rejected,

        /// <summary>
        /// RequestReview - WaitingAReview
        /// </summary>
        WaitingAReview,

        /// <summary>
        /// Review - Reviewed
        /// </summary>
        Reviewed,

        /// <summary>
        /// Publish - Published
        /// </summary>
        Published,

        /// <summary>
        /// Unpublish - Unpublished
        /// </summary>
        Unpublished,

        /// <summary>
        /// RequestActivation - PendingActivation
        /// </summary>
        PendingActivation,

        /// <summary>
        /// activate - Activated
        /// </summary>
        Activated,

        /// <summary>
        /// Deactivate - Deactivated
        /// </summary>
        Deactivated,

        /// <summary>
        /// RequestConfirmation - Waiting4Confirmation
        /// </summary>
        Waiting4Confirmation,

        /// <summary>
        /// Confirm - Confirmed
        /// </summary>
        Confirmed,

        /// <summary>
        /// Register - Registered
        /// </summary>
        Registered,

        /// <summary>
        /// UnRegister - UnRegistered
        /// </summary>
        Unregistered,
        #endregion

        /// <summary>
        /// Add/Edit/Delete/Remove/Restore/Approve/Review/Reject/Publish/Unpublish/Activate/Deactivate/Confirm/Register/Unregister - Queued
        /// </summary>
        Queued,

        /// <summary>
        /// Add/Edit/Delete/Remove/Restore/Approve/Review/Reject/Publish/Unpublish/Activate/Deactivate/Confirm/Register/Unregister - Pending
        /// </summary>
        Pending,

        /// <summary>
        /// Fetch - Fetched
        /// </summary>
        Fetched,

        /// <summary>
        /// Commit - Commited
        /// </summary>
        Commited,

        /// <summary>
        /// Login
        /// </summary>
        Login,

        /// <summary>
        /// Logout
        /// </summary>
        Logout,

        /// <summary>
        /// Error
        /// </summary>
        Error,

        Executing,

        Downloaded,

        Uploaded,

        Executed



    }

}
