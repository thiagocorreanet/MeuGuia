using MediatR;

namespace MeuGuia.Application.Commands.Revenue.Update;

public class UpdateRevenueCommandRequest : IRequest<bool>
{
    public required int Id {get; set;}
    public required string Description { get; set; }
    public required decimal Value { get; set; }
}
