using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class BodyOfWater : Location
    {
        public BodyOfWater()
        {
            this.TypeOfLocation = LocationType.BodyOfWater;
        }
    }
}
