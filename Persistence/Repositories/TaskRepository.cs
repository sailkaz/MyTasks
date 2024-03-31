using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IApplicationDbContext _context;

        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Core.Models.Domains.Task> GetTasks(string userId,
            bool isExecuted = false, int categoryId = 0, string title = null)
        {
            var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.UserId == userId &&
                       x.IsExecuted == isExecuted);

            if (categoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

            return tasks.OrderBy(x => x.Term).ToList();
        }

        public Core.Models.Domains.Task GetTask(int taskId, string userId)
        {
            var task = _context.Tasks.Single(x => x.UserId == userId && x.Id == taskId);
            return task;
        }

        public void Add(Core.Models.Domains.Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Core.Models.Domains.Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id && x.UserId == task.UserId);
            taskToUpdate.Title = task.Title;
            taskToUpdate.Term = task.Term;
            taskToUpdate.Description = task.Description;
            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.IsExecuted = task.IsExecuted;
        }

        public void Delete(int taskId, string userId)
        {
            var taskToRemove = _context.Tasks.Single(x => x.Id == taskId && x.UserId == userId);

            _context.Tasks.Remove(taskToRemove);
        }

        public void Finish(int taskId, string userId)
        {
            var taskToFinish = _context.Tasks.Single(x => x.Id == taskId && x.UserId == userId);
            taskToFinish.IsExecuted = true;
        }
    }
}
