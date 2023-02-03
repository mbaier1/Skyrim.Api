using Skyrim.Api.Domain.Interfaces;

namespace Skyrim.Api.Domain.DomainHelpers
{
    public class GenericFormatHelper<T> : IGenericFormatHelper<T> where T : class
    {
        public object FormatEntity(T entity)
        {
            return entity;
        }
    }
}
