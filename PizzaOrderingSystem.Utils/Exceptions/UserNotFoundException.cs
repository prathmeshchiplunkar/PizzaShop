using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public class UserNotFoundException : BasePizzaSystemException
    {
        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, Dictionary<string, string> fieldDetails) : base(message, fieldDetails) { }

        public override string GetFormatedMessage()
        {
            return $"User not registerd : {ErrorMessage}";
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
