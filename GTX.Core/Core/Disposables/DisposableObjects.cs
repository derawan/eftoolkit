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

namespace System
{
    /// <summary>
    /// DisposableObjects
    /// Abstract classes for disposable objects
    /// </summary>
    public abstract class DisposableObjects : IDisposableObjects
    {
        private bool _isDisposed;
        /// <summary>
        /// Constructor
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            var args = new EventDynamicArgs();
            dynamic arg = args.Parameters;
            arg.ClassName = GetType().Name;
            OnBeforeDisposed(this, args);
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }
            OnAfterDisposed(this, args);
            _isDisposed = true;
        }

        /// <summary>
        /// override this
        /// </summary>
        public virtual void DisposeCore()
        {

        }

        /// <summary>
        /// Distructor
        /// </summary>
        ~DisposableObjects()
        {
            Dispose(false);
        }

        public event EventHandler<EventDynamicArgs> BeforeDisposed;

        /// <summary>
        /// OnBeforeDispose 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnBeforeDisposed(object sender, EventDynamicArgs args)
        {
            BeforeDisposed?.Invoke(this, args);
        }

        public event EventHandler<EventDynamicArgs> AfterDisposed;

        /// <summary>
        /// OnAfterDisposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnAfterDisposed(object sender, EventDynamicArgs args)
        {
            AfterDisposed?.Invoke(this, args);
        }

    }
}
