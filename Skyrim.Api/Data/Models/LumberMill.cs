using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class LumberMill : Location
    {
        public LumberMill()
        {
            this.TypeOfLocation = LocationType.LumberMill;
        }
    }
}
