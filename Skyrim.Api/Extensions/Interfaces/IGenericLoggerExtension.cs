namespace Skyrim.Api.Extensions.Interfaces
{
    public interface IGenericLoggerExtension<T> where T : class
    {
        void LogError(Exception exception, T entity);
    }
}
