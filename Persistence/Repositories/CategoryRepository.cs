using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _context;

        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).OrderBy(x => x.Name).ToList();
        }

        public Category GetCategory(int categoryId, string userId)
        {
            return _context.Categories.Single(x => x.Id == categoryId && x.UserId == userId);
        }

        public void Update(Category category)
        {
            var categoryToUpdate = GetCategory(category.Id, category.UserId);
            categoryToUpdate.Name = category.Name;
        }

        public void Add(Category category)
        { 
            _context.Categories.Add(category);
        }

        public void Delete(int categoryId, string userId)
        {
            var categoryToDelete = GetCategory(categoryId, userId);
            _context.Categories.Remove(categoryToDelete);
        }
    }
}
