using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class OrcStronghold : Location
    {
        public OrcStronghold()
        {
            this.LocationId = LocationType.OrcStronghold;
            this.TypeOfLocation = LocationType.OrcStronghold.GetDisplayName();
        }
    }
}
