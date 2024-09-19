using FluentValidation;

using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Validation.Helper;

namespace MeuGuia.Domain.Validation;

public class ValidationCreateUser : AbstractValidator<IdentityUserCustom>
{
    public ValidationCreateUser()
    {
        RuleFor(b => b.Email)
            .NotEmpty().WithMessage("O campo e-mail não pode ser vazio.")
            .NotNull().WithMessage("O campo e-mail não pode ser nulo")
            .MaximumLength(200).WithMessage("O campo e-mail deve ter no máximo 200 caracteres")
            .Must(HelperEmail.ValidateEmail).WithMessage("Seu e-mail está inválido.");
    }
}
