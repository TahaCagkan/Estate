using Estate.BusinessLayer.Abstract;
using Estate.EntityLayer.Entities;
using Estate.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Estate.UI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAdvertService advertService;
        private readonly ICityService cityService;
        private readonly IDistrictService districtService;
        private readonly INeighbourhoodService neighbourhoodService;
        private readonly ISituationService situationService;
        private readonly ITypeService typeService;
        private readonly IImagesService imageService;
        public HomeController(IImagesService imageService, IAdvertService advertService, ICityService cityService, IDistrictService districtService, INeighbourhoodService neighbourhoodService, ISituationService situationService, ITypeService typeService)
        {
            this.advertService = advertService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.neighbourhoodService = neighbourhoodService;
            this.situationService = situationService;
            this.typeService = typeService;
            this.imageService = imageService;
        }
        public IActionResult Index(int page = 1)
        {
            DropDown();
            //var list = advertService.GetList(x => x.Status == true).ToPagedList(page, 3);
            var list = advertService.GetList(x => x.Status == true);
            var images = imageService.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            return View(list);
        }
        //Filtreleme işlemleri
        public IActionResult Filter(int min, int max, int cityid, int typeid, int neighbourhoodid, int districtid, int situtationid)
        {
            DropDown();
            var imagelist = imageService.GetList(x => x.Status == true);
            ViewBag.imagelist = imagelist;
            var filter = advertService.GetList(x => x.Price >= min && x.Price <= max && x.CityId == cityid && x.TypeId == typeid && x.NeighbourhoodId == neighbourhoodid && x.DistrictId == districtid && x.SituationId == situtationid);
            return View(filter);
        }
        public PartialViewResult PartialFiltre()
        {
            DropDown();
            return PartialView();
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
        public IActionResult DistrictGet(int CityId)
        {
            List<District> districtlist = districtService.GetList(X => X.Status == true && X.CityId == CityId);

            ViewBag.district = new SelectList(districtlist, "DistrictId", "DistrictName");
            return PartialView("DistrictPartial");
        }

        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }

        public PartialViewResult TypePartial()
        {
            return PartialView();
        }

        public PartialViewResult NeighbourhoodPartial()
        {
            return PartialView();
        }
        public IActionResult TypeGet(int SituationId)
        {
            List<EntityLayer.Entities.Type> typelist = typeService.GetList(X => X.Status == true && X.SituationId == SituationId);

            ViewBag.type = new SelectList(typelist, "TypeId", "TypeName");
            return PartialView("TypePartial");
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighlist = neighbourhoodService.GetList(X => X.Status == true && X.DistrictId == DistrictId);

            ViewBag.neigh = new SelectList(neighlist, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
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
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

