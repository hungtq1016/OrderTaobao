
using BaseSource.Builder;
using BaseSource.Dto;

namespace BaseSource.BackendAPI.Services
{
    public class ResponseHelper
    {
        public static Response<T> CreateSuccessResponse<T>(T data)
        {
            return new ResponseBuilder<T>(data).With200().Build();
        }

        public static Response<T> CreateCreatedResponse<T>(T data)
        {
            return new ResponseBuilder<T>(data).With201().Build();
        }

        public static Response<T> CreateErrorResponse<T>(UInt16 statusCode, string message)
        {
            return new ResponseBuilder<T>().WithStatusCode(statusCode).WithMessage(message).WithError().Build();
        }
    }
}
