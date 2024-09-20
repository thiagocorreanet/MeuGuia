using MediatR;

using MeuGuia.Application.Commands.Permission.Create;
using MeuGuia.Application.Commands.Permission.Delete;
using MeuGuia.Application.Commands.Permission.Update;
using MeuGuia.Application.Queries.Permission.GetAll;
using MeuGuia.Application.Queries.Permission.GetById;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.JWT;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuGuia.WebAPI.Controllers;

[Authorize]
[Route("api/permissions")]
public class PermissionsController : MainController
{
    private readonly IMediator _iMediator;

    public PermissionsController(INotificationError notificadorErros, IMediator iMediator) : base(notificadorErros)
    {
        _iMediator = iMediator;
    }

    /// <summary>
    /// Retorna uma lista com todas as permissões cadastradas.
    /// </summary>
    /// <remarks>
    /// Este endpoint retorna uma lista de todas as permissões cadastradas.
    /// </remarks>
    /// <returns>Uma lista de permissões.</returns>
    /// <response code="200">Retorna a lista de permissões</response>
    /// <response code="401">Se o usuário não estiver autorizado</response>
    /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
    [ClaimsAuthorize("Permissions", nameof(GetAll))]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<QueryPermissionGetAllResponse>>> GetAll()
    {
        var permissions = await _iMediator.Send(new QueryPermissionGetAllRequest());
        return Ok(permissions);
    }

    /// <summary>
    /// Obtem o registro das permissões pelo id.
    /// </summary>
    /// <remarks>
    /// Este endpoint retorna uma permissão específica com base no ID fornecido.
    /// </remarks>
    /// <param name="id">O ID da permissão.</param>
    /// <returns>Uma permissão.</returns>
    /// <response code="200">Retorna a permissão com sucesso.</response>
    /// <response code="404">Se a permissão não for encontrado</response>
    /// <response code="401">Se o usuário não estiver autorizado</response>
    /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
    [ClaimsAuthorize("Permissions", nameof(GetById))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QueryPermissionGetByIdResponse>> GetById(int id)
    {
        var permission = await _iMediator.Send(new QueryPermissionGetByIdRequest(id));

        if (permission is null) return NotFound();

        return Ok(permission);
    }

    /// <summary>
    /// Cria uma nova permissão.
    /// </summary>
    /// <remarks>
    /// Este endpoint cria uma nova permissão com base nos dados fornecidos.
    /// </remarks>
    /// <param name="createPermissionCommandRequest">Os dados da nova permissão.</param>
    /// <returns>A permissão criada 201.</returns>
    /// <response code="201">Indica que a permissão foi criada com sucesso.</response>
    /// <response code="400">Se os dados fornecidos são inválidos</response>
    /// <response code="401">Se o usuário não estiver autorizado</response>
    /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
    [ClaimsAuthorize("Permissions", "Post")]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePermissionCommandRequest createPermissionCommandRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _iMediator.Send(createPermissionCommandRequest);

        if (!ValidOperation()) return CustomResponse();

        return Created();
    }

    /// <summary>
    /// Atualiza uma permissão existente.
    /// </summary>
    /// <remarks>
    /// Este endpoint atualiza uma permissão existente com base nos dados fornecidos.
    /// </remarks>
    /// <param name="id">O ID da permissão a ser atualizado.</param>
    /// <param name="updatePermissionCommandRequest">Os novos dados da permissão.</param>
    /// <returns>A permissão atualizada.</returns>
    /// <response code="200">Retorna a permissão atualizada</response>
    /// <response code="400">Se os dados fornecidos são inválidos</response>
    /// <response code="404">Se a receita não for encontrado</response>
    /// <response code="401">Se o usuário não estiver autorizado</response>
    /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
    [ClaimsAuthorize("Permissions", nameof(Put))]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, UpdatePermissionCommandRequest updatePermissionCommandRequest)
    {
        if (id != updatePermissionCommandRequest.Id)
        {
            NotifyError("Ops! Não podemos processar sua solicitação, erro de integridades de IDS.");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _iMediator.Send(updatePermissionCommandRequest);

        if (!ValidOperation()) return CustomResponse();

        return Ok();
    }

    /// <summary>
    /// Exclui uma permissão.
    /// </summary>
    /// <remarks>
    /// Este endpoint exclui uma permissão com base no ID fornecido.
    /// </remarks>
    /// <param name="id">O ID da permissão a ser excluído.</param>
    /// <response code="204">A permissão foi excluído com sucesso.</response>
    /// <response code="400">Se os dados fornecidos são inválidos.</response>
    /// <response code="404">Se a permissão não for encontrado</response>
    /// <response code="401">Se o usuário não estiver autorizado</response>
    /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
    [ClaimsAuthorize("Permissions", nameof(Delete))]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _iMediator.Send(new DeletePermissionCommandRequest(id));

        if (!ValidOperation()) return CustomResponse();

        return NoContent();
    }
}
