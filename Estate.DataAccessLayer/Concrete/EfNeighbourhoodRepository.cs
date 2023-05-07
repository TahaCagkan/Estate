using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfNeighbourhoodRepository : EfGenericRepository<Neighbourhood, DataContext>, INeighbourhoodRepository
    {
        private DataContext context;
        public EfNeighbourhoodRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
