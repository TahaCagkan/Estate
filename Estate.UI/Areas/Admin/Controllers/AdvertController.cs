using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.ValidationRules;
using Estate.EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdvertController : Controller
    {
        IAdvertService _advertService;
        ICityService _cityService;
        IDistrictService _districtService;
        INeighbourhoodService _neighbourhoodService;
        ISituationService _situationService;
        ITypeService _typeService;

        IWebHostEnvironment _hostEnvironment;
        public AdvertController(IAdvertService advertService, ICityService cityService, IDistrictService districtService, INeighbourhoodService neighbourhoodService, ISituationService situationService, ITypeService typeService, IWebHostEnvironment hostEnvironment) 
        {
            _advertService = advertService;
            _cityService = cityService;
            _districtService = districtService;
            _neighbourhoodService = neighbourhoodService;
            _situationService = situationService;
            _typeService = typeService;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("Id");

            var list = _advertService.GetList(x => x.Status == true && x.UserAdminId == id);

            return View(list);
        }
        public IActionResult Create()
        {
            ViewBag.userid= HttpContext.Session.GetString("Id");
            DropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Advert data) 
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var dosyayolu = Path.Combine(_hostEnvironment.WebRootPath, "img");

                    foreach (var item in data.Image)
                    {
                        var tamDosyaAdi = Path.Combine(dosyayolu, item.FileName);

                        using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                        {
                            item.CopyTo(dosyaAkisi);
                        }

                        data.Images.Add(new Images { ImageName = item.FileName, Status = true });
                    }

                    _advertService.Add(data);

                    //TempData["Success"] = "İlan Ekleme İşlemi Başarıyla Gerçekleşti";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            DropDown();
            return View();
        }
        //şehir get işlemi
        public List<City> CityGet()
        {
            List<City> cities = _cityService.GetList(x => x.Status == true);
            return cities;
        }
        //durum bilgisi get işlemi
        public List<Situation> SituationGet()
        {
            List<Situation> situations = _situationService.GetList(x => x.Status == true);
            return situations;
        }
        //Yukarıdan şehir seçip o şehre ait semt bilgisi seçilicektir.
        public IActionResult DistrictGet(int Cityid)
        {
            List<District> districtList = _districtService.GetList(x => x.Status == true && x.CityId == Cityid);

            ViewBag.district = new SelectList(districtList, "DistrictId", "DistrictName");

            return PartialView("DistrictPartial");
        }

        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }
        //Semte ait Komşusunu seçme
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighbourhoodList = _neighbourhoodService.GetList(x => x.Status == true && x.DistrictId == DistrictId);

            ViewBag.neighbourhood = new SelectList(neighbourhoodList, "NeighbourhoodId", "NeighbourhoodName");

            return PartialView("NeighbourhoodPartial");
        }
        public PartialViewResult NeighbourhoodPartial()
        {
            return PartialView();
        }

        //Yukarıdan semt seçip o şehre ait tip bilgisi seçilicektir.
        public IActionResult TypeGet(int SituationId)
        {
            List<EntityLayer.Entities.Type> typeList = _typeService.GetList(x => x.Status == true && x.SituationId == SituationId);

            ViewBag.type = new SelectList(typeList, "TypeId", "TypeName");

            return PartialView("TypePartial");
        }
        public PartialViewResult TypePartial()
        {
            return PartialView();
        }
        public void DropDown()
        {
            ViewBag.citylist = new SelectList(CityGet(),"CityId","CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
        }
        
    }
}
