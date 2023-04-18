using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Castle : Location
    {
        public Castle()
        {
            this.LocationId = LocationType.Castle;
            this.TypeOfLocation = LocationType.Castle.GetDisplayName();
        }
    }
}
