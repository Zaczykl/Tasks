using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tasks.Core;
using Tasks.Core.Models.Domains;
using Tasks.Core.Service;

namespace Tasks.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
           return _unitOfWork.TaskRepository.Get(userId, isExecuted, categoryId, title);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.TaskRepository.GetCategories(userId);
        }

        public Task Get(int id, string userId)
        {
            return _unitOfWork.TaskRepository.Get(id, userId);
        }

        public void Add(Task task)
        {
            _unitOfWork.TaskRepository.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Task task)
        {
            _unitOfWork.TaskRepository.Update(task);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.TaskRepository.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void DeleteCategory(int id, string userId)
        {
            _unitOfWork.TaskRepository.DeleteCategory(id, userId);
            _unitOfWork.Complete();
        }

        public void Finish(int id, string userId)
        {
            _unitOfWork.TaskRepository.Finish(id, userId);
            _unitOfWork.Complete();
        }
        public void AddCategory(Category category)
        {
            _unitOfWork.TaskRepository.AddCategory(category);
            _unitOfWork.Complete();
        }
        public void CreateDefaultCategory(string userId)
        {
            _unitOfWork.TaskRepository.CreateDefaultCategory(userId);
            _unitOfWork.Complete();
        }
        public bool CategoryAlreadyExist(Category category)
        {
            return _unitOfWork.TaskRepository.CategoryAlreadyExist(category);
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.TaskRepository.UpdateCategory(category);
            _unitOfWork.Complete();
        }
    }
}
