using ecom_api_nosql_.Models;
using FluentValidation;

namespace ecom_api_nosql_.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire")
            .MaximumLength(100);

        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom est obligatoire")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email est obligatoire")
            .EmailAddress().WithMessage("Format d'email invalide");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Le téléphone est obligatoire")
            .Matches(@"^\+?[0-9]{8,15}$")
            .WithMessage("Numéro de téléphone invalide");

        RuleFor(x => x.Adresse)
            .NotEmpty().WithMessage("L'adresse est obligatoire")
            .MaximumLength(250);

        RuleFor(x => x.Statut)
            .IsInEnum()
            .WithMessage("Statut client invalide");
    }
}
