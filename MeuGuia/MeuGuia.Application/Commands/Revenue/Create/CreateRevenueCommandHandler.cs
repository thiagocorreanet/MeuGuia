using AutoMapper;

using MediatR;

using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.Validation;

namespace MeuGuia.Application.Commands.Revenue.Create;

public class CreateBannerCommandHandler : CreateBaseCommand, IRequestHandler<CreateRevenueCommandRequest, bool>
{

    private readonly IRepositoryRevenue _iRepositoryRevenue;

    public CreateBannerCommandHandler(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity, IRepositoryRevenue iRepositoryRevenue) : base(notificationError, iMapper)
    {
        _iRepositoryRevenue = iRepositoryRevenue;
    }

    public async Task<bool> Handle(CreateRevenueCommandRequest request, CancellationToken cancellationToken)
    {
        bool transactionStared = true;
        var revenueEntitie = await SimpleMapping<MeuGuia.Domain.Entitie.Revenue>(request);
        if (!await Validate(revenueEntitie)) return false;

        try
        {
            await _iRepositoryRevenue.StartTransactionAsync();
            _iRepositoryRevenue.Create(revenueEntitie);
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
            if (transactionStared)
                await _iRepositoryRevenue.RollbackTransactionAsync();

            Notify($"Não foi possível salvar o registro");
        }

        return true;
    }

    private async Task<bool> Validate(MeuGuia.Domain.Entitie.Revenue revenue)
    {
        if (!RunValidation(new ValidationRevenue(), revenue))
            return false;

        if (!string.IsNullOrWhiteSpace(revenue.Description) && (await _iRepositoryRevenue.SearchByConditionAsync(c => c.Description == revenue.Description)).Any())
        {
            Notify("Receita já consta cadastrada em nossa base de dados.");
            return false;
        }

        return true;
    }
}
