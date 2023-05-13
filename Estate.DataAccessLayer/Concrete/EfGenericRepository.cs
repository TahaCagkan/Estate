using Estate.CoreLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfGenericRepository<T, TContext> : IRepository<T> where T : class, new() where TContext : DbContext, new()
    {
        private DataContext context = new DataContext();
        //private readonly TContext context;
        DbSet<T> _object;
        public EfGenericRepository(DataContext context)
        {
            this.context = context;
            _object = context.Set<T>();
        }
        public void Add(T parameter)
        {
            _object.Add(parameter);
            context.SaveChanges();
        }

        public void Delete(T parameter)
        {
            _object.Remove(parameter);
            context.SaveChanges();
        }

        public List<T> GetAllList()
        {
           return _object.ToList();
        }

        public T GetById(int id)
        {
           return _object.Find(id);
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T parameter)
        {
            _object.Update(parameter);
            context.SaveChanges();
        }
    }
}
