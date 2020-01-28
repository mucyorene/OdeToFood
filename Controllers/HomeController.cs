using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        // GET
        public IActionResult Index()
        {
            // var resto = new Restaurant{Id = 1, Name = "Meze Fresh"};
            //var resto = _restaurantData.GetAll();
            var resto = new HomeViewModel();
            resto.Restaurants = _restaurantData.GetAll();
            return View(resto);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant {Name = model.Name, Cuisine = model.Cuisine};
                _restaurantData.Add(restaurant);

                // return View("Details", restaurant);

                return RedirectToAction("Details", new {id = restaurant.Id}); // TODO :fix redirection to details

            }

            return View();
        }
        
        
        

        public IActionResult Details(int id)
        {
            var resto = _restaurantData.Get(id);

            if (resto == null)
            {
                return RedirectToAction("Index");
            }

            return View(resto);
        }

        public IActionResult Delete(int id)
        {
            var resto = _restaurantData.Get(id);
            
            if (ModelState.IsValid)
            {
                _restaurantData.Delete(id);
                return RedirectToAction("Index", "Home"); // TODO :fix redirection to details
            }

            if (resto == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        
        public IActionResult Edit(int id,Restaurant restaurant)
        {
            var resto = _restaurantData.Get(id);
            if (resto == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var r = new Restaurant
                {
                    Id = id,
                    Name = restaurant.Name,
                    Cuisine = restaurant.Cuisine

                };
                _restaurantData.Update(id,r);
                
            }
            
            return View(resto);
        }

       
        
    }
}