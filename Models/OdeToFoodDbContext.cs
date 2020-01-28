using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Models
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
            
        }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}