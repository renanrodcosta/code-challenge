using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Exceptions;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Filters
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            if (context.Exception is BusinessException)
            {
                context.Result = new JsonResult(new { errorMessage = context.Exception.Message });
            }
            else
            {
                context.Result = new JsonResult(new { errorMessage = "Ops, process fail! :(" });
            }
            base.OnException(context);
        }
    }
}