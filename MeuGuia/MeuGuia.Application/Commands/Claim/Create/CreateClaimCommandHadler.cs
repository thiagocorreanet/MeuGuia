using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

namespace MeuGuia.Application.Commands.Claim.Create;

public class CreateClaimCommandHadler : CreateBaseCommand, IRequestHandler<CreateClaimCommandRequest, IdentityUserClaimCustom>
{
    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;

    public CreateClaimCommandHadler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryManagementAccount iRepositoryManagementAccount) : base(notificationError, iMapper)
    {
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
    }

    public async Task<IdentityUserClaimCustom?> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityUserClaimCustom userClaimCustom = await SimpleMapping<IdentityUserClaimCustom>(request);
        if (!Validate(userClaimCustom)) return null;

        try
        {
            var result = await _iRepositoryManagementAccount.RegisterUserClimAsync(request.Email, request.ClaimType, request.ClaimValue);
            return result;
        }
        catch (Exception ex)
        {
            Notify("Ops! Não foi possível processar sua solicitação");
        }

        return null;

    }

    private bool Validate(IdentityUserClaimCustom identityUserClaimCustom)
    {
        if (!ValidationIdentity(new ValidationUserClaim(), identityUserClaimCustom))
            return false;

        return true;
    }
}
