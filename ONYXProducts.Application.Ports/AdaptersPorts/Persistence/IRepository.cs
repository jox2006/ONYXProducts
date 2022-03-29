namespace ONYXProducts.Application.AdaptersPorts.Persistence
{
    //Interface between APPLICATION - PERSISTENCE ADAPTER layers in Clean Architecture.
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> filter);
    }
}