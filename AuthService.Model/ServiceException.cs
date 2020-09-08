using System;
using System.Net;

namespace AuthService.Model
{
    public class ServiceException : Exception
    {
        public ServiceException(string message, HttpStatusCode code)
            : base(message)
        {
            StatusCode = code;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
