using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfGeneralSettingsRepository : EfGenericRepository<GeneralSettings, DataContext>, IGeneralSettingsRepository
    {
        private DataContext context;
        public EfGeneralSettingsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}