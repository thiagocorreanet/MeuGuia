using System;
using AutoMapper;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Commands.Revenue.Delete;

public class DeleteRevenueCommandHendler : CreateBaseCommand, IRequestHandler<DeleteRevenueCommandRequest, bool>
{
    private readonly IRepositoryRevenue _iRepositoryRevenue;

    public DeleteRevenueCommandHendler(INotificationError notificationError, IMapper iMapper, IRepositoryRevenue iRepositoryRevenue) : base(notificationError, iMapper)
    {
        _iRepositoryRevenue = iRepositoryRevenue;
    }

    public async Task<bool> Handle(DeleteRevenueCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = true;

        try
        {
            var revenueGetById = await _iRepositoryRevenue.GetByIdAsync(request.Id);
            if (revenueGetById is null)
            {
                Notify("Ops! Não foi possível localizar o registro que você está tentando excluir.");
                return false;
            }

            await _iRepositoryRevenue.StartTransactionAsync();
            _iRepositoryRevenue.Delete(revenueGetById);
            var result = await _iRepositoryRevenue.SaveChangesAsync();

            if (!result)
            {
                Notify("Ops! Não foi possível salvar seu registro. Por favor tente novamente.");
                await _iRepositoryRevenue.RollbackTransactionAsync();
                return false;
            }

            await _iRepositoryRevenue.CommitTransactionAsync();
        }
        catch (Exception)
        {
            if (transactionStarted) await _iRepositoryRevenue.RollbackTransactionAsync();

            Notify($"Não foi possível salvar o registro.");
        }

        return true;
    }
}
