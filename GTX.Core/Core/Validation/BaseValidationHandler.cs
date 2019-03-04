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
using System.Linq;
using GTX.Commands;

namespace GTX.Core
{
    public class BaseValidationHandler<TCommand> : IValidationHandler<TCommand> where TCommand : ICommand
    {
        public virtual IEnumerable<ValidationResult> Validate(TCommand command)
        {
            List<ValidationResult> validationResult = new List<ValidationResult>();
            //InitiateDefaultValue(command);
            if (command.IsNull())
            {
                validationResult.Add(new ValidationResult("Empty Command Parameter"));
            }
            else
            {
                // parameter data is missing
                if (command.Parameters.IsNull())
                {
                    validationResult.Add(new ValidationResult("Empty Command Parameter Data"));
                }
                else
                {
                        var context = new ValidationContext(command.Parameters);
                        var res = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

                        Validator.TryValidateObject(command.Parameters, context, res);

                        validationResult = (
                                from item in res
                                from sitem in item.MemberNames
                                select new ValidationResult(sitem, item.ErrorMessage)
                                ).ToList();
                }
            }
            return validationResult;
        }

    }
}