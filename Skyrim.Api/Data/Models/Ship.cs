using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Ship : Location
    {
        public Ship()
        {
            this.LocationId = LocationType.Ship;
            this.TypeOfLocation = LocationType.Ship.GetDisplayName();
        }
    }
}
