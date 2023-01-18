namespace domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        public T Create(T item);
        public T Update(T item);
        bool Delete(int id);
        public bool IsExists(int id);
        public bool IsValid(T entity);
    }
}