using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class ImperialCamp : Location
    {
        public ImperialCamp()
        {
            this.LocationId = LocationType.ImperialCamp;
            this.TypeOfLocation = LocationType.ImperialCamp.GetDisplayName();
        }
    }
}
