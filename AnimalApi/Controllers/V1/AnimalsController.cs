using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalApi.Models;

using Microsoft.AspNetCore.Authorization;

namespace AnimalApi.Controllers.V1
{
  // [Route("api/[controller]")]
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]

  public class AnimalsController : ControllerBase
  {
    private readonly AnimalApiContext _db;

    public AnimalsController(AnimalApiContext db)
    {
      _db = db;
    }

    // GET: api/v1/animals
    [HttpGet]
    public async Task<IActionResult> Get(string species, string name, int minimumAge, int? page)
    {
      IQueryable<Animal> query = _db.Animals.AsQueryable();

      if (species != null)
      {
        query = query.Where(entry => entry.Species == species);
      }

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (minimumAge > 0)
      {
        query = query.Where(entry => entry.Age >= minimumAge);
      }

      int pageCount = query.Count();
      int pageSize = 3;
      int currentPage = page ?? 1;

      var animals = await query
        .Skip((currentPage - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      var response = new AnimalResponse
      {
        Animals = animals,
        //page number inside the url
        CurrentPage = currentPage,
        //the amount of animals returned from the database
        PageItems  = pageCount,
        //amount of items on the page
        PageSize = pageSize         
      };

      return Ok(response);  
    }

    // GET: api/v1/Animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
      Animal animal = await _db.Animals.FindAsync(id);

      if (animal == null)
      {
        return NotFound();
      }

      return animal;
    }
    
    [Authorize]
    // POST api/v1/animals
    [HttpPost]
    public async Task<ActionResult<Animal>> Post([FromBody] Animal animal)
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);
    }

    [Authorize]
    // PUT: api/v1/Animals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Animal animal)
    {
      if (id != animal.AnimalId)
      {
        return BadRequest();
      }

      _db.Animals.Update(animal);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnimalExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool AnimalExists(int id)
    {
      return _db.Animals.Any(e => e.AnimalId == id);
    }

    [Authorize]
    // DELETE: api/v1/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
      Animal animal = await _db.Animals.FindAsync(id);
      if (animal == null)
      {
        return NotFound();
      }

      _db.Animals.Remove(animal);
      await _db.SaveChangesAsync();

      return NoContent();
    }


  }
}