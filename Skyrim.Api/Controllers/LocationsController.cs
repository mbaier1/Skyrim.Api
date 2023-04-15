using Microsoft.AspNetCore.Mvc;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;

namespace Skyrim.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationDomain _locationDomain;

        public LocationsController(ILocationDomain locationDomain)
        {
            _locationDomain = locationDomain;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
        {
            var result = await _locationDomain.GetLocation();
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Location/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var result = await _locationDomain.GetLocation(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            //if (id != location.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(location).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    //if (!LocationExists(id))
            //    //{
            //    //    return NotFound();
            //    //}
            //    //else
            //    //{
            //    //    throw;
            //    //}
            //}

            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(CreateLocationDto creatLocationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var location = await _locationDomain.CreateLocation(creatLocationDto);

            if (location == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetLocation), new { id = location.Id}, new
            {
                location.Id,
                location.Name,
                location.Description,
                location.GeographicalDescription,
                location.TypeOfLocation
            });
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            //var location = await _context.Location.FindAsync(id);
            //if (location == null)
            //{
            //    return NotFound();
            //}

            //_context.Location.Remove(location);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
