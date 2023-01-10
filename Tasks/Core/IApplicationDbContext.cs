using Microsoft.EntityFrameworkCore;
using Tasks.Core.Models.Domains;

namespace Tasks.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Task> Tasks { get; set; }

        int SaveChanges();
    }
}
