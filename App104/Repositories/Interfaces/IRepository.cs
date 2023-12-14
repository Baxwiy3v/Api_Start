using App104.Entities;
using System.Linq.Expressions;

namespace App104.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task SaveChangesAsync();
        void Update(Category category);
        void Delete(Category category);

    }
}
