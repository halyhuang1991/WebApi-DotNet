using Microsoft.EntityFrameworkCore;

namespace CSharp.Models
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            //this.Database.EnsureCreated();
            this.Database.OpenConnectionAsync();
        }

        public DbSet<product> Products { get; set; }
        public DbSet<book> book { get; set; }
    }
}