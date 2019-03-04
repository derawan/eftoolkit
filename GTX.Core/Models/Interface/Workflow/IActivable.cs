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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTX.Models
{

    public interface IActivable
    {
        /// <summary>
        /// This data activation requested on
        /// </summary>
        [Display(Name = "Request Activation On"), Column("Request_Activation_On")]
        DateTime? RequestActivationOn { get; set; }

        /// <summary>
        /// This data activation requested by
        /// </summary>
        [Display(Name = "Request Activation By"), Column("Request_Activation_By")]
        string RequestActivationBy { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Activation Notes"), Column("Request_Activation_Notes")]
        string RequestActivationNotes { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Activation Token"), Column("Request_Activation_Token")]
        string RequestActivationToken { get; set; }

        /// <summary>
        /// This data is Activated on
        /// </summary>
        [Display(Name = "Activated On"), Column("Activated_On")]
        DateTime? ActivatedOn { get; set; }

        /// <summary>
        /// This data is Activated by 
        /// </summary>
        [Display(Name = "Activated By"), Column("Activated_By")]
        string ActivatedBy { get; set; }

        /// <summary>
        /// Activation Token
        /// </summary>
        [Display(Name = "Activation Token"), Column("Activation_Token")]
        string ActivationToken { get; set; }

        [Display(Name = "Activation Notes"), Column("Activation_Notes")]
        string ActivationNotes { get; set; }

        /// <summary>
        /// This data activation requested on
        /// </summary>
        [Display(Name = "Request De-Activation On"), Column("Request_DeActivation_On")]
        DateTime? RequestDeactivationOn { get; set; }

        /// <summary>
        /// This data activation requested by
        /// </summary>
        [Display(Name = "Request De-Activation By"), Column("Request_DeActivation_By")]
        string RequestDeactivationBy { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request De-Activation Notes"), Column("Request_DeActivation_Notes")]
        string RequestDeactivationNotes { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request De-Activation Token"), Column("Request_DeActivation_Token")]
        string RequestDeactivationToken { get; set; } 
        
        /// <summary>
                                                           /// This data is DeActivated on
                                                           /// </summary>
        [Display(Name = "DeActivated On"), Column("Deactivated_On")]
        DateTime? DeActivatedOn { get; set; }

        /// <summary>
        /// This data is DeActivated by 
        /// </summary>
        [Display(Name = "DeActivate By"), Column("Deactivated_By")]
        string DeActivatedBy { get; set; }

    }

    

}
