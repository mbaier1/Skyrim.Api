using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Camp : Location
    {
        public Camp()
        {
            this.LocationId = LocationType.Camp;
            this.TypeOfLocation = LocationType.Camp.GetDisplayName();

        }
    }
}
