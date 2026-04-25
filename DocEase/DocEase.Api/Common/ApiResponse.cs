namespace DocEase.Api.Common
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the request was successful (true/false).
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// HTTP status code (e.g., 200, 400, 500).
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Generic payload data (can be any type).
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Optional message for additional context (errors, success notes).
        /// </summary>
        public string Message { get; set; }

        // Static helper methods for convenience
        public static ApiResponse<T> Success(T data, string message = "Request successful", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Status = true,
                StatusCode = statusCode,
                Data = data,
                Message = message
            };
        }

        public static ApiResponse<T> Fail(string message, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Status = false,
                StatusCode = statusCode,
                Data = default,
                Message = message
            };
        }
    }
}
