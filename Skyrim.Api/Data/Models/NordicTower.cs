using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class NordicTower : Location
    {
        public NordicTower()
        {
            this.LocationId = LocationType.NordicTower;
            this.TypeOfLocation = LocationType.NordicTower.GetDisplayName();
        }
    }
}
