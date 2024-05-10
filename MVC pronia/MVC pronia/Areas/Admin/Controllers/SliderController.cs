using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        ISliderService sliderService;
        IWebHostEnvironment _webHostEnvironment;



        public SliderController(ISliderService sliderService, IWebHostEnvironment webHostEnvironment)
        {
            this.sliderService = sliderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Slider> sliderList = sliderService.GetAllSliders();
            return View(sliderList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!slider.PhotoFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile", "File Formati Duzgun Deyil!!!");
                return View();
            }

            string path = @"C:\Users\Rashid\Desktop\MVC-pronia-.Net-v6\MVC pronia\MVC pronia\wwwroot\upload\Slider\";
            string fileName = slider.PhotoFile.FileName;
            using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
            {
                slider.PhotoFile.CopyTo(stream);
            }
            slider.ImgUrl = fileName;   
            if (!ModelState.IsValid)
            {
                return View();
            }
            sliderService.CreateSlider(slider);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Slider slider = sliderService.GetSlider(x=>x.Id == id);

            string path = _webHostEnvironment.WebRootPath + @"\upload\Slider\" + slider.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            sliderService.DeleteSlider(id);
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            Slider slider = sliderService.GetSlider(x=>x.Id == id);
            
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {

            Slider oldSlider = sliderService.GetSlider(x=>x.Id ==slider.Id);
            if(slider.PhotoFile == null)
            {
                slider.ImgUrl = oldSlider.ImgUrl;
            }else if (!slider.PhotoFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile", "File Formati Duzgun Deyil!!!");
                return View();
            }
            else
            {
                string path = @"C:\Users\Rashid\Desktop\MVC-pronia-.Net-v6\MVC pronia\MVC pronia\wwwroot\upload\Slider\";
                string fileName = slider.PhotoFile.FileName;
                using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
                {
                    slider.PhotoFile.CopyTo(stream);
                }
                slider.ImgUrl = fileName;
            }
           
            sliderService.UpdateSlider(slider.Id, slider);
            return RedirectToAction("Index");
        }
    }
}
