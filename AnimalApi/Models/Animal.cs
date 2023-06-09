namespace AnimalApi;

namespace AnimalApi.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    [StringLength(20)] 
    public string Name { get; set; }
    [Required]
    [StringLength(20)] 
    public string Species { get; set; }
    public int Age { get; set; }
    public DateTime Date { get; set; }
    
  }
}