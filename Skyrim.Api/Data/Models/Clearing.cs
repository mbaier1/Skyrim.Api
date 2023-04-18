using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Clearing : Location
    {
        public Clearing()
        {
            this.LocationId = LocationType.Clearing;
            this.TypeOfLocation = LocationType.Clearing.GetDisplayName();
        }
    }
}
