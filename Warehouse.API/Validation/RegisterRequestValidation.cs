using FluentValidation;
using Warehouse.API.Controllers;

namespace Warehouse.API.Validation
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password обязателен")
                .MinimumLength(8).WithMessage("Password должен быть не короче 8 символов")
                .Matches("[A-Z]").WithMessage("Password должен содержать хотя бы одну заглавную букву")
                .Matches("[0-9]").WithMessage("Password должен содержать хотя бы одну цифру");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Недопустимая роль");
        }
    }
}
