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
using System.IO;

namespace GTX
{
    public interface IContext
    {
        /// <summary>
        /// Context Id
        /// </summary>
        Guid ContextId { get; set; }

        /// <summary>
        /// On Exception Event Handler
        /// </summary>
        event EventHandler<ErrorEventArgs> OnExceptionHandler;


        event EventHandler<EventDynamicArgs> OnInits;

        /// <summary>
        /// On Exception Event Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="methodName"></param>
        /// <param name="error"></param>
        void OnExceptionRaised(object sender, string methodName, Exception error);

        //void OnInitialize(object sender, EventDynamicArgs args);

        void Initialize();
    }
}
