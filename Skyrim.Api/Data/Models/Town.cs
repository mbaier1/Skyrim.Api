using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Town : Location
    {
        public Town()
        {
            this.LocationId = LocationType.Town;
            this.TypeOfLocation = LocationType.Town.GetDisplayName();
        }
    }
}