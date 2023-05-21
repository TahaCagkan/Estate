using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class NeighbourhoodManager : INeighbourhoodService
    {
        INeighbourhoodRepository _neighbourhoodRepository;

        public NeighbourhoodManager(INeighbourhoodRepository neighbourhoodRepository)
        {
            _neighbourhoodRepository = neighbourhoodRepository;
        }
        public void Add(Neighbourhood parameter)
        {
            parameter.Status = true;
            _neighbourhoodRepository.Add(parameter);
        }

        public void Delete(Neighbourhood parameter)
        {
            var neigh = _neighbourhoodRepository.GetById(parameter.NeighbourhoodId);
            neigh.Status = false;
            _neighbourhoodRepository.Update(neigh);
        }

        public List<Neighbourhood> GetAllList()
        {
            return _neighbourhoodRepository.GetAllList();
        }

        public Neighbourhood GetById(int id)
        {
            return _neighbourhoodRepository.GetById(id);
        }

        public List<Neighbourhood> GetList(Expression<Func<Neighbourhood, bool>> filter)
        {
            return _neighbourhoodRepository.GetList(filter);
        }

        public void Update(Neighbourhood parameter)
        {
            var neigh = _neighbourhoodRepository.GetById(parameter.NeighbourhoodId);
            neigh.NeighbourhoodName = parameter.NeighbourhoodName;
            neigh.DistrictId = parameter.DistrictId;
            _neighbourhoodRepository.Update(neigh);
        }
    }
}
