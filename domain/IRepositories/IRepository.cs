namespace domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(int id);
        public bool IsExists(int id);
        public bool IsValid(T entity);
        void Save();
    }
}