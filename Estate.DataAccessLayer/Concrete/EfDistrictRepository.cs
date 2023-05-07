using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfDistrictRepository : EfGenericRepository<District, DataContext>, IDistrictRepository
    {
        private DataContext context;
        public EfDistrictRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
