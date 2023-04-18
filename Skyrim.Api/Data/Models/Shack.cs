using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Shack : Location
    {
        public Shack()
        {
            this.LocationId = LocationType.Shack;
            this.TypeOfLocation = LocationType.Shack.GetDisplayName();
        }
    }
}
