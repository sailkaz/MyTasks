using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Service;

namespace MyTasks.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Core.Models.Domains.Task> GetTasks(string userId,
            bool isExecuted = false, int categoryId = 0, string title = null)
        {
            return _unitOfWork.TaskRep.GetTasks(userId, isExecuted, categoryId, title);
        }

        public Core.Models.Domains.Task GetTask(int taskId, string userId)
        {
            return _unitOfWork.TaskRep.GetTask(taskId, userId);
        }

        public void Add(Core.Models.Domains.Task task)
        {
            _unitOfWork.TaskRep.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Core.Models.Domains.Task task)
        {
            _unitOfWork.TaskRep.Update(task);
            _unitOfWork.Complete();
        }

        public void Delete(int taskId, string userId)
        {
            _unitOfWork.TaskRep.Delete(taskId, userId);
            _unitOfWork.Complete();
        }

        public void Finish(int taskId, string userId)
        {
            _unitOfWork.TaskRep.Finish(taskId, userId);
            _unitOfWork.Complete();
        }
    }
}
