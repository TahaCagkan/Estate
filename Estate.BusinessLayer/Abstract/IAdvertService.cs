using Estate.EntityLayer.Entities;

namespace Estate.BusinessLayer.Abstract
{
    public interface IAdvertService : IGenericService<Advert>
    {
        public void RestoreDelete(Advert parameter);

        public void FullDelete(Advert parameter);
    }
}
