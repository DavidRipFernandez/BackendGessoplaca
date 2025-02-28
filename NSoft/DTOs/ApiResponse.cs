namespace NSoft.DTOs
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public string TechnicalMessage { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }

        public ApiResponse(string message, string technicalMessage, T data, int statusCode, bool success)
        {
            Message = message;
            TechnicalMessage = technicalMessage;
            Data = data;
            StatusCode = statusCode;
            Success = success;
        }

    }
}
