using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    
    //ENUM type is used to hold the type of cuisine 
    //of each restaurant but you can also use a second model if you like
    public enum CuisineType
    {
        None,
        African,
        Italian,
        Indian,
        Mexican
        
        
    }
    public class Restaurant
    {
        public int Id { get; set; }
        
        [Required, MaxLength(80)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; } // reference to the enum cuisinetype
    }
}