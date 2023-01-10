using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tasks.Core.Models.Domains;

namespace Tasks.Core.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null);

        IEnumerable<Category> GetCategories();

        Task Get(int id, string userId);

        void Add(Task task);

        void Update(Task task);

        void Delete(int id, string userId);

        void Finish(int id, string userId);
    }
}
