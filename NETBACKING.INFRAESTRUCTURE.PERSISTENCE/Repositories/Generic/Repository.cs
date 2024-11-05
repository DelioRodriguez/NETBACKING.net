using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbSet<T> DbSet;
    protected readonly AppDbContext Context;

    public Repository(AppDbContext context)
    {
        this.Context = context;
        this.DbSet = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(); 
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}