using Ecomerce.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models.General;

namespace Ecomerce.Filters
{
    public class SessionUsuarioFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            MethodsHelper MethodsHep = new MethodsHelper();
            var AuthorizationH = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (AuthorizationH != null)
            {
                var MessageError = MethodsHep.ValidateTokenSesion(AuthorizationH);
                if (MessageError == null)
                {
                    await next();
                } else
                {
                    context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new GenericResponse<dynamic>
                    {
                        statusCode = 401,
                        message = MessageError,
                    }));
                }
            } else
            {
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new GenericResponse<dynamic>
                {
                    statusCode = 401,
                    message = MessageHelper.TokenSesionErrorNotParams,
                }));
            }
            
        }
    }
}
