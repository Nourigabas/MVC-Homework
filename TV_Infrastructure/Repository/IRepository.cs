namespace TV_Infrastructure.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T? Get(Guid Id);
        IList<T> All();
        void SaveChange();
    }
}
