using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class LightHouse : Location
    {
        public LightHouse()
        {
            this.TypeOfLocation = LocationType.LightHouse;
        }
    }
}
