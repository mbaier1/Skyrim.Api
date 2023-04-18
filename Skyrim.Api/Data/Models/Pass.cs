using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Pass : Location
    {
        public Pass()
        {
            this.LocationId = LocationType.Pass;
            this.TypeOfLocation = LocationType.Pass.GetDisplayName();
        }
    }
}
