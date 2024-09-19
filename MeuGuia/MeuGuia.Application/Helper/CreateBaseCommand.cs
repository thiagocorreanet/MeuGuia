using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Helper;

public abstract class CreateBaseCommand
{
    private readonly INotificationError _notificationError;
    private readonly IMapper _iMapper;
    private readonly HelperIdentity _helperIdentity;

    protected CreateBaseCommand(INotificationError notificationError, IMapper iMapper, HelperIdentity helperIdentity)
    {
        _notificationError = notificationError;
        _iMapper = iMapper;
        _helperIdentity = helperIdentity;
    }

    #region Notification

    /// <summary>
    /// Notifica erros de validação.
    /// </summary>
    /// <param name="validationResult">Resultado da validação.</param>
    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    /// <summary>
    /// Notifica um erro específico.
    /// </summary>
    /// <param name="message">Mensagem de erro.</param>
    protected void Notify(string message)
    {
        _notificationError.Handle(new Domain.Notification.NotificationError(message));
    }

    /// <summary>
    /// Executa a validação de uma entidade.
    /// </summary>
    /// <typeparam name="TV">Tipo do validador.</typeparam>
    /// <typeparam name="TE">Tipo da entidade.</typeparam>
    /// <param name="validation">Validador a ser utilizado.</param>
    /// <param name="entity">Entidade a ser validada.</param>
    /// <returns>Verdadeiro se a validação for bem-sucedida, falso caso contrário.</returns>
    protected bool RunValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Base
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

    protected bool ValidationIdentity<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

    #endregion

    #region Mapping

    /// <summary>
    /// Mapeia uma instância do modelo para a entidade de destino de forma assíncrona.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade de destino.</typeparam>
    /// <param name="tModel">A instância do modelo a ser mapeada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado da tarefa contém a instância mapeada da entidade de destino.</returns>
    protected Task<TEntity> SimpleMapping<TEntity>(object tModel)
    {
        TEntity entity = _iMapper.Map<TEntity>(tModel);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Mapeia uma lista de instâncias do modelo para uma lista de entidades de destino de forma assíncrona.
    /// </summary>
    /// <typeparam name="TEntity">O tipo das entidades de destino.</typeparam>
    /// <param name="tModelList">A lista de instâncias do modelo a serem mapeadas.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado da tarefa contém a lista de instâncias mapeadas das entidades de destino.</returns>
    protected Task<IEnumerable<TEntity>> MappingList<TEntity>(IEnumerable<object> tModelList)
    {
        IEnumerable<TEntity> entityList = _iMapper.Map<IEnumerable<TEntity>>(tModelList);
        return Task.FromResult(entityList);
    }



    #endregion

    #region Users

    /// <summary>
    /// Método responsável por obter os dados do usuário que esta logado na API.
    /// </summary>
    /// <returns>Retorna os dados do usuário e caso não ache vai retornar nulo.</returns>
    protected async Task<IdentityUserCustom?> GetLoggedInUser()
    {
        var user = await _helperIdentity.GetLoggedInUser();

        if (user is null)
        {
            Notify("Usuário não autenticado, por favor tente novamente.");
            return null;
        }

        return user;
    }

    #endregion
}
