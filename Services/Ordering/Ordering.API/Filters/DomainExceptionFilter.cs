using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Ordering.Domain;
using Ordering.Infrastructure;
using System.Net;

namespace Ordering.API.Filters
{
    public class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException ||
                context.Exception is ApplicationException ||
                context.Exception is InfrastructureException)
            {
                string json = JsonConvert.SerializeObject(context.Exception.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
