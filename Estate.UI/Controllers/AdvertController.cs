using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Estate.BusinessLayer.Abstract;
using Estate.EntityLayer.Entities;

namespace Estate.UI.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IImagesService im;
        private readonly IAdvertService advert;
        private readonly ICityService cityService;
        private readonly IDistrictService districtService;
        private readonly INeighbourhoodService neighbourhoodService;
        private readonly ISituationService situationService;
        private readonly ITypeService typeService;
        
        public AdvertController(IImagesService im, IAdvertService advert, ICityService cityService, IDistrictService districtService, INeighbourhoodService neighbourhoodService, ISituationService situationService, ITypeService typeService)
        {
            this.im = im;
            this.advert = advert;
            this.cityService = cityService;
            this.districtService = districtService;
            this.situationService = situationService;
            this.typeService = typeService;
            this.neighbourhoodService = neighbourhoodService;
        }

        public IActionResult Details(int id)
        {
            var detail = advert.GetById(id);

            var image = im.GetList(x => x.AdvertId == id);
            ViewBag.imgs=image;
            return View(detail);
        }

        public IActionResult AdvertRent()
        {
            DropDown();
            var rent = advert.GetList(x => x.Type.Situation.SituationName == "Kiralık");

            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            return View(rent);
        }

        public IActionResult AdvertSale()
        {
            DropDown();
            var rent = advert.GetList(x => x.Type.Situation.SituationName == "Satılık");

            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            return View(rent);
        }
     
        public List<City> CityGet()
        {
            List<City> cities = cityService.GetList(x => x.Status == true);
            return cities;
        }
        public List<Situation> SituationGet()
        {
            List<Situation> situation = situationService.GetList(x => x.Status == true);
            return situation;
        }
       
        public void DropDown()
        {
            ViewBag.citylist = new SelectList(CityGet(), "CityId", "CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");

            List<SelectListItem> value1 = (from i in districtService.GetList(X => X.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.DistrictName,
                                               Value = i.DistrictId.ToString()
                                           }).ToList();
            ViewBag.district = value1;

            List<SelectListItem> value2 = (from i in neighbourhoodService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.NeighbourhoodName,
                                               Value = i.NeighbourhoodId.ToString()
                                           }).ToList();
            ViewBag.neighbourhood = value2;
            List<SelectListItem> value3 = (from i in typeService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.TypeName,
                                               Value = i.TypeId.ToString()
                                           }).ToList();
            ViewBag.type = value3;

            List<SelectListItem> value4 = (from i in situationService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.SituationName,
                                               Value = i.SituationId.ToString()
                                           }).ToList();
            ViewBag.situation = value4;
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
