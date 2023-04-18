using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Cave : Location
    {
        public Cave()
        {
            this.LocationId = LocationType.Cave;
            this.TypeOfLocation = LocationType.Cave.GetDisplayName();
        }
    }
}
