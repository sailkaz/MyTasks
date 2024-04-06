using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.Service;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using MyTasks.Persistence.Services;

namespace MyTasks.Controllers
{
    [Authorize]

    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;


        public TaskController(ITaskService taskService, ICategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
        }


        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var tasksViewModel = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = _taskService.GetTasks(userId),
                Categories = _categoryService.GetCategories(userId)
            };
            return View(tasksViewModel);
        }


        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskService.GetTasks(userId, viewModel.FilterTasks.IsExecuted,
                                                 viewModel.FilterTasks.CategoryId, viewModel.FilterTasks.Title);

            return PartialView("_TaskTable", tasks);
        }

        public IActionResult Task(int taskId = 0)
        {
            var userId = User.GetUserId();

            var task = taskId == 0 ?
                 new Core.Models.Domains.Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                 _taskService.GetTask(taskId, userId);

            var taskViewModel = new TaskViewModel
            {
                Task = task,
                Heading = taskId == 0 ?
                          "Dodawanie nowego zadania" : "Edycja zadania",
                Categories = _categoryService.GetCategories(userId)
            };

            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Core.Models.Domains.Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var taskViewModel = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                          "Dodawanie nowego zadania" : "Edycja zadania",
                    Categories = _categoryService.GetCategories(userId)
                };

                return View("Task", taskViewModel);
            }

            if (task.Id == 0)
                _taskService.Add(task);
            else
                _taskService.Update(task);

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Delete(taskId, userId);
            }
            catch (Exception ex)
            {
                // Logowanie błędu do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Finish(int taskId)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(taskId, userId);
            }
            catch (Exception ex)
            {
                // Logowanie błędu do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}