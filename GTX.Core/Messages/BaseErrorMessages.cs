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

namespace GTX
{
    /// <summary>
    /// Base Error Messages Class Templates
    /// </summary>
    public class BaseErrorMessages : BaseMessages, IErrorMessage
    {
        private Exception exception; 
        public BaseErrorMessages(Exception except, long? errorCode = GlobalErrorCode.DefaultSystemError) : 
            base(MessageType.Error, except.Message)
        {
            exception = except;
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Hold The Last Exception from current operations
        /// </summary>
        public Exception Exception {
            get { return exception; }
            set {
                exception = value;
                if (value != null)
                {
                    Text = exception.Message;
                    if (!ErrorCode.HasValue)
                    {
                        ErrorCode = 0x3B9AC9FF;
                    }
                }
            }
        }

        /// <summary>
        /// Numeric Error Code For Error Messages for internal purpose Error Code Constant
        /// ref Constant.GlobalErrorCode
        /// </summary>
        public long? ErrorCode
        {
            get;
            set;
        }

        
    }
}
