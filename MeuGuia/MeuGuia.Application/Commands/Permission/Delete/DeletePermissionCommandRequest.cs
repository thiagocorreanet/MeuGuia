using MediatR;

namespace MeuGuia.Application.Commands.Permission.Delete;

public class DeletePermissionCommandRequest : IRequest<bool>
{
    public DeletePermissionCommandRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
