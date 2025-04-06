namespace Core.Application.DTOs
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public GenericResponse(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
