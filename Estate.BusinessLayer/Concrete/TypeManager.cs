using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class TypeManager : ITypeService
    {
        ITypeRepository _typeRepository;

        public TypeManager(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public void Add(EntityLayer.Entities.Type parameter)
        {
            _typeRepository.Add(parameter);
        }

        public void Delete(EntityLayer.Entities.Type parameter)
        {
            _typeRepository.Delete(parameter);
        }

        public List<EntityLayer.Entities.Type> GetAllList()
        {
            return _typeRepository.GetAllList();
        }

        public EntityLayer.Entities.Type GetById(int id)
        {
            return _typeRepository.GetById(id);
        }

        public List<EntityLayer.Entities.Type> GetList(Expression<Func<EntityLayer.Entities.Type, bool>> filter)
        {
            return _typeRepository.GetList(filter);
        }

        public void Update(EntityLayer.Entities.Type parameter)
        {
            _typeRepository.Update(parameter);
        }
    }
}

