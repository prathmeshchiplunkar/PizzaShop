using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public abstract class BasePizzaSystemException : ApplicationException, IPizzaSystemException
    {
        public string ErrorMessage { get; protected set; }

        public ErrorType ErrorType { get { return GetErrorType(); } }

        public HttpStatusCode HttpStatus { get { return GetHttpStatusCode(); } }

        public ErrorLevelType ErrorLevelType { get { return GetErrorLevelType(); } }
        public Dictionary<string, string> FieldDetails { get; protected set; }

        public virtual ErrorType GetErrorType()
        {
            return ErrorType.Technical;
        }

        public virtual HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.InternalServerError;
        }

        public virtual ErrorLevelType GetErrorLevelType()
        {
            return ErrorLevelType.Critical;
        }    
        public virtual string GetFormatedMessage()
        {
            return $"Pipeline failed due to : {ErrorMessage}";
        }

        protected BasePizzaSystemException(string message):base(message) 
        {
            this.ErrorMessage = message;
        }

        protected BasePizzaSystemException(string message, Dictionary<string, string> fieldDetails) : base(message)
        {
            this.ErrorMessage = message;
            this.FieldDetails = fieldDetails;
        }
    }
}

public enum ErrorType
{
    Technical = 0,
    Business = 1
}

public enum ErrorLevelType
{
    Information = 0,
    Critical = 1
}
