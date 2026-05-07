using System;

namespace ClinicWeb.Services.Shared
{
    public interface ICrudService<T, ID>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(ID id);

        public Task CreateAsync(T entity);
        public Task UpdateAsync(ID id, T entity);
        public Task DeleteAsync(ID id);
    }
}
