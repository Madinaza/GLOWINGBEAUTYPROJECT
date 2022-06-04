using GLOWING_FinalProject.Models;
using System.Collections.Generic;

namespace GLOWING_FinalProject.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }

        public List<News> News { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List <Product> Products { get; set; }
    }
}
