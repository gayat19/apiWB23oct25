using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstAPI.Misc
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message
            };
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(response)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
