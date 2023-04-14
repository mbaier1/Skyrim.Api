using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class StormcloakCamp : Location
    {
        public StormcloakCamp()
        {
            this.TypeOfLocation = LocationType.StormcloakCamp;
        }
    }
}
