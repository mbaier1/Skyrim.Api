using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Farm : Location
    {
        public Farm()
        {
            this.LocationId = LocationType.Farm;
            this.TypeOfLocation = LocationType.Farm.GetDisplayName();
        }
    }
}
