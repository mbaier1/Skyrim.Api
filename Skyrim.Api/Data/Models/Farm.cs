using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Farm : Location
    {
        public Farm()
        {
            this.TypeOfLocation = LocationType.Farm;
        }
    }
}
