using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class DistrictManager : IDistrictService
    {
        IDistrictRepository _districtRepository;

        public DistrictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        public void Add(District parameter)
        {
            parameter.Status = true;
            _districtRepository.Add(parameter);
        }

        public void Delete(District parameter)
        {
            var delete = _districtRepository.GetById(parameter.DistrictId);
            delete.Status = false;
            _districtRepository.Update(delete);
        }

        public List<District> GetAllList()
        {
            return _districtRepository.GetAllList();
        }

        public District GetById(int id)
        {
            return _districtRepository.GetById(id);
        }

        public List<District> GetList(Expression<Func<District, bool>> filter)
        {
            return _districtRepository.GetList(filter);
        }

        public void Update(District parameter)
        {
            var district = _districtRepository.GetById(parameter.DistrictId);
            district.DistrictName = parameter.DistrictName;
            district.CityId = parameter.CityId;
            _districtRepository.Update(district);
        }
    }
}
