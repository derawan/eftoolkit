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
    public enum CommandTypes
    {
        #region CRUD
        /// <summary>
        /// Add an entity into the database *
        /// </summary>
        Add,

        /// <summary>
        /// Update existing entity within the database *
        /// </summary>
        Edit,

        /// <summary>
        /// Delete an existing entity within the database *
        /// </summary>
        Delete,

        /// <summary>
        /// Do Soft Deletion for an existing entity within the database *
        /// </summary>
        Remove,

        /// <summary>
        /// Restore a soft deleted entity in a database *
        /// </summary>
        Restore,
        #endregion

        /// <summary>
        /// Fetch a/some entity data from the database *
        /// </summary>
        Fetch,

        #region Approval, Review, Reject, Publish, Activate, Register

        RequestApproval,

        /// <summary>
        /// Approve a process which are in waiting for approval state
        /// </summary>
        Approve,

        /// <summary>
        /// Reject a process which are in waiting for approval state
        /// </summary>
        Reject,

        RequestReview,
        /// <summary>
        /// Give a review for a process which are need a review
        /// </summary>
        Review,

        /// <summary>
        /// Publish an entity
        /// </summary>
        Publish,

        /// <summary>
        /// Unpublish an entity
        /// </summary>
        Unpublish,

        RequestActivation,

        /// <summary>
        /// activate status
        /// </summary>
        Activate,

        RequestDeactivation,

        /// <summary>
        /// Deactivate Status
        /// </summary>
        Deactivate,

        RequestConfirmation,
        /// <summary>
        /// Confirm
        /// </summary>
        Confirm,

        /// <summary>
        /// Register
        /// </summary>
        Register,

        /// <summary>
        /// Un-Register
        /// </summary>
        Unregister,
        #endregion

        /// <summary>
        /// Commit changes into the database
        /// </summary>
        Commit,

        /// <summary>
        /// Login into a system
        /// </summary>
        Login,

        /// <summary>
        /// Logout from system
        /// </summary>
        Logout,
        
        /// <summary>
        /// Download something or Print something
        /// </summary>
        Download,

        /// <summary>
        /// Upload something
        /// </summary>
        Upload,

        /// <summary>
        /// Unknown Process
        /// </summary>
        Unknown,


        System

    }

}
