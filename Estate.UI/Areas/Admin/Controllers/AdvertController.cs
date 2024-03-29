﻿using Estate.BusinessLayer.Abstract;
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
        private readonly IAdvertService _advertService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly INeighbourhoodService _neighbourhoodService;
        private readonly ISituationService _situationService;
        private readonly ITypeService _typeService;
        private readonly IImagesService _imagesService;

        IWebHostEnvironment _hostEnvironment;
        public AdvertController(IAdvertService advertService, ICityService cityService, IDistrictService districtService, INeighbourhoodService neighbourhoodService, ISituationService situationService, ITypeService typeService, IWebHostEnvironment hostEnvironment, IImagesService imagesService)
        {
            _advertService = advertService;
            _cityService = cityService;
            _districtService = districtService;
            _neighbourhoodService = neighbourhoodService;
            _situationService = situationService;
            _typeService = typeService;
            _hostEnvironment = hostEnvironment;
            _imagesService = imagesService;
        }

        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("Id");

            var list = _advertService.GetList(x => x.Status == true && x.UserAdminId == id);

            return View(list);
        }
        public IActionResult AdvertAll()
        {
            string id = HttpContext.Session.GetString("Id");

            var list = _advertService.GetList(x => x.Status == true && x.UserAdminId != id);
            return View(list);
        }
        //ilgili ilandaki detay resimler
        public IActionResult ImageList(int id)
        {
            var list = _imagesService.GetList(x => x.Status == true && x.AdvertId == id);

            return View(list);
        }
        //ilgili ilana resim ekleme işlemi get
        public IActionResult ImageCreate(int id)
        {
            var advert = _advertService.GetById(id);
            return View(advert);
        }
        //ilgili ilana resim ekleme işlemi post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageCreate(Advert data)
        {
            var advert = _advertService.GetById(data.AdvertId);
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

                    _imagesService.Add(new Images { ImageName = item.FileName, Status = true,AdvertId = advert.AdvertId });
                }

                TempData["Success"] = "İlan'a Resim Ekleme İşlemi Başarıyla Gerçekleştirildi.";
                return RedirectToAction("Index");
            }
            return View(advert);
        }
        //detay listesi silme işlemi
        public IActionResult ImageDelete(int id)
        {
            var delete = _imagesService.GetById(id);
            _imagesService.Delete(delete);
            return RedirectToAction("Index");
        }
        //detay listesi güncelleme işlemi
        public IActionResult ImageUpdate(int id)
        {
            var image = _imagesService.GetById(id);
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageUpdate(Images data)
        {        
                ImagesValidation validationRules = new ImagesValidation();
                ValidationResult result = validationRules.Validate(data);
                if (result.IsValid)
                {
                    if (data.Image != null)
                    {
                        var dosyaYolu = Path.Combine(_hostEnvironment.WebRootPath, "img");

                        var tamDosyaAdi = Path.Combine(dosyaYolu, data.Image.FileName);
                        using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                        {
                            data.Image.CopyTo(dosyaAkisi);
                        }
                        _imagesService.Update(data);
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

                return View();         
        }

        public IActionResult DeleteList()
        {
            string id = HttpContext.Session.GetString("Id");

            var list = _advertService.GetList(x => x.Status == false && x.UserAdminId == id);

            return View(list);
        }
        //Ekleme Get
        public IActionResult Create()
        {
            ViewBag.userid = HttpContext.Session.GetString("Id");
            DropDown();
            return View();
        }
        //Ekleme Post
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

                    TempData["Success"] = "İlan Ekleme İşlemi Başarıyla Gerçekleştirildi.";
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

        //Güncelleme işlemi
        public IActionResult Update(int id)
        {
            ViewBag.userid = HttpContext.Session.GetString("Id");
            DropDown();
            var advert = _advertService.GetById(id);
            return View(advert);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Advert data)
        {

            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                _advertService.Update(data);
                TempData["Update"] = "İlan Güncelleme İşlemi Başarıyla Gerçekleştirildi.";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            DropDown();
            return View(data);
        }
        //Silme işlemi
        public IActionResult Delete(int id)
        {
            var sessionUser = HttpContext.Session.GetString("Id");
            var delete = _advertService.GetById(id);
            if (sessionUser.ToString() == delete.UserAdminId)
            {
                _advertService.Delete(delete);
                return RedirectToAction("Index");
            }
            return View();
        }
        //Silineni geri yükle işlemi
        public IActionResult RestoreDeleted(int id)
        {
            var sessionUser = HttpContext.Session.GetString("Id");
            var delete = _advertService.GetById(id);
            if (sessionUser.ToString() == delete.UserAdminId)
            {
                _advertService.RestoreDelete(delete);
                TempData["RestoreDelete"] = "İlan Geri Yükleme İşlemi Başarıyla Gerçekleştirildi";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult FullDeleted(int id)
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
