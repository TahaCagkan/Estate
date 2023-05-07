using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfImagesRepository : EfGenericRepository<Images, DataContext>, IImagesRepository
    {
        private DataContext context;
        public EfImagesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}