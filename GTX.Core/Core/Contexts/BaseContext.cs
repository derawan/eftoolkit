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
    public abstract class BaseContext : 
        DisposableObjects, 
        IContext
    {
        private Guid _contextId = Guid.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseContext(Guid contextId)
        {
            ContextId = contextId;
            OnInits += BaseContext_OnInit;
            Initialize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseContext()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Context Id
        /// </summary>
        public virtual Guid ContextId
        {
            get { return _contextId; }
            set { _contextId = value; }
        }

        public event EventHandler<EventDynamicArgs> OnInits;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void BaseContext_OnInit(object sender, EventDynamicArgs e)
        {
            
        }

        protected virtual void OnInitialize(object sender, EventDynamicArgs args)
        {
            OnInits?.Invoke(this, args);
        }

        public virtual void Initialize()
        {
            var args = new EventDynamicArgs();
            OnInitialize(this,args);
        }

        /// <summary>
        /// On Exception Event Handler
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnExceptionHandler;


        /// <summary>
        /// On Exception Event Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="methodName"></param>
        /// <param name="error"></param>
        public virtual void OnExceptionRaised(object sender, string methodName, Exception error)
        {
            OnExceptionHandler?.Invoke(this, new ErrorEventArgs(error));
            var context = sender as IContext;
            Bootstrapper.BootStrapManager.LogError(sender, methodName, error);
        }

        

    }
}
