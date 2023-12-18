using App104.DAl;
using App104.Entities;
using App104.Repositories.Interfaces;

namespace App104.Repositories.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {

        }
    }
}
