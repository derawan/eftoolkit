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
    public interface IReviewable
    {
        /// <summary>
        /// This data review requested on
        /// </summary>
        [Display(Name = "Request Review On"), Column("Request_Review_On")]
        DateTime? RequestReviewOn { get; set; }

        /// <summary>
        /// This data review requested by
        /// </summary>
        [Display(Name = "Request Review By"), Column("Request_Review_By")]
        string RequestReviewBy { get; set; }

        /// <summary>
        /// This requester notes 
        /// </summary>
        [Display(Name = "Request Review Notes"), Column("Request_Review_Notes")]
        string RequestReviewNotes { get; set; }

        /// <summary>
        /// This data is Reviewed on
        /// </summary>
        [Display(Name = "Reviewed On"), Column("Reviewed_On")]
        DateTime? ReviewedOn { get; set; }

        /// <summary>
        /// This data is Reviewed by 
        /// </summary>
        [Display(Name = "Reviewed By"), Column("Reviewed_By")]
        string ReviewedBy { get; set; }

        /// <summary>
        /// Review Notes1
        /// </summary>
        [Display(Name = "Review Notes"), Column("Review_Notes")]
        string ReviewedNotes { get; set; }
    }
}
