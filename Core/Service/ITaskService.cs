namespace MyTasks.Core.Service
{
    public interface ITaskService
    {
        IEnumerable<Core.Models.Domains.Task> GetTasks(string userId,
            bool isExecuted = false, int categoryId = 0, string title = null);

        Models.Domains.Task GetTask(int taskId, string userId);

        void Add(Models.Domains.Task task);

        void Update(Models.Domains.Task task);

        void Delete(int taskId, string userId);

        void Finish(int taskId, string userId);

    }
}
