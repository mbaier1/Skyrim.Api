using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class DragonLair : Location
    {
        public DragonLair()
        {
            this.TypeOfLocation = LocationType.DragonLair;
        }
    }
}
