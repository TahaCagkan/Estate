using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class AdvertManager : IGenericService<Advert>
    {
        IAdvertRepository _advertRepository;

        public AdvertManager(IAdvertRepository  advertRepository)
        {
            _advertRepository= advertRepository;
        }
        public void Add(Advert parameter)
        {
            _advertRepository.Add(parameter);
        }

        public void Delete(Advert parameter)
        {
            _advertRepository.Delete(parameter);
        }

        public List<Advert> GetAllList()
        {
            return _advertRepository.GetAllList();
        }

        public Advert GetById(int id)
        {
            return _advertRepository.GetById(id);
        }

        public List<Advert> GetList(Expression<Func<Advert, bool>> filter)
        {
          return _advertRepository.GetList(filter);
        }

        public void Update(Advert parameter)
        {
            _advertRepository.Update(parameter);
        }
    }
}
