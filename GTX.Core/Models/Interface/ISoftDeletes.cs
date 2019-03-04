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
    public interface ISoftDeletes
    {
        /// <summary>
        /// This data is soft deleted on : See CommandTypes : Remove
        /// </summary>
        [Display(Name = "Deleted On"), Column("Deleted_On")]
        DateTime? DeletedOn { get; set; }

        /// <summary>
        /// This data is soft deleted by : See CommandTypes : Remove
        /// </summary>
        [Display(Name = "Deleted By"), Column("Deleted_By")]
        string DeletedBy { get; set; }

        [Display(Name = "Reason of Deletion"), Column("Deletion_Notes")]
        string DeletionReason { get; set; }

        /// <summary>
        /// This data is restored on : See CommandTypes : Restore
        /// </summary>
        [Display(Name = "Restored On"), Column("Restored_On")]
        DateTime? RestoredOn { get; set; }

        /// <summary>
        /// This data is restored by : See CommandTypes : Restore
        /// </summary>
        [Display(Name = "Restored By"), Column("Restored_OBy")]
        string RestoredBy { get; set; }

        [Display(Name = "Notes Of Data Restored"), Column("Restore_Notes")]
        string RestoreNotes { get; set; }

        [Display(Name = "Status when soft Deleted"), Column("Previous_Status")]
        string PrevStatus { get; set; }
    }
}
