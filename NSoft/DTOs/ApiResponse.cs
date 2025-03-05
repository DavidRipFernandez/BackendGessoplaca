using System.Net.NetworkInformation;

namespace NSoft.DTOs
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; } //mensaje para el usuario
        public string? TechnicalMessage { get; set; } // mensaje tecnico 
        public T Data { get; set; } 
        public int StatusCode { get; set; } //respuesta http
        public bool Success { get; set; } //true or false

        public ApiResponse(string message, string technicalMessage, T data, int statusCode, bool success)
        {
            Message = message;
            TechnicalMessage = technicalMessage;
            Data = data;
            StatusCode = statusCode;
            Success = success;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message)
        {
            string finalMessage = string.IsNullOrEmpty(message) ? "Operación Exitosa" : message;
            return new ApiResponse<T>(finalMessage, "", data, 200, true);
        }

        public static ApiResponse<T> ErrorResponse(string message, string technicalMessage, int statusCode)
        {
            return new ApiResponse<T>(message, technicalMessage, default, statusCode,false);
        }

    }
}
