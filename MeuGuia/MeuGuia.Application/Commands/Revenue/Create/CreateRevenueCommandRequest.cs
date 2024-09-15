using System;
using MediatR;

namespace MeuGuia.Application.Commands.Revenue.Create;

public class CreateRevenueCommandRequest : IRequest<bool>
{
    public required string Description {get; set;}
    public required double Value {get; set;}
}
