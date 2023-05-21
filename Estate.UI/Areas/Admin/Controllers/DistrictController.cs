using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.ValidationRules;
using Estate.EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Estate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DistrictController : Controller
    {
        IDistrictService _districtService;
        ICityService _cityService;
        public DistrictController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;

        }
        public IActionResult Index()
        {
            var list = _districtService.GetList(x => x.Status == true);

            return View(list);
        }

        public IActionResult Create()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(District data)
        {
            DistrictValidation validationRules = new DistrictValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _districtService.Add(data);
                TempData["Success"] = "Semt Ekleme İşlemi Başarıyla Gerçekleştirildi.";
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
            return View();
        }
        public IActionResult Delete(int id)
        {
            var district = _districtService.GetById(id);
            _districtService.Delete(district);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            DropDown();
            var update = _districtService.GetById(id);
            return View(update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(District data)
        {
            DistrictValidation validationRules = new DistrictValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _districtService.Update(data);
                TempData["Update"] = "Semt Güncelleme İşlemi Başarıyla Gerçekleştirildi";
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
            return View();
        }
        public void DropDown()
        {
            List<SelectListItem> value = (from i in _cityService.GetList(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.CityName,
                                              Value = i.CityId.ToString()
                                          }).ToList();

            ViewBag.cities = value;
        }
    }
}
