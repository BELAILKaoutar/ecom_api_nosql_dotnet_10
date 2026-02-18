using ecom_api_nosql_.Models;
using FluentValidation;

namespace ecom_api_nosql_.Validators;

public class ProductSpecificationsValidator : AbstractValidator<ProductSpecifications>
{
    public ProductSpecificationsValidator()
    {
        RuleFor(x => x.Processeur)
            .MaximumLength(100);

        RuleFor(x => x.Ram)
            .MaximumLength(50);

        RuleFor(x => x.Stockage)
            .MaximumLength(50);
    }
}
