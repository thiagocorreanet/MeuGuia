using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

using Microsoft.Extensions.Logging;

namespace MeuGuia.Application.Commands.Permission.Create;

public class CreatePermissionCommandHandler : CreateBaseCommand, IRequestHandler<CreatePermissionCommandRequest, bool>
{
    private readonly IRepositoryPermission _iRepositoryPermission;
    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;
    private readonly HelperIdentity _helperIdentity;
    private readonly ILogger<CreatePermissionCommandHandler> _logger;

    public CreatePermissionCommandHandler(INotificationError notificationError, IMapper iMapper, IRepositoryPermission iRepositoryPermission, IRepositoryManagementAccount iRepositoryManagementAccount, HelperIdentity helperIdentity, ILogger<CreatePermissionCommandHandler> logger) : base(notificationError, iMapper)
    {
        _iRepositoryPermission = iRepositoryPermission;
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
        _helperIdentity = helperIdentity;
        _logger = logger;
    }

    public async Task<bool> Handle(CreatePermissionCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStared = true;

        try
        {
            var userClaim = await RegisterUserClaim(request.Email, request.Page, request.Role);

            if (userClaim is null)
            {
                return false;
            }

            request.AspNetUserClaimId = userClaim.Id;
            request.UserId = userClaim.UserId;

            var permissionEntitie = await SimpleMapping<MeuGuia.Domain.Entitie.Permission>(request);
            if (!Validate(permissionEntitie)) return false;

            await _iRepositoryPermission.StartTransactionAsync();
            _iRepositoryPermission.Create(permissionEntitie);
            var result = await _iRepositoryPermission.SaveChangesAsync();

            if (!result)
            {
                _logger.LogError($"Ops! Não foi possível salvar seu registro. Por favor tente novamente.");
                Notify("Ops! Não foi possível salvar seu registro. Por favor tente novamente.");
                await _iRepositoryPermission.RollbackTransactionAsync();
                return false;
            }

            await _iRepositoryPermission.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"Não foi possível cadastrar sua permissão: {ex.Message}");
            if (transactionStared is true)
                await _iRepositoryPermission.RollbackTransactionAsync();

            Notify($"Não foi possível salvar o registro");
        }

        return true;
    }

    public async Task<IdentityUserClaimCustom?> RegisterUserClaim(string email, string claimType, string claimValue)
    {
        try
        {
            var result = await _iRepositoryManagementAccount.RegisterUserClimAsync(email, claimType, claimValue);

            if (result is null)
            {
                Notify("Dados do usuário, não encontrados.");
            }

            return result;
        }
        catch (Exception ex)
        {
            Notify("Ops! Não foi possível processar sua solicitação");
        }

        return null;

    }

    private bool Validate(MeuGuia.Domain.Entitie.Permission permission)
    {
        if (!RunValidation(new ValidationPermission(), permission))
            return false;

        return true;
    }
}
