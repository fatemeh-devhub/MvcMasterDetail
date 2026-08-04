using System.Net;

namespace MasterDetailSample01.ResponseFrameworks.Contracts
{
    public interface IResponse<T>
    {
        bool IsSuccessful { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string? Message { get; set; }
        List<string>? Errors { get; set; }
        T? Value { get; set; }
    }
}
