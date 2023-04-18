using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Dock : Location
    {
        public Dock()
        {
            this.LocationId = LocationType.Dock;
            this.TypeOfLocation = LocationType.Dock.GetDisplayName();
        }
    }
}
