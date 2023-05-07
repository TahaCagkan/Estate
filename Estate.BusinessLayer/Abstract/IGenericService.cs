using System.Linq.Expressions;

namespace Estate.BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        List<T> GetAllList();
        List<T> GetList(Expression<Func<T, bool>> filter);
        T GetById(int id);
        void Add(T parameter);
        void Delete(T parameter);
        void Update(T parameter);
    }
}
