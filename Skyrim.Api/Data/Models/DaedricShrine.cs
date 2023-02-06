using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class DaedricShrine : Location
    {
        public DaedricShrine()
        {
            this.TypeOfLocation = LocationType.DaedricShrine;
        }
    }
}
