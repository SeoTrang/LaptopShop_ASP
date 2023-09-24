using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;

namespace LapTopShop.ViewModels
{
    public class HomeViewModel
    {
        

        public List<Product> SaleHotProducts { get; set; }
        public List<Product> OfficeLaptops { get; set; }
        public List<Product> GamingLaptops { get; set; }
        public List<Product> GraphicsLaptops { get; set; }
        public List<Product> LaptopForYou { get; set; }
        public List<Category> Categories { get; set; }

        
        

    }
}