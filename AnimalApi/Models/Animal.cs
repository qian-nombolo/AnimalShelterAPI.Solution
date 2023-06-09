
using System.ComponentModel.DataAnnotations;

namespace AnimalApi.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name must be between 0 and 20 characters.")] 
    public string Name { get; set; }
    [Required]
    [StringLength(20)] 
    public string Species { get; set; }
    [StringLength(20)] 
    public string Breed { get; set; }

    [Range(0, 20, ErrorMessage = "Age must be between 0 and 20.")]
    public int Age { get; set; }

    public DateTime AdoptionDate { get; set; }

  }
}