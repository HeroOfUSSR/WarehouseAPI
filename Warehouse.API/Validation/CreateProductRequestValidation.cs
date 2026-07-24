using FluentValidation;
using Warehouse.API.Controllers;

namespace Warehouse.API.Validation
{
    public class CreateProductRequestValidation : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidation()
        {
            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("Sku обязателен")
                .MaximumLength(64).WithMessage("Sku не должен превышать 64 символа");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name обязателен")
                .MaximumLength(256).WithMessage("Name не должен превышать 256 символов");

            RuleFor(x => x.Unit)
                .IsInEnum().WithMessage("Недопустимое значение единицы измерения");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description не должен превышать 2000 символов");
        }
    }
}
