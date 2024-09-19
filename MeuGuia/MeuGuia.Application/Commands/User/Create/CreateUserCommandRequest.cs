using MediatR;

namespace MeuGuia.Application.Commands.User.Create;

public class CreateUserCommandRequest : IRequest<bool>
{
    public string Email { get; set; } = null!;
}
