using Microsoft.AspNetCore.Mvc;
using HotelListing.Api.Data;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotels> hotels = new List<Hotels>
        {
            new Hotels { Id = 1, Name = "Hotel A", Address = "123 Main St", Rating = 4.5 },
            new Hotels { Id = 2, Name = "Hotel B", Address = "456 Elm St", Rating = 3.8 },
            new Hotels { Id = 3, Name = "Hotel C", Address = "789 Oak St", Rating = 4.2 }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Hotels>> Get()
        {
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public ActionResult<Hotels> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotels> Post([FromBody] Hotels newHotel)
        {
            if(hotels.Any(h => h.Id == newHotel.Id))
            {
                return BadRequest("Hotel with the same ID already exists.");
            }
            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        [HttpPut("{id}")]
        public ActionResult<Hotels> Put(int id,[FromBody] Hotels updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null)
            {
                return NotFound();
            }
            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." });
            }
            hotels.Remove(hotel);
            return Ok(new {message="Hotel Deleted"});
        }
    }
}
