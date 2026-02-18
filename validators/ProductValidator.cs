using ecom_api_nosql_.Models;
using FluentValidation;

namespace ecom_api_nosql_.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du produit est obligatoire")
            .MaximumLength(150);

        RuleFor(x => x.Categorie)
            .NotEmpty().WithMessage("La catégorie est obligatoire")
            .MaximumLength(100);

        RuleFor(x => x.Prix)
            .GreaterThan(0)
            .WithMessage("Le prix doit être supérieur à 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Le stock ne peut pas être négatif");

        RuleForEach(x => x.Tags)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Specifications)
            .SetValidator(new ProductSpecificationsValidator()!)
            .When(x => x.Specifications != null);
    }
}

