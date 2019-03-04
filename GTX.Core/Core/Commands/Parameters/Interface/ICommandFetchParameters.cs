using System;

namespace GTX.Commands
{

    public interface ICommandFetchParameters<TModel, TModelKey> : IBaseCommandParameters<TModel,TModelKey>, IQueryParameters // ICommandParameters<TModel> 
        where TModel : class
    {
        /// <summary>
        /// Query
        /// </summary>
        Func<TModel, bool> Query { get; set; }

        /// <summary>
        /// if you want to use string expression as query set this to true and fill predicate and predicateparameters
        /// </summary>
        bool UseStringParameters { get; set; }

        /// <summary>
        /// string expression as query
        /// </summary>
        string Predicate { get; set; }

        /// <summary>
        /// string expression parameters
        /// </summary>
        object[] PredicateParameters { get; set; }

        string OrderBy { get; set; }
    }

    
}
