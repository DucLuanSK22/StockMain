using Stock.BE.Core.ESPException;
using Stock.BE.Core.Resource;

namespace Stock.BE.Host.Middleware
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Khi một hàm đi qua middleware nó phải đấy sang hàm tiếp theo, nên cần requestdelegate _next;
        /// </summary>
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        /// <summary>
        /// hàm xử lý lỗi khi đi quan middleware
        /// </summary>
        /// <param name="context">context của http</param>
        /// <returns></returns>
        /// Created by: nttue (12/07/2023)
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //nếu đi qua hàm _next mà bị lỗi thì nó nhảy vào catch để bắt Exception
                await _next(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Hàm xử lý các exception
        /// </summary>
        /// <param name="context">context của http</param>
        /// <param name="exception">ngoại lệ trả về.</param>
        /// <returns>Trả về ngoại lệ</returns>
        /// Created by: nttue (12/07/2023)
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Ban đầu contentType = null sau đó mình gán lại thành application/json.
            context.Response.ContentType = "application/json";
            Console.WriteLine(exception);

            BaseException baseException = null;
            int statusCode;

            switch (exception)
            {
                // Trường hợp không tìm thấy tài nguyên của hệ thống thì trả về exception này
                case NotFoundException notFoundException:
                    baseException = new BaseException()
                    {
                        ErrorCode = 1,
                        UserMessage = Resource.ExceptionNotFoundDefault,
                        DevMessage = Resource.ExceptionNotFoundDefault,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = notFoundException.HelpLink
                    };
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                // Trường hợp liên quan đến auth 
                case AuthException authException:
                    baseException = new BaseException()
                    {
                        ErrorCode = 5,
                        UserMessage = authException.Message ?? Resource.ValidateUserInputError,
                        DevMessage = authException.Message ?? Resource.ValidateUserInputError,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = authException.HelpLink
                    };
                    statusCode = StatusCodes.Status409Conflict;
                    break;
                // Trường hợp liên quan đến validate khi thêm entity
                case ValidateException validateException:
                    baseException = new BaseException()
                    {
                        ErrorCode = 2,
                        UserMessage = validateException.Message ?? Resource.ValidateUserInputError,
                        DevMessage = validateException.Message ?? Resource.ValidateUserInputError,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = validateException.HelpLink
                    };
                    statusCode = StatusCodes.Status409Conflict;
                    break;
                // Trường hợp bị conflic tài nguyên
                case ConflictException conflictException:
                    baseException = new BaseException()
                    {
                        ErrorCode = 3,
                        UserMessage = conflictException.Message ?? Resource.ValidateUserInputError,
                        DevMessage = conflictException.Message ?? Resource.ValidateUserInputError,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = conflictException.HelpLink
                    };
                    statusCode = StatusCodes.Status409Conflict;
                    break;

                default:
                    baseException = new BaseException()
                    {
                        ErrorCode = 4,
                        UserMessage = Resource.ExceptionSystemDefault,
                        DevMessage = Resource.ExceptionSystemDefault,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink
                    };
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(baseException);
        }

    }
}
