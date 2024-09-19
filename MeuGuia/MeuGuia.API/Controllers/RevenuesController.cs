using MediatR;

using MeuGuia.Application.Commands.Revenue.Create;
using MeuGuia.Application.Commands.Revenue.Delete;
using MeuGuia.Application.Commands.Revenue.Update;
using MeuGuia.Application.Queries.Revenue.GetAll;
using MeuGuia.Application.Queries.Revenue.GetById;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.JWT;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuGuia.WebAPI.Controllers
{
    [Authorize]
    [Route("api/revenues")]
    public class RevenuesController : MainController
    {
        private readonly IMediator _iMediator;

        public RevenuesController(INotificationError notificadorErros, IMediator iMediator) : base(notificadorErros)
        {
            _iMediator = iMediator;
        }

        /// <summary>
        /// Retorna uma lista com todas as receitas cadastradas.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma lista de todas as receitas cadastradas.
        /// </remarks>
        /// <returns>Uma lista de receitas.</returns>
        /// <response code="200">Retorna a lista de receitas</response>
        /// <response code="401">Se o usuário não estiver autorizado</response>
        /// <response code="403">Se o usuário não tiver as permissões necessárias</response>
        [ClaimsAuthorize("Revenues", nameof(GetAll))]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<QueryRevenueGetAllResponse>>> GetAll()
        {
            var revenues = await _iMediator.Send(new QueryRevenueGetAllRequest());
            return Ok(revenues);
        }

        /// <summary>
        /// Obtem o registro das receitas pelo id.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna uma receita específica com base no ID fornecido.
        /// </remarks>
        /// <param name="id">O ID da receita.</param>
        /// <returns>Uma Receita.</returns>
        /// <response code="200">Retorna a receita com sucesso.</response>
        /// <response code="404">Se a receita não for encontrado</response>
        [ClaimsAuthorize("Revenues", nameof(GetById))]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<QueryRevenueGetByIdResponse>> GetById(int id)
        {
            var banner = await _iMediator.Send(new QueryRevenueGetByIdRequest(id));

            if (banner is null) return NotFound();

            return Ok(banner);
        }

        /// <summary>
        /// Cria uma nova receita.
        /// </summary>
        /// <remarks>
        /// Este endpoint cria uma nova receita com base nos dados fornecidos.
        /// </remarks>
        /// <param name="createRevenueCommandRequest">Os dados da nova receita.</param>
        /// <returns>A receita criada 201.</returns>
        /// <response code="201">Indica que a receita foi criada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos são inválidos</response>
        [ClaimsAuthorize("Revenues", nameof(Post))]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRevenueCommandRequest createRevenueCommandRequest)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _iMediator.Send(createRevenueCommandRequest);

            if (!ValidOperation()) return CustomResponse();

            return Created();
        }

        /// <summary>
        /// Atualiza uma receita existente.
        /// </summary>
        /// <remarks>
        /// Este endpoint atualiza uma receita existente com base nos dados fornecidos.
        /// </remarks>
        /// <param name="id">O ID da receita a ser atualizado.</param>
        /// <param name="updateRevenueCommandRequest">Os novos dados da receita.</param>
        /// <returns>A receita atualizada.</returns>
        /// <response code="200">Retorna a receita atualizada</response>
        /// <response code="400">Se os dados fornecidos são inválidos</response>
        /// <response code="404">Se a receita não for encontrado</response>
        [ClaimsAuthorize("Revenues", nameof(Put))]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateRevenueCommandRequest updateRevenueCommandRequest)
        {
            if (id != updateRevenueCommandRequest.Id)
            {
                NotifyError("Ops! Não podemos processar sua solicitação, erro de integridades de IDS.");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _iMediator.Send(updateRevenueCommandRequest);

            if (!ValidOperation()) return CustomResponse();

            return Ok();
        }

        /// <summary>
        /// Exclui uma receita.
        /// </summary>
        /// <remarks>
        /// Este endpoint exclui uma receita com base no ID fornecido.
        /// </remarks>
        /// <param name="id">O ID da receita a ser excluído.</param>
        /// <response code="204">A receita foi excluído com sucesso.</response>
        /// <response code="400">Se os dados fornecidos são inválidos.</response>
        /// <response code="404">Se a receita não for encontrado</response>
        [ClaimsAuthorize("Revenues", nameof(Delete))]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _iMediator.Send(new DeleteRevenueCommandRequest(id));

            if (!ValidOperation()) return CustomResponse();

            return NoContent();
        }

    }
}
