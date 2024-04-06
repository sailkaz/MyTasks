using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Service;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence.Extensions;

namespace MyTasks.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Categories()
        {
            var userId = User.GetUserId();

            var categoryViewModel = new CategoryViewModel
            {
                Categories = _categoryService.GetCategories(userId)
            };

            return View(categoryViewModel);
        }


        public IActionResult Category(int categoryId)
        {
            var userId = User.GetUserId();

            var category = categoryId == 0 ?
                new Category { Id = 0, UserId = userId } :
                _categoryService.GetCategory(categoryId, userId);

            var categoryViewModel = new CategoryViewModel
            {
                Category = category,
                Heading = categoryId == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii",
                Categories = _categoryService.GetCategories(userId)
            };

            return View(categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            ModelState.Remove("category.UserId");

            if (!ModelState.IsValid)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Category = category,
                    Heading = category.Id == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii",
                };

                return View("Category", categoryViewModel);
            }

            if (category.Id == 0)
                _categoryService.Add(category);
            else
                _categoryService.Update(category);

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult Delete(int categoryId)
        {
            try
            {
                var userId = User.GetUserId();
                _categoryService.Delete(categoryId, userId);
            }
            catch (Exception ex)
            {
                // Logowanie błędu do pliku
                return Json(new { success = false, message = ex.Message }); ;
            }

            return Json(new { success = true });
        }
    }
}
