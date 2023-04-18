using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class DwarvenRuin : Location
    {
        public DwarvenRuin()
        {
            this.LocationId = LocationType.DwarvenRuin;
            this.TypeOfLocation = LocationType.DwarvenRuin.GetDisplayName();
        }
    }
}
