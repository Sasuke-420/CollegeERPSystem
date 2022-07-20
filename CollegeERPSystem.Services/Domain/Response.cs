namespace CollegeERPSystem.Services.Domain
{
    public class Response
    {
        public Response(string? message, bool isSuccess, int statusCode, IEnumerable<string>? errors,Object? content=null)
        {
            Message = message;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Errors = errors;
            Content = content;
        }

        public string? Message { get; }

        public bool IsSuccess { get; }
        public Object? Content { get; }

        public int StatusCode { get;}

        public IEnumerable<string>? Errors { get;}

    }
}
