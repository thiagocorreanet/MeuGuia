using FluentValidation;

using MeuGuia.Domain.Entitie;

namespace MeuGuia.Domain.Validation;

public class ValidationUserClaim : AbstractValidator<IdentityUserClaimCustom>
{
    public ValidationUserClaim()
    {
        RuleFor(x => x.ClaimType)
           .NotEmpty().WithMessage("O campo do tipo da claim, não pode ser em branco.")
           .NotNull().WithMessage("O campo de identificação do usuário, não pode ser nulo.");

        RuleFor(x => x.ClaimValue)
          .NotEmpty().WithMessage("O campo do valor da claim, não pode ser em branco.")
          .NotNull().WithMessage("O campo do valor da claim, não pode ser nulo.");
    }
}
