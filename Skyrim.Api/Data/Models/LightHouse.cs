using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class LightHouse : Location
    {
        public LightHouse()
        {
            this.LocationId = LocationType.LightHouse;
            this.TypeOfLocation = LocationType.LightHouse.GetDisplayName();
        }
    }
}
