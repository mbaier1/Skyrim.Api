using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Tomb : Location
    {
        public Tomb()
        {
            this.TypeOfLocation = LocationType.Tomb;
        }
    }
}
