using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Stable : Location
    {
        public Stable()
        {
            this.TypeOfLocation = LocationType.Stable;
        }
    }
}
