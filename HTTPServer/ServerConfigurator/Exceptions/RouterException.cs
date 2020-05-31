using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.ServerConfigurator.Exceptions
{
    public class RouterException : Exception
    {
        public RouterException(): base("Router can not find such route")
        {}
    }
}
