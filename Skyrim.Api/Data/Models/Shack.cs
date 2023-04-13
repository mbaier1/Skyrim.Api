using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Shack : Location
    {
        public Shack()
        {
            this.TypeOfLocation = LocationType.Shack;
        }
    }
}
