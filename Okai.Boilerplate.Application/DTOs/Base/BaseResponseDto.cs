using System.Net;

namespace Okai.Boilerplate.Application.DTOs.Base
{
    public abstract class BaseResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime ResponseTime { get; set; }
        public abstract bool Succeeded { get; }

        protected BaseResponseDto(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            ResponseTime = DateTime.Now;
        }

        protected BaseResponseDto(HttpStatusCode statusCode,
                                  string message)
        {
            StatusCode = statusCode;
            Message = message;
            ResponseTime = DateTime.Now;
        }
    }
}
