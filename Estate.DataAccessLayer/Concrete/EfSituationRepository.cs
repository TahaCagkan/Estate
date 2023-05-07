using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfSituationRepository : EfGenericRepository<Situation, DataContext>, ISituationRepository
    {
        private DataContext context;
        public EfSituationRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}