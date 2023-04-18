using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class DragonLair : Location
    {
        public DragonLair()
        {
            this.LocationId = LocationType.DragonLair;
            this.TypeOfLocation = LocationType.DragonLair.GetDisplayName();
        }
    }
}
