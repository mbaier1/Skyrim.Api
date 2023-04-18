using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Ruin : Location
    {
        public Ruin()
        {
            this.LocationId = LocationType.Ruin;
            this.TypeOfLocation = LocationType.Ruin.GetDisplayName();
        }
    }
}