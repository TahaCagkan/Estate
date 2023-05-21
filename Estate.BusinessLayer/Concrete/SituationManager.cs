using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace Estate.BusinessLayer.Concrete
{
    public class SituationManager : ISituationService
    {
        ISituationRepository _situationRepository;

        public SituationManager(ISituationRepository situationRepository)
        {
            _situationRepository = situationRepository;
        }
        public void Add(Situation parameter)
        {
            parameter.Status = true;
            _situationRepository.Add(parameter);
        }

        public void Delete(Situation parameter)
        {
            var status = _situationRepository.GetById(parameter.SituationId);
            status.Status = false;
            _situationRepository.Update(status);
        }

        public List<Situation> GetAllList()
        {
            return _situationRepository.GetAllList();
        }

        public Situation GetById(int id)
        {
            return _situationRepository.GetById(id);
        }

        public List<Situation> GetList(Expression<Func<Situation, bool>> filter)
        {
            return _situationRepository.GetList(filter);
        }

        public void Update(Situation parameter)
        {
            var status = _situationRepository.GetById(parameter.SituationId);
            status.SituationName = parameter.SituationName;
            _situationRepository.Update(status);
        }
    }
}

