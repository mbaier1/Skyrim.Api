using Skyrim.Api.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Skyrim.Api.Domain.DTOs
{
    public class CreateLocationDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public LocationType TypeOfLocation { get; set; }
        [Required]
        public string GeographicalDescription { get; set; }
    }
}
