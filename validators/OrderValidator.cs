using ecom_api_nosql_.Models;
using FluentValidation;

namespace ecom_api_nosql_.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId est obligatoire");

        RuleFor(x => x.DateCommande)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("La date de commande ne peut pas être dans le futur");

        RuleFor(x => x.Statut)
            .NotEmpty()
            .WithMessage("Le statut de la commande est obligatoire")
            .MaximumLength(50);

        RuleFor(x => x.Articles)
            .NotNull()
            .WithMessage("La liste des articles est obligatoire")
            .Must(x => x.Count > 0)
            .WithMessage("La commande doit contenir au moins un article");

        RuleFor(x => x.MontantTotal)
            .GreaterThan(0)
            .WithMessage("Le montant total doit être supérieur à 0");

        RuleFor(x => x.AdresseLivraison)
            .NotEmpty()
            .WithMessage("L'adresse de livraison est obligatoire")
            .MaximumLength(250);
    }
}
