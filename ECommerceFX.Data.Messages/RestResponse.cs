using System.Net;

namespace ECommerceFX.Data.Messages
{
    public class RestResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }

        /// <summary>
        /// Constructor to work with RestSharp since it requires a parameterless constructor
        /// </summary>
        public RestResponse()
            :this(default(T)) { }

        public RestResponse(T data = default(T), HttpStatusCode statusCode = HttpStatusCode.OK, string message = null, bool isError = false)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            IsError = isError;
        }
    }
}