

using AutoMapper;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Commands.Logout.Create;

public class CreateLogoutCommandHandler : CreateBaseCommand, IRequestHandler<CreateLogoutCommandRequest, bool>
{

    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;

    public CreateLogoutCommandHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryManagementAccount iRepositoryManagementAccount) : base(notificationError, iMapper, helperIdentity)
    {
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
    }

    public async Task<bool> Handle(CreateLogoutCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Task result = _iRepositoryManagementAccount.Logout();
        }
        catch (Exception ex)
        {
            Notify($"Ops! Não foi possível processar sua solicitação. Detalhe do erro: {ex.Message}");
        }

        return true;
    }
}
