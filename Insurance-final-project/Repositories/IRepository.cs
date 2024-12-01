
namespace Insurance_final_project.Repositories
{
    public interface IRepository<T>
    {
        public T Add(T entity);
        public IQueryable<T> GetAll();
        public T Update(T entity);
        public T Get(Guid id);
        public int Delete(T entity);
    }
}
