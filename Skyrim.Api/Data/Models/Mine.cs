using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Mine : Location
    {
        public Mine()
        {
            this.TypeOfLocation = LocationType.Mine;
        }
    }
}
