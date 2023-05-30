using Estate.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Estate.UI.ViewComponents
{
    public class AdvertSlider:ViewComponent
    {
        private readonly IImagesService im;
        public AdvertSlider(IImagesService im)
        {
            this.im = im;
        }
        public IViewComponentResult Invoke()
        {
            var list = im.GetList(x => x.Status == true);
            return View(list);
        }
    }
}
