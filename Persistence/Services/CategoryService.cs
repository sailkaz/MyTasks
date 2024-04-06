using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Service;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.CategoryRep.GetCategories(userId);
        }


        public Category GetCategory(int categoryId, string userId)
        {
            return _unitOfWork.CategoryRep.GetCategory(categoryId, userId);
        }


        public void Update(Category category)
        {
            _unitOfWork.CategoryRep.Update(category);
            _unitOfWork.Complete();
        }


        public void Add(Category category)
        {
            _unitOfWork.CategoryRep.Add(category);
            _unitOfWork.Complete();
        }


        public void Delete(int categoryId, string userId)
        {
            _unitOfWork.CategoryRep.Delete(categoryId, userId);
            _unitOfWork.Complete();
        }
    }
}
