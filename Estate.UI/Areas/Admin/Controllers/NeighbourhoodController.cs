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
    public class NeighbourhoodController : Controller
    {
        INeighbourhoodService _neighbourhoodService;
        IDistrictService _districtService;

        public NeighbourhoodController(INeighbourhoodService neighbourhoodService, IDistrictService districtService)
        {
            _neighbourhoodService = neighbourhoodService;
            _districtService = districtService;
        }

        public IActionResult Index()
        {
            var list = _neighbourhoodService.GetList(x => x.Status == true);
            return View(list);
        }
        public IActionResult Create()
        {
            DropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Neighbourhood data)
        {
            NeighbourhoodValidation validationRules = new NeighbourhoodValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _neighbourhoodService.Add(data);
                TempData["Success"] = "Mahalle Ekleme İşlemi Başarıyla Gerçekleştirildi.";
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
            var neigh = _neighbourhoodService.GetById(id);
            _neighbourhoodService.Delete(neigh);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            DropDown();
            var neigh = _neighbourhoodService.GetById(id);
            return View(neigh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Neighbourhood data)
        {
            NeighbourhoodValidation validationRules = new NeighbourhoodValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                _neighbourhoodService.Update(data);
                TempData["Update"] = "Mahalle Güncelleme İşlemi Başarıyla Gerçekleştirildi";
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
            List<SelectListItem> value = (from i in _districtService.GetList(x => x.Status == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.DistrictName,
                                              Value = i.DistrictId.ToString()
                                          }).ToList();

            ViewBag.district = value;
        }
    }
}
