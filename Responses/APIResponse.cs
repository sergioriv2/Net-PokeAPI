namespace PokeApi.Responses
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
        public List<APIResponseError> Errors { get; set; } = new List<APIResponseError>();

        public APIResponse() { }

        public APIResponse(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }
    }

    public class APIResponseError
    {
        public string? Property { get; set; }
        public object? Constraints { get; set; }

        public APIResponseError()
        {
            
        }
    }

    public class APIResponseController<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
        public List<APIResponseError> Errors { get; set; } = new List<APIResponseError>();
    }
}
