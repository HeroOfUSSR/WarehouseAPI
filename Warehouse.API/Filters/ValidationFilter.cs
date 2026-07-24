using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warehouse.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator is null)
                    continue; // для этого типа нет валидатора — пропускаем без ошибки

                var validationContext = new ValidationContext<object>(argument);
                var result = await validator.ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors)
                    {
                        Title = "Ошибка валидации",
                        Status = StatusCodes.Status400BadRequest
                    });
                    return; // не вызываем next() — прерываем конвейер, до контроллера запрос не дойдёт
                }
            }

            await next(); // всё валидно — передаём управление дальше, в контроллер
        }
    }
}
