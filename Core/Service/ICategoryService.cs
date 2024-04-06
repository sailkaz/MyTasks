using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(string userID);

        Category GetCategory(int categoryId, string userId);

        void Add(Category category);

        void Update(Category category);

        void Delete(int categoryId, string userId);

    }
}