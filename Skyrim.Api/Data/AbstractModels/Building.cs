using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.AbstractModels
{
    public abstract class Building
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BuildingType TypeOfBuilding { get; set; }

        [ForeignKey(nameof(LocationId))]
        public int LocationId { get; set; }
    }
}
