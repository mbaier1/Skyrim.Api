namespace Skyrim.Api.Domain.Interfaces
{
    public interface IGenericFormatHelper<T> where T : class
    {
        object FormatEntity(T entity);
    }
}
