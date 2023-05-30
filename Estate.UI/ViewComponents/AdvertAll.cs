
using Estate.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
//using X.PagedList;

namespace Estate.UI.ViewComponents
{
    public class AdvertAll:ViewComponent
    {
        private readonly IImagesService im;
        private readonly IAdvertService advert;
        public AdvertAll(IImagesService im, IAdvertService advert)
        {
            this.im = im;
            this.advert= advert;
        }
        public IViewComponentResult Invoke(int page=1)
        {
            //var list = advert.GetList(x => x.Status == true).ToPagedList(page, 3);
            var list = advert.GetList(x => x.Status == true);
            var images = im.GetList(x => x.Status == true);
            ViewBag.imgs = images;
            return View(list);
        }

    }
}
