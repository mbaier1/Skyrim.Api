using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class UnmarkedLocation : Location
    {
        public UnmarkedLocation()
        {
            this.TypeOfLocation = LocationType.UnmarkedLocation;
        }
    }
}
