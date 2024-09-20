using AutoMapper;

using MediatR;

using MeuGuia.Application.Commands.Permission.Create;
using MeuGuia.Application.Commands.Revenue.Update;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

using Microsoft.Extensions.Logging;

namespace MeuGuia.Application.Commands.Permission.Update;

public class UpdatePermissionCommandHandler : CreateBaseCommand, IRequestHandler<UpdatePermissionCommandRequest, bool>
{
    private readonly IRepositoryPermission _iRepositoryPermission;
    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;
    private readonly ILogger<CreatePermissionCommandHandler> _logger;

    public UpdatePermissionCommandHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryPermission iRepositoryPermission, IRepositoryManagementAccount iRepositoryManagementAccount, ILogger<CreatePermissionCommandHandler> logger) : base(notificationError, iMapper)
    {
        _iRepositoryPermission = iRepositoryPermission;
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdatePermissionCommandRequest request, CancellationToken cancellationToken)
    {

        var permissionEntitie = await SimpleMapping<MeuGuia.Domain.Entitie.Permission>(request);
        if (Validate(permissionEntitie) is false) return false;

        try
        {
            var permissionById = await _iRepositoryPermission.GetByIdAsync(request.Id);
            if (permissionById is null)
            {
                Notify("Não foi possível alterar seu registro, erro ao tentar obter os dados.");
                return false;
            }

            await UpdatePermission(permissionEntitie);

            var updateClaim = await UpdateUserClaim(request.Id, request.Page, request.Role);
            if (updateClaim is null)
            {
                await UpdatePermission(permissionById);
                return false;
            }
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível salvar o registro.");
            _logger.LogError($"Não foi possível atualizar sua permissão. Detalhe da mensagem: {ex.Message} - [{nameof(UpdatePermissionCommandHandler)} - {nameof(Handle)}]");
            return false;
        }

        return true;
    }

    private async Task<bool> UpdatePermission(Domain.Entitie.Permission permissionEntitie)
    {
        bool transactionStarted = true;

        try
        {
            await _iRepositoryPermission.StartTransactionAsync();
            _iRepositoryPermission.Update(permissionEntitie);
            var result = await _iRepositoryPermission.SaveChangesAsync();

            if (result is false)
            {
                Notify("Ops! Não foi possível salvar seu registro. Por favor tente novamente.");
                await _iRepositoryPermission.RollbackTransactionAsync();
                return false;
            }

            await _iRepositoryPermission.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            if (transactionStarted) await _iRepositoryPermission.RollbackTransactionAsync();
            Notify($"Não foi possível salvar o registro.");
            _logger.LogError($"Não foi possível atualizar sua permissão. Detalhe da mensagem: {ex.Message} - [{nameof(UpdatePermissionCommandHandler)} - {nameof(Handle)}]");
            return false;
        }

        return true;
    }

    public async Task<IdentityUserClaimCustom?> UpdateUserClaim(int id, string claimType, string claimValue)
    {
        var result = new IdentityUserClaimCustom();

        try
        {
            result = await _iRepositoryManagementAccount.UpdateUserClimAsync(id, claimType, claimValue);

            if (result is null)
            {
                Notify("Dados da permissão, não foram encontrados.");
                _logger.LogError($"Não foi possível atualizar sua claim. [{nameof(UpdatePermissionCommandHandler)} - {nameof(UpdateUserClaim)}]");
                return result;
            }
        }
        catch (Exception ex)
        {
            Notify("Ops! Não foi possível processar sua solicitação");
            _logger.LogCritical($"Não foi possível processar atualização da claim: {ex.Message}");
            return null;
        }

        return result;
    }

    private bool Validate(MeuGuia.Domain.Entitie.Permission permission)
    {
        if (!RunValidation(new ValidationPermission(), permission))
            return false;

        return true;
    }
}
