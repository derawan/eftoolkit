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

namespace GTX.Bootstrapper
{
    public abstract class BaseBootstrap : BaseContext, IBootstrap
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public BaseBootstrap(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseBootstrap()
            : this(Guid.NewGuid())
        {
        }

        public virtual T Resolve<T>()
        {
            return default(T);
        }

        public virtual Object Resolve(Type param)
        {
            return null;
        }

        private IApplicationContext applicationContext; // = new BaseApplicationContext();

        public virtual IApplicationContext ApplicationContext {
            get {
                if (applicationContext.IsNull())
                    applicationContext = Resolve<IApplicationContext>();
                return applicationContext;
            }
            protected set
            {
                applicationContext = value;
            }
        }

        public virtual IAuthenticationContext AuthenticationContext
        {
            get { return Resolve<IAuthenticationContext>();  }
        }

        public virtual IRequestContext RequestContext
        {
            get { return Resolve<IRequestContext>();  }
        }

        public virtual void RegisterTypes() { }
    }
}




