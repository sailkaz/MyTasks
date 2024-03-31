using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
