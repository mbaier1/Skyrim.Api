using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Temple : Location
    {
        public Temple()
        {
            this.LocationId = LocationType.Temple;
            this.TypeOfLocation = LocationType.Temple.GetDisplayName();
        }
    }
}
