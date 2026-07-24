using FluentValidation;
using Warehouse.API.Controllers;

namespace Warehouse.API.Validation
{
    public class TransferRequestValidation : AbstractValidator<TransferRequest>
    {
        public TransferRequestValidation()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId обязателен");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity должен быть положительным");

            RuleFor(x => x.SourceStorehouseId)
                .NotEmpty().WithMessage("SourceStorehouseId обязателен");

            RuleFor(x => x.DestinationStorehouseId)
                .NotEmpty().WithMessage("DestinationStorehouseId обязателен");

            RuleFor(x => x)
                .Must(x => x.SourceStorehouseId != x.DestinationStorehouseId)
                .WithMessage("Склад-источник и склад-назначение не могут совпадать")
                .WithName("DestinationStorehouseId");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Comment не должен превышать 1000 символов");
        }
    }
}
