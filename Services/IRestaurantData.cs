using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    // services directory holds all our repositories
    
    // THOUGH THE CLASSES ARE CREATED IN ONE FILE YOU CAN SEPARATE THEM IN INDIVIDUAL FILES
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);

        void Add(Restaurant newRestaurant);

        void Delete(int id);
        void Update(int id,Restaurant restaurant);
    }
    
    //SQLRestaurantData holds the actual implementation of the IRestaurantData Interface of how
    // our data will be stored in the DB

    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;
        

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Restaurant> GetAll()
        {
           return  _context.Restaurants.ToList();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id); 
        }

        public void Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Restaurant restaurant;
            restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            _context.Remove(restaurant);
            _context.SaveChanges();
        }

        public void Update(int id,Restaurant restaurant)
        {
            Restaurant rest;
            rest = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            rest = restaurant;
            
            _context.Update(rest);
            _context.SaveChanges();
            
        }
    }

    // In Memory data is Used just for testing before we store all our database in actual SQLite DB
    // it holds temporary CRUD logic for testing
    class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name = "Meze Fresh"},
                new Restaurant{Id = 2, Name = "Camellia"},
                new Restaurant{Id = 3, Name = "Riders Lounge"},
                new Restaurant{Id = 4, Name = "Soleluna"},
                new Restaurant{Id = 5, Name = "Cannaberra"}
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);
        }
     
        public void Delete(int id)
        {
            Restaurant restaurant;
            restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            _restaurants.Remove(restaurant);
        }
        public void Update(int id,Restaurant restaurant)
        {
            Restaurant rest;
            rest = _restaurants.FirstOrDefault(r => r.Id == id);
            rest = restaurant;
            
            var index = _restaurants.FindIndex(c => c.Id == 1);
            _restaurants[index] = rest;
            
        }
        
        static List<Restaurant> _restaurants;
    }
}