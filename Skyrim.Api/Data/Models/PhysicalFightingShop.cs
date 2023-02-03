using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class PhyscialFightingShop : Shop
    {
        public bool HasBlackSmithStation { get; set; }
        public PhyscialFightingShop()
        {
            this.TypeOfShop = ShopType.PhysicalArmorWeaponsAndMaterials;
        }
    }
}
