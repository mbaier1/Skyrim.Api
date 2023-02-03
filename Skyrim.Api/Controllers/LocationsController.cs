using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skyrim.Api.Data;
using Skyrim.Api.Data.AbstractModels;
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

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
        {
            //return await _context.Location.ToListAsync();
            return Ok();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            //var location = await _context.Location.FindAsync(id);

            //if (location == null)
            //{
            //    return NotFound();
            //}

            //return location;
            return Ok();
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

            return CreatedAtAction("GetLocation", new
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
