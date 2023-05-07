using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfCityRepository : EfGenericRepository<City, DataContext>, ICityRepository
    {
        private DataContext context;
        public EfCityRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
