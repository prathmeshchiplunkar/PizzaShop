using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Utils.Exceptions
{
    public interface IPizzaSystemException
    {
        string ErrorMessage { get; }
        HttpStatusCode HttpStatus { get; }
        public ErrorType ErrorType { get;  }
        public ErrorLevelType ErrorLevelType { get;  }
        public Dictionary<string, string> FieldDetails { get; }

        public string GetFormatedMessage();

    }
}
