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
    public interface IConfirmable
    {
        /// <summary>
        /// This data Confirmation requested on
        /// </summary>
        [Display(Name = "Request Confirmation On"), Column("Request_Confirmation_On")]
        DateTime? RequestConfirmationOn { get; set; }

        /// <summary>
        /// This data Confirmation requested by
        /// </summary>
        [Display(Name = "Request Confirmation By"), Column("Request_Confirmation_By")]
        string RequestConfirmationBy { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Confirmation Notes"), Column("Request_Confirmation_Notes")]
        string RequestConfirmationNotes { get; set; }

        /// <summary>
        /// This data is Confirmed on
        /// </summary>
        [Display(Name = "Confirmed On"), Column("Confirmed_On")]
        DateTime? ConfirmedOn { get; set; }

        /// <summary>
        /// This data is Confirmed by 
        /// </summary>
        [Display(Name = "Confirmed By"), Column("Confirmed_By")]
        string ConfirmedBy { get; set; }

        /// <summary>
        /// This data is Registration Token
        /// </summary>
        [Display(Name = "Confirmation Token"), Column("Confirmation_Token")]
        string ConfirmationToken { get; set; }

    }
}
