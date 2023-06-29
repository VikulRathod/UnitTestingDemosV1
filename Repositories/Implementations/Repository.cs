using DAL;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected AppDbContext _db;
        public Repository(AppDbContext db)
        {
            _db = db;
        }
        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }
        public void Delete(object Id)
        {
            TEntity entity = _db.Set<TEntity>().Find(Id);
            if (entity != null)
                _db.Set<TEntity>().Remove(entity);
        }
        public TEntity Find(object Id)
        {
            return _db.Set<TEntity>().Find(Id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}
