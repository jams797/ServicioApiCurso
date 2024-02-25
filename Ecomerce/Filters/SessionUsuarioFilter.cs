using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecomerce.Filters
{
    public class SessionUsuarioFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            Console.WriteLine("Hola");
            await next();
            Console.WriteLine("Adios");
        }
    }
}
