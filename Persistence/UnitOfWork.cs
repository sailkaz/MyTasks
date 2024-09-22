using MyTasks.Core;
using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            TaskRep = new TaskRepository(context);
            CategoryRep = new CategoryRepository(context);
        }

        public ITaskRepository TaskRep { get; set; }
        public ICategoryRepository CategoryRep { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
