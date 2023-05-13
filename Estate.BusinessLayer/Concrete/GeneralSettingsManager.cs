using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class GeneralSettingsManager : IGeneralSettingsService
    {
        IGeneralSettingsRepository _generalSettingsRepository;

        public GeneralSettingsManager(IGeneralSettingsRepository generalSettingsRepository)
        {
            _generalSettingsRepository = generalSettingsRepository;
        }
        public void Add(GeneralSettings parameter)
        {
            _generalSettingsRepository.Add(parameter);
        }

        public void Delete(GeneralSettings parameter)
        {
            _generalSettingsRepository.Delete(parameter);
        }

        public List<GeneralSettings> GetAllList()
        {
            return _generalSettingsRepository.GetAllList();
        }

        public GeneralSettings GetById(int id)
        {
            return _generalSettingsRepository.GetById(id);
        }

        public List<GeneralSettings> GetList(Expression<Func<GeneralSettings, bool>> filter)
        {
            return _generalSettingsRepository.GetList(filter);
        }

        public void Update(GeneralSettings parameter)
        {
            _generalSettingsRepository.Update(parameter);
        }
    }
}
