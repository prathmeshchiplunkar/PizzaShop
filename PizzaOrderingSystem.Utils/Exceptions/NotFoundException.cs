using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public class NotFoundException : BasePizzaSystemException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Dictionary<string, string> fieldDetails) : base(message, fieldDetails) { }

        public override string GetFormatedMessage()
        {
            return $"Data Not found : {ErrorMessage}";
        }

        public override ErrorType GetErrorType()
        {
            return ErrorType.Business;
        }

        public override HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}