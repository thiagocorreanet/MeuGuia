using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MeuGuia.Domain.Interface;

[ApiController]
public class MainController : ControllerBase
{
    private readonly INotificationError _notificadorErros;

    protected MainController(INotificationError notificadorErros)
    {
        _notificadorErros = notificadorErros;
    }

    /// <summary>
    /// Verifica se a operação atual é válida.
    /// </summary>
    /// <returns>Retorna verdadeiro se a operação for válida, falso caso contrário.</returns>
    protected bool ValidOperation()
    {
        return !_notificadorErros.HasNotifications();
    }

    /// <summary>
    /// Cria uma resposta personalizada com base na validade da operação.
    /// </summary>
    /// <param name="result">O resultado a ser retornado se a operação for válida.</param>
    /// <returns>Retorna um objeto com a propriedade 'success' e 'data' ou 'errors'.</returns>
    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }
        return BadRequest(new
        {
            success = false,

            errors = _notificadorErros.GetNotifications().Select(n => n.Message)
        });
    }

    /// <summary>
    /// Cria uma resposta personalizada com base na validade do estado do modelo.
    /// </summary>
    /// <param name="modelState">O estado do modelo a ser verificado.</param>
    /// <returns>Retorna uma resposta personalizada.</returns>
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
        return CustomResponse();
    }

    /// <summary>
    /// Notifica todos os erros no estado do modelo fornecido.
    /// </summary>
    /// <param name="modelState">O estado do modelo a ser verificado.</param>
    protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in erros)
        {
            var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    /// <summary>
    /// Lida com a notificação de erro.
    /// </summary>
    /// <param name="message">A mensagem de erro a ser notificada.</param>
    protected void NotifyError(string message)
    {
        _notificadorErros.Handle(new MeuGuia.Domain.Notification.NotificationError(message));
    }
}