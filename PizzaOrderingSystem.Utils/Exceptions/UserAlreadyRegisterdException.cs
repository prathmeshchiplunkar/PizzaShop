using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public class UserAlreadyRegisterdException : BasePizzaSystemException
    {
        public UserAlreadyRegisterdException(string message) : base(message) { }

        public UserAlreadyRegisterdException(string message, Dictionary<string, string> fieldDetails) : base(message, fieldDetails) { }

        public override string GetFormatedMessage()
        {
            return $"User already registerd : {ErrorMessage}";
        }

        public override HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.NotFound;
        }

        public override ErrorType GetErrorType()
        {
            return ErrorType.Business;
        }
    }
}
