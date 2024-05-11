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
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Commit();
        }

        public void DeleteProduct(int id)
        {
            Product existProduct = _productRepository.Get(x=> x.Id == id);
            if (existProduct != null)
            {
                _productRepository.Delete(existProduct);
                _productRepository.Commit();
                return;
            }
            throw new NotFoundProductException("Bele bir Product Yoxdur!!!");
        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            return _productRepository.GetAll(func);
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            return _productRepository.Get(func);
        }

        public void UpdateProduct(int id, Product product)
        {
            Product oldProduct = _productRepository.Get(x => x.Id == id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Price = product.Price;
                oldProduct.Color = product.Color;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.Count = product.Count;
                oldProduct.MainImgUrl = product.MainImgUrl;
                oldProduct.ImgUrl = product.ImgUrl;
                oldProduct.Size = product.Size;
                _productRepository.Commit();
                return;
            }
            throw new NotFoundProductException("Bele bir Product Yoxdur!!!");
        }
    }
}
