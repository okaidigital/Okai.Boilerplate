using System.Net;

namespace Okai.Boilerplate.Application.DTOs.Base
{
    public class SuccessResponseDto : BaseResponseDto
    {
        public override bool Succeeded { get; }

        public SuccessResponseDto(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Succeeded = true;
        }
        public SuccessResponseDto(HttpStatusCode statusCode) : base(statusCode)
        {

        }

        public static SuccessResponseDto? Create(HttpStatusCode statusCode, object? data)
        {
            var successResponseType = typeof(SuccessResponseDto<>).MakeGenericType(data?.GetType() ?? throw new InvalidOperationException());
            var successResponseDto = Activator.CreateInstance(successResponseType, statusCode, data);
            return (SuccessResponseDto?)successResponseDto;
        }
    }

    public class SuccessResponseDto<TResponseDto> : SuccessResponseDto where TResponseDto : class
    {
        public TResponseDto? Data { get; set; }

        public SuccessResponseDto(HttpStatusCode statusCode) : base(statusCode)
        {

        }

        public SuccessResponseDto(HttpStatusCode statusCode, string message, TResponseDto? data) : base(statusCode, message)
        {
            Data = data;
        }

        public SuccessResponseDto(HttpStatusCode statusCode, TResponseDto? data) : base(statusCode, string.Empty)
        {
            Data = data;
        }

        public static SuccessResponseDto? Create(Type responseType, HttpStatusCode statusCode, string message, object? data)
        {
            var successResponseType = typeof(SuccessResponseDto<>).MakeGenericType(responseType);
            var successResponseDto = Activator.CreateInstance(successResponseType, statusCode, message, data) as SuccessResponseDto;
            return successResponseDto;
        }

        public static SuccessResponseDto? Create(Type responseType, HttpStatusCode statusCode, object? data)
        {
            var successResponseType = typeof(SuccessResponseDto<>).MakeGenericType(responseType);
            var successResponseDto = Activator.CreateInstance(successResponseType, statusCode, data) as SuccessResponseDto;
            return successResponseDto;
        }

        public static Type GetGenericType(Type type)
        {
            return typeof(SuccessResponseDto<>).MakeGenericType(type);
        }

    }
}
