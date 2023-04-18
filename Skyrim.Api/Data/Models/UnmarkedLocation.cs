using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class UnmarkedLocation : Location
    {
        public UnmarkedLocation()
        {
            this.LocationId = LocationType.UnmarkedLocation;
            this.TypeOfLocation = LocationType.UnmarkedLocation.GetDisplayName();
        }
    }
}
