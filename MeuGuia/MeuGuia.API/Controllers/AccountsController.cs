using MediatR;

using MeuGuia.Application.Commands.Login.Create;
using MeuGuia.Application.Commands.Logout.Create;
using MeuGuia.Application.Commands.User.Create;
using MeuGuia.Domain.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuGuia.WebAPI.Controllers;

[Route("api/account")]
public class AccountsController : MainController
{
    private readonly IMediator _iMediator;

    public AccountsController(INotificationError notificadorErros, IMediator iMediator) : base(notificadorErros)
    {
        _iMediator = iMediator;
    }

    /// <summary>
    /// Autentica um usuário com base no e-mail e senha fornecidos.
    /// </summary>
    /// <param name="createLoginCommandRequest">Objeto contendo o e-mail e senha do usuário.</param>
    /// <returns>Um objeto contendo o token de acesso e as informações do usuário, ou um problema de detalhes em caso de erro.</returns>
    /// <response code="200">Retorna o token de acesso e as informações do usuário.</response>
    /// <response code="400">Se o modelo de solicitação for inválido.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CreateLoginCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<ActionResult> Authenticate([FromBody] CreateLoginCommandRequest createLoginCommandRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var isAuthenticate = await _iMediator.Send(createLoginCommandRequest);

        if (!ValidOperation()) return CustomResponse();

        return Ok(isAuthenticate);
    }

    /// <summary>
    /// Registra um novo usuário com base nas informações fornecidas.
    /// </summary>
    /// <param name="createUserCommandRequest">Objeto contendo as informações do usuário a ser registrado.</param>
    /// <returns>Um status de sucesso ou um problema de detalhes em caso de erro.</returns>
    /// <response code="200">Usuário registrado com sucesso.</response>
    /// <response code="400">Se o modelo de solicitação for inválido.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [ProducesResponseType(typeof(CreateUserCommandRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("register-user")]
    public async Task<ActionResult> RegisterUsuer([FromBody] CreateUserCommandRequest createUserCommandRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _iMediator.Send(createUserCommandRequest);

        if (!ValidOperation()) return CustomResponse();

        return Ok();
    }

    /// <summary>
    /// Realiza o logout do usuário autenticado.
    /// </summary>
    /// <returns>Um status de sucesso ou um problema de detalhes em caso de erro.</returns>
    /// <response code="200">Logout realizado com sucesso.</response>
    /// <response code="400">Se o modelo de solicitação for inválido.</response>
    /// <response code="500">Se ocorrer um erro no servidor.</response>
    [Authorize]
    [ProducesResponseType(typeof(CreateLogoutCommandRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _iMediator.Send(new CreateLogoutCommandRequest());

        if (!ValidOperation()) return CustomResponse();

        return Ok();
    }
}
