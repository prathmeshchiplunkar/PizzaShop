using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public class AuthenticationException : BasePizzaSystemException
    {
        public AuthenticationException(string message) : base(message) { }

        public AuthenticationException(string message, Dictionary<string, string> fieldDetails) : base(message, fieldDetails) { }

        public override string GetFormatedMessage()
        {
            return $"Authentication Error : {ErrorMessage}";
        }

        public override HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.Unauthorized;
        }
    }
}
