using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class StandingStone : Location
    {
        public StandingStone()
        {
            this.TypeOfLocation = LocationType.StandingStone;
        }
    }
}
