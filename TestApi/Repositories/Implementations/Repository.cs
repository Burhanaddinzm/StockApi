using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestApi.Data;
using TestApi.Models.Common;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseAuditableEntity
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not save entity to the database.", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not update entity in the database.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Entity with id {id} does not exist!");
            }
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not delete entity in the database.", ex);
        }
    }

    public async Task<T?> GetByIdAsync(int id, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? expression, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return expression != null
            ? await query.FirstOrDefaultAsync(expression)
            : await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (expression != null)
        {
            query = query.Where(expression);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == id);
    }
}
