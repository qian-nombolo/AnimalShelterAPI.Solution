using Microsoft.EntityFrameworkCore;
using System;

namespace AnimalApi.Models
{
  public class AnimalApiContext : DbContext
  {
    public DbSet<Animal> Animals { get; set; }

    public AnimalApiContext(DbContextOptions<AnimalApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Animal>()
        .HasData(
          new Animal { AnimalId = 1, Name = "Tiger", Species = "Dog", Breed = "Selberian Husky", Age = 1, AdoptionDate = new DateTime (2023, 1, 1) },

          new Animal { AnimalId = 2, Name = "Rexie", Species = "Dog", Breed = "Bulldog", Age = 2, AdoptionDate = new DateTime (2022, 12, 1)},

          new Animal { AnimalId = 3, Name = "Matilda", Species = "Dog", Breed = "Goldern Retriever", Age = 4, AdoptionDate = new DateTime(2023, 3, 1)},

          new Animal { AnimalId = 4, Name = "Pip", Species = "Cat", Breed = "British Shorthair", Age = 5, AdoptionDate = new DateTime(2023, 2, 3)},

          new Animal { AnimalId = 5, Name = "Bartholomew", Species = "Cat", Breed = "Ragdoll", Age = 3, AdoptionDate = new DateTime(2023, 6, 3)},

          new Animal { AnimalId = 6, Name = "Jasmine", Species = "Cat", Breed = "Birman", Age = 1, AdoptionDate = new DateTime(2023, 4, 3)}

        );
    }

  }
}