namespace _02.Comparendo.Core.Aplicacion.Utils
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

        public Response()
        {
            Message = null;
        }
        public Response(string message)
        {
            Message = message;
        }

        public Response(T data, string? message)
        {
            Success = data != null;
            Message = message;
            Data = data;
        }
    }
}