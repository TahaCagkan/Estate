using Estate.CoreLayer.Abstract;
using Estate.EntityLayer.Entities;

namespace Estate.DataAccessLayer.Abstract
{
    public interface IAdvertRepository: IRepository<Advert>
    {
        public void FullDelete(Advert parameter);
    }
}
