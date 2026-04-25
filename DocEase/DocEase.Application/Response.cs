namespace DocEase.Application
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public static Response<T> Error(string message)
        {
            return new Response<T>
            {
                Status = false,
                Data = default,
                Message = message
            };
        }
        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Status = true,
                Data = data,
                Message = string.Empty
            };
        }
    }
}
