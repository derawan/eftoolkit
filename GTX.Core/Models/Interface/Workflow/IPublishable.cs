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
    public interface IPublishable
    {
        /// <summary>
        /// This data is Published on
        /// </summary>
        [Display(Name = "Published On"), Column("Published_On")]
        DateTime? PublishedOn { get; set; }

        /// <summary>
        /// This data is Published by 
        /// </summary>
        [Display(Name = "Published By"), Column("Published_By")]
        string PublishedBy { get; set; }

        /// <summary>
        /// This data is Unpublished on
        /// </summary>
        [Display(Name = "UnPublished On"), Column("UnPublished_On")]
        DateTime? UnpublishedOn { get; set; }

        /// <summary>
        /// This data is Unpublished by 
        /// </summary>
        [Display(Name = "UnPublished By"), Column("Unpublished_By")]
        string UnpublishedBy { get; set; }

    }
}
