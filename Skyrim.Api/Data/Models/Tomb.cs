using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Tomb : Location
    {
        public Tomb()
        {
            this.LocationId = LocationType.Tomb;
            this.TypeOfLocation = LocationType.Tomb.GetDisplayName();
        }
    }
}
