using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace API.Filters
{
    public class LogSensitiveActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var apiAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            Debug.WriteLine($"Accessed to this route by this API address {apiAddress}");
        }
    }
}
