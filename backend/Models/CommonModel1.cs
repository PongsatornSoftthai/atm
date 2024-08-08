using System.Text.Json;

namespace backend.Models
{
    public class CommonModel1
    {
        public class ResultApi
        {
            public int nStatusCode { get; set; } = StatusCodes.Status200OK;
            public string? sMessage { get; set; }
            public object? objResult { get; set; }
        }
        public class ErrorDetails
        {
            /// <summary>
            /// </summary>
            public int StatusCode { get; set; }
            /// <summary>
            /// </summary>
            public string? Message { get; set; }
            /// <summary>
            /// </summary>
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}
