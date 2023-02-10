using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Clearing : Location
    {
        public Clearing()
        {
            this.TypeOfLocation = LocationType.Clearing;
        }
    }
}
