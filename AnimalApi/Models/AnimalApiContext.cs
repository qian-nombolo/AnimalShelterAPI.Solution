using Microsoft.EntityFrameworkCore;

namespace AnimalApi.Models
{
  public class AnimalApiContext : DbContext
  {
    public DbSet<Animal> Animals { get; set; }

    public AnimalApiContext(DbContextOptions<AnimalApiContext> options) : base(options)
    {
    }
    
  }
}