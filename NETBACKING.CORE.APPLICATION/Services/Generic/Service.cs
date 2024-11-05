using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;

namespace NETBACKING.CORE.APPLICATION.Services.Generic;

public class Service<T> : IService<T> where T : class
{
    private readonly IRepository<T> _repository;

    public Service(IRepository<T> repository)
    {
        this._repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity); 
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}