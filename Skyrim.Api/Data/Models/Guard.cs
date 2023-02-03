using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Guard : Patroller
    {
        public Guard()
        {
            this.TypeOfPerson = PersonType.Guard;
        }
    }
}
