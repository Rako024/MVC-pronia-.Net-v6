using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace MVC_pronia.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    IProductService _productService;
    ICategoryService _categoryService;
    IWebHostEnvironment _webHostEnvironment;

    public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> products = _productService.GetAllProducts();
        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _categoryService.GetAllCategorys();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product) 
    {
        if (!product.MainPhotoFile.ContentType.Contains("image/"))
        {
            ModelState.AddModelError("MainPhotoFile", "File Formati duzgun deyil!!!");
            return View();
        }
        if (!product.PhotoFile.ContentType.Contains("image/"))
        {
            ModelState.AddModelError("PhotoFile", "File Formati duzgun deyil!!!");
            return View();
        }

        string path = @"C:\\Users\\Rashid\\Desktop\\MVC-pronia-.Net-v6\\MVC pronia\\MVC pronia\\wwwroot\\upload\\Product\\";
        string mainImgName = product.MainPhotoFile.FileName;
        string ImgName = product.PhotoFile.FileName;
        using(FileStream file = new FileStream(path+mainImgName, FileMode.Create))
        {
            product.MainPhotoFile.CopyTo(file);
        }
        using (FileStream file = new FileStream(path + ImgName, FileMode.Create))
        {
            product.PhotoFile.CopyTo(file);
        }
        product.MainImgUrl = mainImgName;
        product.ImgUrl = ImgName;

            ViewBag.Categories = _categoryService.GetAllCategorys();
        //if(!ModelState.IsValid)
        //{
        //    return View();
        //}
        _productService.CreateProduct(product);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        Product product = _productService.GetProduct(x => x.Id == id);

        string path = _webHostEnvironment.WebRootPath + @"\upload\Product\" + product.MainImgUrl;
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        string path1 = _webHostEnvironment.WebRootPath + @"\upload\Product\" + product.ImgUrl;
        FileInfo fileInfo1 = new FileInfo(path1);
        if (fileInfo1.Exists)
        {
            fileInfo1.Delete();
        }
        _productService.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        ViewBag.Categories = _categoryService.GetAllCategorys();
        Product product = _productService.GetProduct(x=>x.Id == id);
        return View(product);
    }
    [HttpPost]
    public IActionResult Update(Product product)
    {
        ViewBag.Categories = _categoryService.GetAllCategorys();
        Product oldProduct = _productService.GetProduct(x=> x.Id == product.Id);
        if (product.PhotoFile == null)
        {
            product.ImgUrl = oldProduct.ImgUrl;
        }
        else if (!product.PhotoFile.ContentType.Contains("image/"))
        {
            ModelState.AddModelError("PhotoFile", "File Formati Duzgun Deyil!!!");
            return View();
        }
        else
        {
            string path = @"C:\Users\Rashid\Desktop\MVC-pronia-.Net-v6\MVC pronia\MVC pronia\wwwroot\upload\Product\";
            string fileName = product.PhotoFile.FileName;
            using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
            {
                product.PhotoFile.CopyTo(stream);
            }
            product.ImgUrl = fileName;
        }


        
        if (product.MainPhotoFile == null)
        {
            product.MainImgUrl = oldProduct.MainImgUrl;
        }
        else if (!product.MainPhotoFile.ContentType.Contains("image/"))
        {
            ModelState.AddModelError("MainPhotoFile", "File Formati Duzgun Deyil!!!");
            return View();
        }
        else
        {
            string path = @"C:\Users\Rashid\Desktop\MVC-pronia-.Net-v6\MVC pronia\MVC pronia\wwwroot\upload\Product\";
            string fileName = product.MainPhotoFile.FileName;
            using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
            {
                product.MainPhotoFile.CopyTo(stream);
            }
            product.MainImgUrl = fileName;
        }
        _productService.UpdateProduct(product.Id, product);
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id)
    {
        Product product = _productService.GetProduct(x => x.Id == id);
        return View(product);
    }

    
}
