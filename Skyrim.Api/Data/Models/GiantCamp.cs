using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class GiantCamp : Location
    {
        public GiantCamp()
        {
            this.TypeOfLocation = LocationType.GiantCamp;
        }
    }
}
