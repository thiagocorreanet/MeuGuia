using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace MeuGuia.Infra.Repository;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Base
{

    protected MeuGuiaContext _context;
    protected DbSet<TEntity> _dbSet;

    public RepositoryBase(MeuGuiaContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    /// <summary>
    /// Obtém todos os registros da entidade de forma assíncrona.
    /// </summary>
    /// <param name="trackChange">Indica se as alterações devem ser rastreadas (padrão: false).</param>
    /// <returns>Uma coleção de registros da entidade.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange = false)
    {
        return !trackChange
             ? await _dbSet.AsNoTracking().ToListAsync()
             : await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Busca registros da entidade com base em uma expressão de condição de forma assíncrona.
    /// </summary>
    /// <param name="expression">Expressão de condição para filtrar os registros.</param>
    /// <param name="trackChange">Indica se as alterações devem ser rastreadas (padrão: false).</param>
    /// <returns>Uma coleção de registros da entidade que atendem à condição especificada.</returns>
    public async Task<IEnumerable<TEntity>> SearchByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChange = false)
    {
        return !trackChange
            ? await _dbSet.AsNoTracking().Where(expression).ToListAsync()
            : await _dbSet.Where(expression).ToListAsync();
    }

    /// <summary>
    /// Obtém uma entidade pelo seu ID de forma assíncrona.
    /// </summary>
    /// <param name="id">ID da entidade a ser buscada.</param>
    /// <param name="trackChange">Indica se as alterações devem ser rastreadas (padrão: false).</param>
    /// <returns>A entidade correspondente ao ID especificado ou null se não encontrada.</returns>
    public async Task<TEntity> GetByIdAsync(int id, bool trackChange = false)
    {
        return !trackChange
           ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
           : await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Cria uma nova entidade no contexto.
    /// </summary>
    /// <param name="entity">Entidade a ser criada.</param>
    public void Create(TEntity entity)
    {
        _context.Add(entity);
    }

    /// <summary>
    /// Atualiza uma entidade no contexto.
    /// </summary>
    /// <param name="entity">Entidade a ser atualizada.</param>
    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    /// <summary>
    /// Remove uma entidade do contexto.
    /// </summary>
    /// <param name="entity">Entidade a ser removida.</param>
    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    /// <summary>
    /// Salva as alterações feitas no contexto de forma assíncrona.
    /// </summary>
    /// <returns>Verdadeiro se alguma alteração foi salva, falso caso contrário.</returns>
    public async Task<bool> SaveChangesAsync()
    {

        _context.OnBeforeSaveChanges();
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Inicia uma transação assíncrona no contexto do banco de dados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task StartTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Confirma a transação assíncrona no contexto do banco de dados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    /// <summary>
    /// Desfaz (rollback) uma transação assíncrona no contexto do banco de dados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    /// <summary>
    /// Libera os recursos do contexto do banco de dados.
    /// </summary>
    public void Dispose()
    {
        _context?.Dispose();
    }
}
