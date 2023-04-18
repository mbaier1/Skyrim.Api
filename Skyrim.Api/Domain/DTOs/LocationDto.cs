using Skyrim.Api.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Skyrim.Api.Domain.DTOs
{
    public class LocationDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public LocationType LocationId { get; set; }
        [Required]
        public string GeographicalDescription { get; set; }
    }
}
