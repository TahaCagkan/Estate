using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfTypeRepository : EfGenericRepository<EntityLayer.Entities.Type, DataContext>, ITypeRepository
    {
        private DataContext context;
        public EfTypeRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}