using App104.DAl;
using App104.Entities;
using App104.Repositories.Interfaces;

namespace App104.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
