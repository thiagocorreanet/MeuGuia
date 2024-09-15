
using System.Data.Common;
using MediatR;

namespace MeuGuia.Application.Commands.Revenue.Delete;

public class DeleteRevenueCommandRequest : IRequest<bool>
{
    public DeleteRevenueCommandRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

}

