using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.ValidationRules;
using Estate.EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Estate.UI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly INeighbourhoodService _neighbourhoodService;
        private readonly ISituationService _situationService;
        private readonly ITypeService _typeService;
        private readonly IImagesService _imageService;
        IWebHostEnvironment _hostEnvironment;
        public AdvertController(IAdvertService advertService, ICityService cityService, IDistrictService districtService, INeighbourhoodService neighbourhoodService, ISituationService situationService, ITypeService typeService, IImagesService imageService, IWebHostEnvironment hostEnvironment)
        {
            _advertService = advertService;
            _cityService = cityService;
            _districtService = districtService;
            _neighbourhoodService = neighbourhoodService;
            _situationService = situationService;
            _typeService = typeService;
            _imageService = imageService;
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
            ViewBag.userid = HttpContext.Session.GetString("Id");
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);
            string id = HttpContext.Session.GetString("Id");
            if (result.IsValid)
            {
                if (data.Image != null && data.UserAdminId == id)
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

                    TempData["Success"] = "İlan Ekleme İşlemi Başarıyla Gerçekleşti";
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

        public IActionResult Delete(int id)
        {
            var sessionuser = HttpContext.Session.GetString("Id");

            var delete = _advertService.GetById(id);

            if (sessionuser.ToString() == delete.UserAdminId)
            {
                _advertService.Delete(delete);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteList()
        {
            string id = HttpContext.Session.GetString("Id");

            var list = _advertService.GetList(x => x.Status == false && x.UserAdminId == id);
            return View(list);
        }

        public IActionResult RestoreDeleted(int id)
        {
            var sessionuser = HttpContext.Session.GetString("Id");

            var delete = _advertService.GetById(id);

            if (sessionuser.ToString() == delete.UserAdminId)
            {
                _advertService.RestoreDelete(delete);
                TempData["RestoreDelete"] = "İlan Geri Yükleme Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult FullDelete(int id)
        {
            var sessionuser = HttpContext.Session.GetString("Id");

            var delete = _advertService.GetById(id);

            if (sessionuser.ToString() == delete.UserAdminId)
            {
                _advertService.FullDelete(delete);

                return RedirectToAction("Index");
            }
            return View();
        }

        public List<City> CityGet()
        {
            List<City> cities = _cityService.GetList(x => x.Status == true);
            return cities;
        }
        public List<Situation> SituationGet()
        {
            List<Situation> situation = _situationService.GetList(x => x.Status == true);
            return situation;
        }
        public IActionResult DistrictGet(int CityId)
        {
            List<District> districtlist = _districtService.GetList(X => X.Status == true && X.CityId == CityId);

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
            List<EntityLayer.Entities.Type> typelist = _typeService.GetList(X => X.Status == true && X.SituationId == SituationId);

            ViewBag.type = new SelectList(typelist, "TypeId", "TypeName");
            return PartialView("TypePartial");
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighlist = _neighbourhoodService.GetList(X => X.Status == true && X.DistrictId == DistrictId);

            ViewBag.neigh = new SelectList(neighlist, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
        }
        public void DropDown()
        {
            ViewBag.citylist = new SelectList(CityGet(), "CityId", "CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");

            List<SelectListItem> value1 = (from i in _districtService.GetList(X => X.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.DistrictName,
                                               Value = i.DistrictId.ToString()
                                           }).ToList();
            ViewBag.district = value1;

            List<SelectListItem> value2 = (from i in _neighbourhoodService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.NeighbourhoodName,
                                               Value = i.NeighbourhoodId.ToString()
                                           }).ToList();
            ViewBag.neighbourhood = value2;
            List<SelectListItem> value3 = (from i in _typeService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.TypeName,
                                               Value = i.TypeId.ToString()
                                           }).ToList();
            ViewBag.type = value3;

            List<SelectListItem> value4 = (from i in _situationService.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.SituationName,
                                               Value = i.SituationId.ToString()
                                           }).ToList();
            ViewBag.situation = value4;
        }
    }
}
