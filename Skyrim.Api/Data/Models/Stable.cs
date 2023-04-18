using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Stable : Location
    {
        public Stable()
        {
            this.LocationId = LocationType.Stable;
            this.TypeOfLocation = LocationType.Stable.GetDisplayName();
        }
    }
}
