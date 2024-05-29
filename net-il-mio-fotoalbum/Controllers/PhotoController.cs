using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View(PhotoManager.GetAllPhotos());
        }

        // Show Details
        public IActionResult Details(int id)
        {
            Photo photo = PhotoManager.GetPhotoById(id);
            if(photo!= null)
                return View(photo);
            else
                return View("Error");
        }

        // Create 
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
            PhotoManager.InsertPhoto(data.Photo, data.SelectedCategories);
            return RedirectToAction("Index");
        }

        // Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Photo photoToEdit = PhotoManager.GetPhotoById(id);
            if(photoToEdit == null)
                return NotFound();

           // List<string> selectedCategoriesIds = photoToEdit.Categories.Select(c => c.CategoryId.ToString()).ToList();
            PhotoFormModel model = new PhotoFormModel(photoToEdit);
            // model.SelectedCategories = selectedCategoriesIds;
            model.CreateCategories();
            return View(model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Photo.PhotoId = id;
                data.CreateCategories();
                return View("Edit", data);
            }
            var modifiedPhoto = PhotoManager.EditPhoto(id, data.Photo, data.SelectedCategories);

            if (modifiedPhoto)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (PhotoManager.DeletePhoto(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
