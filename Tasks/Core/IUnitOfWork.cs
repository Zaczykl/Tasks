using Tasks.Core.Repositories;
using Tasks.Persistence.Repositories;

namespace Tasks.Core
{
    public interface IUnitOfWork
    {
        ITaskRepository TaskRepository { get;}

        void Complete();
    }
}
