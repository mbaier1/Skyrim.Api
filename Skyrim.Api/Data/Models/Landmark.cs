using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Landmark : Location
    {
        public Landmark()
        {
            this.LocationId = LocationType.Landmark;
            this.TypeOfLocation = LocationType.Landmark.GetDisplayName();
        }
    }
}
