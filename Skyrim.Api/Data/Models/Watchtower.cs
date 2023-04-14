using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Watchtower : Location
    {
        public Watchtower()
        {
            this.TypeOfLocation = LocationType.Watchtower;
        }
    }
}
