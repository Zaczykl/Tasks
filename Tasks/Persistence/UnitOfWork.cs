using Tasks.Core;
using Tasks.Core.Repositories;
using Tasks.Persistence.Repositories;

namespace Tasks.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            TaskRepository = new TaskRepository(_context);
        }
        public ITaskRepository TaskRepository { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
