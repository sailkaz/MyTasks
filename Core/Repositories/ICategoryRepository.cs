using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string userId);

        Category GetCategory(int categoryId, string userId);

        void Add(Category category);

        void Update(Category category);

        void Delete(int categoryId, string userId);
    }
}
