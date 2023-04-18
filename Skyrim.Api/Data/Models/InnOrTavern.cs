using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class InnOrTavern : Location
    {
        public InnOrTavern()
        {
            this.LocationId = LocationType.InnOrTavern;
            this.TypeOfLocation = LocationType.InnOrTavern.GetDisplayName();
        }
    }
}
