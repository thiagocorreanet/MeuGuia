using System;
using AutoMapper;
using Azure.Core;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

namespace MeuGuia.Application.Commands.Revenue.Update;

public class UpdateRevenueCommandHandler : CreateBaseCommand, IRequestHandler<UpdateRevenueCommandRequest, bool>
{

    private readonly IRepositoryRevenue _iRepositoryRevenue;

    public UpdateRevenueCommandHandler(INotificationError notificationError, IMapper iMapper, IRepositoryRevenue iRepositoryRevenue) : base(notificationError, iMapper)
    {
        _iRepositoryRevenue = iRepositoryRevenue;
    }

    public async Task<bool> Handle(UpdateRevenueCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStarted = true;
        var revenueEntitie = await SimpleMapping<MeuGuia.Domain.Entitie.Revenue>(request);
        if (!Validate(revenueEntitie))return false;

        try
        {
            request.Value = Math.Round(request.Value, 2);
            var revenueById = await _iRepositoryRevenue.GetByIdAsync(request.Id);
            if(revenueById is null)
            {
                Notify("Não foi possível alterar seu registro, erro ao tentar obter os dados.");
                return false;
            }

            await _iRepositoryRevenue.StartTransactionAsync();
            _iRepositoryRevenue.Update(revenueEntitie);
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

    private bool Validate(MeuGuia.Domain.Entitie.Revenue revenue)
    {
        if (!RunValidation(new ValidationRevenue(), revenue))
            return false;

        return true;
    }
}
