using Microsoft.OpenApi.Writers;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Shop : Building
    {
        public ShopType TypeOfShop { get; set; }

        public Shop()
        {
            this.TypeOfBuilding = BuildingType.Shop;
        }
    }
}
