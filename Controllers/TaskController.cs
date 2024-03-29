using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Controllers
{
    //[Authorize]

    public class TaskController : Controller
    {
        private readonly TaskRepository _taskRepository;
        private readonly CategoryRepository _categoryRepository;


        public TaskController(ApplicationDbContext context)
        {
            _taskRepository = new TaskRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }


        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var tasksViewModel = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = _taskRepository.GetTasks(userId),
                Categories = _categoryRepository.GetCategories()
            };
            return View(tasksViewModel);
        }


        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskRepository.GetTasks(userId, viewModel.FilterTasks.IsExecuted,
                                                 viewModel.FilterTasks.CategoryId, viewModel.FilterTasks.Title);

            return View("_TaskTable", tasks);
        }

        public IActionResult Task(int taskId = 0)
        {
            var userId = User.GetUserId();

            var task = taskId == 0 ?
                 new Core.Models.Domains.Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                 _taskRepository.GetTask(taskId, userId);

            var taskViewModel = new TaskViewModel
            {
                Task = task,
                Heading = taskId == 0 ?
                          "Dodawanie nowego zadania" : "Edycja zadania",
                Categories = _categoryRepository.GetCategories()
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
                    Categories = _categoryRepository.GetCategories()
                };

                return View("Task", taskViewModel);
            }

            if (task.Id == 0)
                _taskRepository.Add(task);
            else
                _taskRepository.Update(task);

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            try
            {
                var userId = User.GetUserId();
                _taskRepository.Delete(taskId, userId);
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
                _taskRepository.Finish(taskId, userId);
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