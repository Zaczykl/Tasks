using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tasks.Core.Models;
using Tasks.Core.Models.Domains;
using Tasks.Core.Service;
using Tasks.Core.ViewModels;
using Tasks.Persistence.Extensions;

namespace Tasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public IActionResult Tasks()
        {            
            var userId = User.GetUserId();

            var vm = new TasksViewModel
            {
                Tasks = _taskService.Get(userId),
                Categories = _taskService.GetCategories(userId),
                FilterTasks = new FilterTasks()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel vm)
        {
            var userId = User.GetUserId();
            var tasks= _taskService.Get(
                userId,
                vm.FilterTasks.isExecuted,
                vm.FilterTasks.CategoryId,
                vm.FilterTasks.Title);

            return PartialView("_TasksTable", tasks);
        }

        public IActionResult Task(int id=0)
        {
            var userId = User.GetUserId();
            var task = id == 0 ?
                new Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                _taskService.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading=id==0?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                Categories= _taskService.GetCategories(userId)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                    "Dodawanie nowego zadania" : "Edytowanie zadania",
                    Categories = _taskService.GetCategories(userId)
                };
                return View("Task", vm);
            }
            if (task.Id == 0)
                _taskService.Add(task);
            else
                _taskService.Update(task);


            return RedirectToAction("Tasks");            
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new {success=false,message=ex.Message});
            }
            return Json(new {success=true});
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.DeleteCategory(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }
        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var vm = new CategoriesViewModel { Categories = _taskService.GetCategories(userId), Category = new Category { Id = 0 } };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Categories(Category category)
        {            
            if (!ModelState.IsValid )
            {
                var vm = new CategoriesViewModel { Categories = _taskService.GetCategories(User.GetUserId()) };
                return View("Categories", vm);
            }
            bool categoryAlreadyExist = _taskService.CategoryAlreadyExist(category);
            if (category.Id>0 && !categoryAlreadyExist)
            {
                _taskService.UpdateCategory(category);
                return RedirectToAction("Categories");
            }
            if (categoryAlreadyExist)
            {
                var vm = new CategoriesViewModel { Categories = _taskService.GetCategories(User.GetUserId()) };
                ModelState.AddModelError("Name", "Kategoria znajduje się już w bazie danych.");
                return View(vm);
            }
            _taskService.AddCategory(new Category { UserId = User.GetUserId(), Name = category.Name });
            return RedirectToAction("Categories");
        }
    }
}
