using FluentValidation;

using MeuGuia.Domain.Entitie;

namespace MeuGuia.Domain.Validation;

public class ValidationRevenue : AbstractValidator<Revenue>
{
    public ValidationRevenue()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("O camp não pode ser com valores em branco.")
            .NotNull().WithMessage("O campo descrição não pode ser nulo.")
            .MaximumLength(50).WithMessage("O campo descrição deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Value)
           .NotEmpty().WithMessage("O camp não pode ser com valores em branco.")
           .NotNull().WithMessage("O campo descrição não pode ser nulo.");
    }
}
