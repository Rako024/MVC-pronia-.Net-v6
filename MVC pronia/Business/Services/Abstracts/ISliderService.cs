using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ISliderService
    {
        void CreateSlider(Slider slider);
        void DeleteSlider(int id);
        void UpdateSlider(int id, Slider slider);
        Slider GetSlider(Func<Slider,bool>? func=null);
        List<Slider> GetAllSliders(Func<Slider, bool>? func=null);
    }
}
