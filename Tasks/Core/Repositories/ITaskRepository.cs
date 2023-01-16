using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Tasks.Areas.Identity.Pages.Account;
using Tasks.Core.Models.Domains;
using Tasks.Persistence.Observer;

namespace Tasks.Core.Repositories
{
    public interface ITaskRepository: IObserver
    {
        IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null);

        IEnumerable<Category> GetCategories(string userId);

        Task Get(int id, string userId);

        void Add(Task task);

        void Update(Task task);

        void Delete(int id, string userId);

        void DeleteCategory(int id, string userId);

        void Finish(int id, string userId);
        void AddCategory(Category category);
        void CreateDefaultCategory(string userId);
        new void OnUpdate(string name = null);
    }
}
