using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            List<Category> categories = categoryService.GetAllCategorys();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryService.CreateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id) 
        {
            Category category = categoryService.GetCategory(x=>x.Id == id);
            if(category != null)
            {
                return View(category);
            }
            throw new NotFoundCategoryException("Bele Id-li Category Yoxdur");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            categoryService.UpdateCategory(category.Id,category);
            return RedirectToAction(nameof (Index));
        }
    }
}
