using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using Location = Skyrim.Api.Data.AbstractModels.Location;

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
            var location = await _locationDomain.GetLocation();
            if (location == null)
                return NotFound();

            return Ok(location);
        }

        // GET: api/Location/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _locationDomain.GetLocation(id);
            if (location == null)
                return NotFound();

            return Ok(location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Location>> UpdateLocation(int id, LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updatedLocation = await _locationDomain.UpdateLocation(id, locationDto);
            if(updatedLocation == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetLocation), new { id = updatedLocation.Id }, new
            {
                updatedLocation.Id,
                updatedLocation.Name,
                updatedLocation.Description,
                updatedLocation.GeographicalDescription,
                updatedLocation.LocationId,
                updatedLocation.TypeOfLocation
            });
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var location = await _locationDomain.CreateLocation(locationDto);

            if (location == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetLocation), new { id = location.Id}, new
            {
                location.Id,
                location.Name,
                location.Description,
                location.GeographicalDescription,
                location.LocationId,
                location.TypeOfLocation
            });
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteLocation(int id)
        {
            var wasLocationDeleted = await _locationDomain.DeleteLocation(id);
            if (!wasLocationDeleted)
                return BadRequest();

            return Ok();
        }
    }
}
