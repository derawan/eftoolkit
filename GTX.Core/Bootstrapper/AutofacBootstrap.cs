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
using Autofac;
using Autofac.Configuration;

namespace GTX.Bootstrapper
{
    public class AutofacBootstrap : BaseBootstrap
    {
        protected ContainerBuilder Builder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public AutofacBootstrap(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AutofacBootstrap()
            : this(Guid.NewGuid())
        {
        }

        public virtual IContainer DependencyContainer
        {
            get;
            protected set;
        }


        protected virtual void InitBuilder()
        {
            try
            {
                Builder = new ContainerBuilder();
                /*
                 * this only works for Autofac 4.0.0 up
                 * var config = new ConfigurationBuilder();
                 * config.AddJsonFile("autofac.json");
                 * var module = new ConfigurationModule(config.Build());
                 * Builder.RegisterModule(module);
                 */
                // Obsolete
                Builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "InitBuilder", error);
                throw;
            }
        }

        public override  void RegisterTypes() { }

        protected virtual void FinalizeBuilder()
        {
            try
            {
                if (Builder.IsNull()) return;
                DependencyContainer = Builder.Build();
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "FinalizeBuilder", error);
                throw;
            }
        }

        public override void Initialize()
        {
            try
            {
                InitBuilder();
                RegisterTypes();
                FinalizeBuilder();
            }
            catch (Exception error)
            {
                OnExceptionRaised(this, "Initialize", error);
                throw;
            }
            base.Initialize();
        }

        public override T Resolve<T>()
        {
            Type tipe = typeof(T);
            if (tipe.IsAbstract && tipe.IsSealed)
            {
                return default(T);
            }
            else
            {
                if (DependencyContainer == null)
                {
                    try
                    {
                        return Activator.CreateInstance<T>();
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                else
                    return DependencyContainer.IsRegistered<T>() ? DependencyContainer.Resolve<T>() : default(T);
            }
        }

        public override object Resolve(Type param)
        {
            if (param.IsAbstract && param.IsSealed)
            {
                return null;
            }
            else
            {
                if (DependencyContainer == null)
                {
                    try
                    {
                        return Activator.CreateInstance(param);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                    return DependencyContainer.IsRegistered(param) ? DependencyContainer.Resolve(param) : null;
            }
        }
    }
}




