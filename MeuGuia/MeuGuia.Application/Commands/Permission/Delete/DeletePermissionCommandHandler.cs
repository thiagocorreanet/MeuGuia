using AutoMapper;

using MediatR;
using MeuGuia.Application.Commands.Revenue.Delete;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Repository;

namespace MeuGuia.Application.Commands.Permission.Delete;

public class DeletePermissionCommandHandler : CreateBaseCommand, IRequestHandler<DeleteRevenueCommandRequest, bool>
{
    private readonly IRepositoryPermission _iRepositoryPermission;

    public DeletePermissionCommandHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryPermission iRepositoryPermission) : base(notificationError, iMapper)
    {
        _iRepositoryPermission = iRepositoryPermission;
    }

    public async Task<bool> Handle(DeleteRevenueCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = true;

        try
        {
            var permissionGetById = await _iRepositoryPermission.GetByIdAsync(request.Id);
            if(permissionGetById is null)
            {
                Notify("Ops! Não foi possível localizar o registro que você está tentando excluir.");
                return false;
            }

            await _iRepositoryPermission.StartTransactionAsync();
            _iRepositoryPermission.Delete(permissionGetById);
            var result = await _iRepositoryPermission.SaveChangesAsync();

            if(result is false)
            {
                Notify("Ops! Não foi possível salvar seu registro. Por favor tente novamente.");
                await _iRepositoryPermission.RollbackTransactionAsync();
                return false;
            }

            await _iRepositoryPermission.CommitTransactionAsync();
        }
        catch (Exception)
        {
            if (transactionStarted is true) await _iRepositoryPermission.RollbackTransactionAsync();

            Notify($"Não foi possível salvar o registro.");
        }

        return true;
    }
}
