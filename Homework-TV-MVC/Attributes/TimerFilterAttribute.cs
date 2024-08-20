using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Homework_TV_MVC.Attributes
{
    public class TimerFilterAttribute:ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService <ILogger<TimerFilterAttribute>>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await next();
            stopwatch.Stop();
            logger.LogInformation($"Duration of implementation{stopwatch.ElapsedMilliseconds}");
        }
    }
}
