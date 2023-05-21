using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.ValidationRules;
using Estate.EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        public IActionResult Index()
        {
            var list = _cityService.GetList(x => x.Status == true);
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City data)
        {
            CityValidation validationRules = new CityValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                _cityService.Add(data);
                TempData["Success"] = "Şehir Ekleme İşlemi Başarıyla Gerçekleştirildi.";
                return RedirectToAction("Index");
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
        public IActionResult Delete(int id)
        {
            var city = _cityService.GetById(id);
            _cityService.Delete(city);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var update = _cityService.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City data)
        {
            CityValidation validationRules = new CityValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _cityService.Update(data);
                TempData["Update"] = "Şehir Güncelleme İşlemi Başarıyla Gerçekleştirildi.";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View(data);
        }
    }
}
