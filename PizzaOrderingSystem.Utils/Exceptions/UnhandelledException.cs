using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public class UnhandelledException : BasePizzaSystemException
    {
        public UnhandelledException(string message) : base(message) { }

        public UnhandelledException(string message, Dictionary<string, string> fieldDetails) : base(message, fieldDetails) { }

        public override string GetFormatedMessage()
        {
            return $"Unhandelled Error : {ErrorMessage}";
        }

        public override HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}