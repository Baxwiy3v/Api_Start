using App104.Entities;
using Microsoft.EntityFrameworkCore;

namespace App104.DAl
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
       

       public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }




    }
}
