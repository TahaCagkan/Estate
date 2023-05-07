using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class CityManager : IGenericService<City>
    {
        ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
          public void Add(City parameter)
        {
            _cityRepository.Add(parameter);
        }

        public void Delete(City parameter)
        {
            _cityRepository.Delete(parameter);
        }

        public List<City> GetAllList()
        {
            return _cityRepository.GetAllList();
        }

        public City GetById(int id)
        {
            return _cityRepository.GetById(id);
        }

        public List<City> GetList(Expression<Func<City, bool>> filter)
        {
          return _cityRepository.GetList(filter);
        }

        public void Update(City parameter)
        {
            _cityRepository.Update(parameter);
        }
    }
}
