using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class StormcloakCamp : Location
    {
        public StormcloakCamp()
        {
            this.LocationId = LocationType.StormcloakCamp;
            this.TypeOfLocation = LocationType.StormcloakCamp.GetDisplayName();
        }
    }
}
