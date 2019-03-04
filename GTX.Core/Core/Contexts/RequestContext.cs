using System;
using System.Collections.Generic;


namespace GTX
{
    public class RequestContext : BaseRequestContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextId"></param>
        public RequestContext(Guid contextId) : base(contextId)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestContext()
            : this(Guid.NewGuid())
        {
        }

        
    }
}
