using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abcMarket.Controllers
{
    public class ImageController : Controller
    {
        [LoginAuthorize()]
        public ActionResult UploadImage()
        {
            return View();
        }

        [LoginAuthorize()]
        [HttpPost]
        public ActionResult Uploadimage(HttpPostedFileBase file)
        {
            ImageService.UserUploadimage(file);
            if (!string.IsNullOrEmpty(ImageService.ReturnAreaName))
                return RedirectToAction(ImageService.ReturnActionName, ImageService.ReturnControllerName, new { area = ImageService.ReturnAreaName });
            return RedirectToAction(ImageService.ReturnActionName, ImageService.ReturnControllerName);
        }

    }
}