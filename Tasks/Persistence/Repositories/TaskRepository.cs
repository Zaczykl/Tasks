using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Tasks.Core;
using Tasks.Core.Models.Domains;
using Tasks.Core.Repositories;

namespace Tasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IApplicationDbContext _context;
        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        

        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);
            
            if (categoryId!=0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

            if (!tasks.Any())
            {
                return null;
            }
            return tasks.OrderBy(x => x.Term).ToList();
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            var categories= _context.Categories.Where(x => x.UserId == userId).OrderBy(x => x.Name).ToList();
            return categories;
        }

        public Task Get(int id, string userId)
        {
            return _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);

            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.Description = task.Description;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Term = task.Term;
            taskToUpdate.Title = task.Title;            
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId==userId);
            _context.Tasks.Remove(taskToDelete);
        }

        public void DeleteCategory(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            _context.Categories.Remove(categoryToDelete);
        }

        public void Finish(int id, string userId)
        {
            var taskToFinish = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            taskToFinish.IsExecuted = true;
        } 
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void CreateDefaultCategory(string userId)
        {
            var defaultCategory = new Category { Name = "Ogólna", UserId = userId };
            _context.Categories.Add(defaultCategory);
        }
        public bool CategoryAlreadyExist(Category category)
        {
            return _context.Categories.Any(x => x.Name == category.Name && x.Id != category.Id);
        }
        public void UpdateCategory(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id);
            categoryToUpdate.Name=category.Name;
        }
    }
}
