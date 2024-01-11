namespace PokeApi.Responses
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public APIResponse() { }

        public APIResponse(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }
    }

    public class APIResponseController<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
