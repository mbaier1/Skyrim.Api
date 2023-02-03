using Microsoft.Build.Framework;

namespace Skyrim.Api.Domain.DTOs
{
    public class GetLocationDto
    {
        [Required]
        public int Id { get; set; }
    }
}
