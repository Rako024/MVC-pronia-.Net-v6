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
    public class SliderService : ISliderService
    {
        ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public void CreateSlider(Slider slider)
        {
            _sliderRepository.Add(slider);
            _sliderRepository.Commit();
        }

        public void DeleteSlider(int id)
        {
            Slider existSlider = _sliderRepository.Get(x=>x.Id == id);
            if (existSlider != null)
            {
                _sliderRepository.Delete(existSlider);
                _sliderRepository.Commit();
                return;
            }
            throw new NotFoundSliderException("Bele bir Slider Yoxdur!");
            
        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

        public void UpdateSlider(int id, Slider slider)
        {
            Slider oldSlider = _sliderRepository.Get(x => x.Id == id);
            if (oldSlider != null)
            {
                oldSlider.Title = slider.Title;
                oldSlider.SubTitle = slider.SubTitle;
                oldSlider.Description = slider.Description;
                oldSlider.ImgUrl = slider.ImgUrl;
                _sliderRepository.Commit();
                return ;
            }
            throw new NotFoundSliderException("Bele bir Slider Yoxdur!");
        }
    }
}
