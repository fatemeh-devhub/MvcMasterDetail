using System.Net;
using MasterDetailSample01.ResponseFrameworks.Contracts;

namespace MasterDetailSample01.ResponseFrameworks
{
    public class Response<T> : IResponse<T>
    {
        public Response()
        {
        }

        public Response(bool isSuccessful, HttpStatusCode statusCode, string? message, T? value)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Message = message;
            Value = value;
        }

        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public T? Value { get; set; }
    }
}

