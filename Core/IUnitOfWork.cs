using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Core
{
    public interface IUnitOfWork
    {
        public ITaskRepository TaskRep { get; }
        public ICategoryRepository CategoryRep { get; }


        void Complete();
    }
}
