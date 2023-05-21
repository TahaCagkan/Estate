using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class CityManager : ICityService
    {
        ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public void Add(City parameter)
        {
            parameter.Status = true;
            _cityRepository.Add(parameter);
        }

        public void Delete(City parameter)
        {
            var city = _cityRepository.GetById(parameter.CityId);
            city.Status =false;
            _cityRepository.Update(city);
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
            var city = _cityRepository.GetById(parameter.CityId);
            city.CityName = parameter.CityName;
            _cityRepository.Update(city);
        }
    }
}
