using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Grove : Location
    {
        public Grove()
        {
            this.TypeOfLocation = LocationType.Grove;
        }
    }
}
