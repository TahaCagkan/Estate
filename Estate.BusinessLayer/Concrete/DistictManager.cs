using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class DistictManager : IGenericService<District>
    {
        IDistrictRepository _districtRepository;

        public DistictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }
        public void Add(District parameter)
        {
            _districtRepository.Add(parameter);
        }

        public void Delete(District parameter)
        {
            _districtRepository.Delete(parameter);
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
            _districtRepository.Update(parameter);
        }
    }
}
