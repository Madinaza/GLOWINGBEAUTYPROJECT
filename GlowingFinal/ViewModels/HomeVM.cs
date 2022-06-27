using GlowingFinal.Models;
using System.Collections.Generic;

namespace GlowingFinal.ViewModels
{
    public class HomeVM:VmBase
    {
        public List<Slider> Sliders { get; set; }

        public List<News> News { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List <Product> Products { get; set; }
        public List<string> Favourite { get; set; }
        //public List<AllInfoToProduct> AllInfoToProducts { get; set; }
    }
}
