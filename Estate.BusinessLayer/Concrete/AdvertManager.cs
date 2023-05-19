using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class AdvertManager : IAdvertService
    {
        IAdvertRepository _advertRepository;

        public AdvertManager(IAdvertRepository  advertRepository)
        {
            _advertRepository= advertRepository;
        }
        public void Add(Advert parameter)
        {
            parameter.Status = true;
            parameter.AdvertDate=DateTime.Now;
            _advertRepository.Add(parameter);
        }

        public void Delete(Advert parameter)
        {
            var delete = _advertRepository.GetById(parameter.AdvertId);
            parameter.Status = false;
            _advertRepository.Update(delete);
        }

        public void FullDelete(Advert parameter)
        {
            var delete = _advertRepository.GetById(parameter.AdvertId);
            _advertRepository.FullDelete(delete);
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

        public void RestoreDelete(Advert parameter)
        {
            var delete = _advertRepository.GetById(parameter.AdvertId);
            parameter.Status = true;
            _advertRepository.Update(delete);
        }

        public void Update(Advert parameter)
        {
            var advert = _advertRepository.GetById(parameter.AdvertId);
            advert.Address = parameter.Address;
            advert.Description = parameter.Description;
            advert.BathroomNumbers= parameter.BathroomNumbers;
            advert.NumberOfrooms=parameter.NumberOfrooms;
            advert.Floor=parameter.Floor;
            advert.AirCordinator=parameter.AirCordinator;
            advert.Garage=parameter.Garage;
            advert.Garden=parameter.Garden;
            advert.Furniture=parameter.Furniture;
            advert.FirePlace=parameter.FirePlace;
            advert.Area=parameter.Area;
            advert.Pool=parameter.Pool;
            advert.Teras=parameter.Teras;
            advert.DistrictId=parameter.DistrictId;
            advert.CityId=parameter.CityId;
            advert.Neighbourhood=parameter.Neighbourhood;
            advert.PhoneNumber=parameter.PhoneNumber;
            advert.AdvertDate=DateTime.Now;
            advert.SituationId = parameter.SituationId;
            _advertRepository.Update(advert);
        }
    }
}
