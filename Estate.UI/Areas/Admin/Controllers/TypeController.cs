using Estate.BusinessLayer.Abstract;
using Estate.BusinessLayer.ValidationRules;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Estate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypeController : Controller
    {

        private readonly ITypeService _typeService;
        private readonly ISituationService _situationService;
        public TypeController(ITypeService typeService, ISituationService situationService)
        {
            _typeService = typeService;
            _situationService = situationService;

        }
        public IActionResult Index()
        {
            var list = _typeService.GetList(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntityLayer.Entities.Type data)
        {
            TypeValidation validationRules = new TypeValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _typeService.Add(data);
                TempData["Success"] = "Tip Ekleme İşlemi Başarıyla Gerçekleştirildi.";
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

        public IActionResult Update(int id)
        {
            DropDown();
            var type = _typeService.GetById(id);
            return View(type);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EntityLayer.Entities.Type data)
        {
            TypeValidation validationRules = new TypeValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _typeService.Update(data);
                TempData["Update"] = "Tip Ekleme İşlemi Başarıyla Gerçekleştirildi.";
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
            var type = _typeService.GetById(id);
            _typeService.Delete(type);
            return RedirectToAction("Index");
        }
        public void DropDown()
        {
            List<SelectListItem> value = (from i in _situationService.GetList(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = i.SituationName,
                                              Value = i.SituationId.ToString()
                                          }).ToList();

            ViewBag.status = value;

        }
    }
}
