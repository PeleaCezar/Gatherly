using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
                  modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
