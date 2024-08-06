namespace backend.Models
{
    public class CommonModel
    {
        public class ResultApi
        {
            public int nStatusCode { get; set; } = StatusCodes.Status200OK;
            public string? sMessage { get; set; }
            public object? objResult { get; set; }
        }
    }
}
