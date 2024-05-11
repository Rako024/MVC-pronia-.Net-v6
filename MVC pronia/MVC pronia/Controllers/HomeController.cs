using Business.Services.Abstracts;
using Business.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using MVC_pronia.ViewModels.Home;

namespace MVC_pronia.Controllers
{
    public class HomeController : Controller
    {
        ISliderService _sliderService;
        IProductService _productService;

        public HomeController(ISliderService sliderService, IProductService productService)
        {
            _sliderService = sliderService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.Products = _productService.GetAllProducts();
            vm.Sliders = _sliderService.GetAllSliders();
            return View(vm);
        }
    }
}
