using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (context.ModelState.IsValid) return;

            var errors = modelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).First())
                .Select(x => new { x.Key, x.Value })
                .ToList();

            context.Result = new BadRequestObjectResult(new { errors });
        }
    }
}
