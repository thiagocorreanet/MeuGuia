using FluentValidation;

using MeuGuia.Domain.Entitie;

namespace MeuGuia.Domain.Validation;

public class ValidationPermission :AbstractValidator<Permission>
{
    public ValidationPermission()
    {

        RuleFor(x => x.Page)
            .NotEmpty().WithMessage("O campo página, não poderá ser em branco.")
            .NotNull().WithMessage("O campo página, não poderá ser nulo.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("O campo permissão, não poderá ser em branco.")
            .NotNull().WithMessage("O campo permissão, não poderá ser nulo.");
    }
}
