namespace AnimalApi.Models
{
  public class AnimalResponse
  {
    public List<Animal> Animals { get; set; }
    public int PageItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }
 
}