using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.Commit();
        }

        public void DeleteCategory(int id)
        {
            Category existCategory = _categoryRepository.Get(x=>x.Id == id);
            if(existCategory != null)
            {
                _categoryRepository.Delete(existCategory);
                _categoryRepository.Commit();
                return;
            }
            throw new NotFoundCategoryException("Bele bir category yoxdur!!!");
        }

        public List<Category> GetAllCategorys(Func<Category, bool>? func = null)
        {
            return _categoryRepository.GetAll(func);
        }

        public Category GetCategory(Func<Category, bool>? func = null)
        {
            return _categoryRepository.Get(func);
        }

        public void UpdateCategory(int id, Category category)
        {
            Category oldCategory = _categoryRepository.Get(x=> x.Id == id);
            if(oldCategory != null)
            {
                oldCategory.Name = category.Name;
                _categoryRepository.Commit();
                return ;
            }
            throw new NotFoundCategoryException("Bele bir category yoxdur!!!");
        }
    }
}
