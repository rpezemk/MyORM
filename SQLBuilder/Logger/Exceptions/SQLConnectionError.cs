using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder.Logger.Exceptions
{
    public  class SQLConnectionError : Exception
    {
        public SQLConnectionError() { }
        public SQLConnectionError(string message) : base(message) { }
        public SQLConnectionError(string message, Exception inner) : base(message, inner) { }
    }

    public class QueryEmptyError : Exception
    {
        public QueryEmptyError() { }
        public QueryEmptyError(string message) : base(message) { }
        public QueryEmptyError(string message, Exception inner) : base(message, inner) { }
    }

    public class QuerySyntaxError : Exception
    {
        public QuerySyntaxError() { }
        public QuerySyntaxError(string message) : base(message) { }
        public QuerySyntaxError(string message, Exception inner) : base(message, inner) { }
    }

    public class QueryParametersError : Exception
    {
        public QueryParametersError() { }
        public QueryParametersError(string message) : base(message) { }
        public QueryParametersError(string message, Exception inner) : base(message, inner) { }
    }



}
