using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Ship : Location
    {
        public Ship()
        {
            this.TypeOfLocation = LocationType.Ship;
        }
    }
}
