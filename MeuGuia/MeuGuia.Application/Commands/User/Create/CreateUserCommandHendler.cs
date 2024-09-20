using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

namespace MeuGuia.Application.Commands.User.Create;

public class CreateUserCommandHendler : CreateBaseCommand, IRequestHandler<CreateUserCommandRequest, bool>
{
    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;

    public CreateUserCommandHendler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryManagementAccount iRepositoryManagementAccount) : base(notificationError, iMapper)
    {
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
    }

    public async Task<bool> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            IdentityUserCustom? user = await SimpleMapping<IdentityUserCustom>(request);
            if (!await Validate(user)) return false;

            bool result = await _iRepositoryManagementAccount.RegisterUserAsync(user);

            if (!result)
            {
                Notify("Ops! Não foi possível registar seu usuário, por favor tente novamente.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Notify("Ops! Não foi possível processar sua solicitação");
        }

        return true;
    }

    private async Task<bool> Validate(IdentityUserCustom identityUserCustom)
    {
        if (!ValidationIdentity(new ValidationCreateUser(), identityUserCustom))
            return false;

        return true;
    }
}
