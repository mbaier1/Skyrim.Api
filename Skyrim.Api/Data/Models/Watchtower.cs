using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Watchtower : Location
    {
        public Watchtower()
        {
            this.LocationId = LocationType.Watchtower;
            this.TypeOfLocation = LocationType.Watchtower.GetDisplayName();
        }
    }
}
