using AutoMapper;
using MediatR;
using MeuGuia.Application.Helper;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Commands.Login.Create;

public class CreateLoginCommandHandler : CreateBaseCommand, IRequestHandler<CreateLoginCommandRequest, CreateLoginCommandResponse>
{
    private readonly IRepositoryManagementAccount _iRepositoryManagementAccount;

    public CreateLoginCommandHandler(INotificationError notificationError, IMapper iMapper, IRepositoryManagementAccount iRepositoryManagementAccount) : base(notificationError, iMapper)
    {
        _iRepositoryManagementAccount = iRepositoryManagementAccount;
    }

    public async Task<CreateLoginCommandResponse> Handle(CreateLoginCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _iRepositoryManagementAccount.AuthenticateAsync(request.Email, request.Passwork);

            if (result is null)
            {
                Notify("Não foi possível autenticar, por favor tente novamente.");
                return null;
            }

            var claims = new List<CreateClaimUserCommandResponse>();

            foreach (var item in result.UserToken.Claims)
            {
                claims.Add(new CreateClaimUserCommandResponse
                {
                    Type = item.Type,
                    Value = item.Value
                });
            }

            var response = new CreateLoginCommandResponse
            {
                AccessToken = result.AccessToken,
                ExpiresIn = result.ExpiresIn,
                UserToken = new CreateUserTokenCommandResponse
                {
                    Id = result.UserToken.Id,
                    Email = result.UserToken.Email,
                    Claims = claims
                }
            };

            return response;

        }
        catch (Exception ex)
        {
            Notify("Ops! Não foi possível processar sua solicitação");
        }

        return null;
    }
}
