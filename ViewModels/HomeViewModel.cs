using System.Collections.Generic;
using OdeToFood.Models;

namespace OdeToFood.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}