using Insurance_final_project.Data;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InsuranceContext _context;
        private readonly DbSet<T> _table;

        public Repository(InsuranceContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public T Add(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public int Delete(T entity)
        {
            _table.Remove(entity);
            return _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return _table.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public T Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
    }
}
