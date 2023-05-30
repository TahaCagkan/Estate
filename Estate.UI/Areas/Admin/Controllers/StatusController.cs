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
    public class StatusController : Controller
    {
        private readonly ISituationService _situationService;

        public StatusController(ISituationService situationService)
        {
            _situationService = situationService;


        }
        public IActionResult Index()
        {
            var list = _situationService.GetList(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Situation data)
        {
            SituationValidation validationRules = new SituationValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                _situationService.Add(data);
                TempData["Success"] = "Durum Ekleme İşlemi Başarıyla Gerçekleşti";
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
            var status = _situationService.GetById(id);
            _situationService.Delete(status);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var status = _situationService.GetById(id);
            return View(status);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Situation data)
        {
            SituationValidation validationRules = new SituationValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                _situationService.Update(data);
                TempData["Update"] = "Durum Güncelleme İşlemi Başarıyla Gerçekleştirildi.";
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
    }
}