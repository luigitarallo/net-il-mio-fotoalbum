using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View(PhotoManager.GetAllPhotos());
        }

        public IActionResult Details(int id)
        {
            Photo photo = PhotoManager.GetPhotoById(id);
            if(photo!= null)
                return View(photo);
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Create()
        {
            PhotoFormModel model = new PhotoFormModel();
            model.Photo = new Photo();
            model.CreateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel data)
        {
            if(!ModelState.IsValid)
            {
                data.CreateCategories();
                return View("Create", data);
            }
            

            data.SetImageFileFromFormFile();
            PhotoManager.IsertPhoto(data.Photo, data.SelectedCategories);
            return RedirectToAction("Index");
        }


    }
}
