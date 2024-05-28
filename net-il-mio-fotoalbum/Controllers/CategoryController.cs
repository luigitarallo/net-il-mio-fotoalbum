using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View(PhotoManager.GetAllCategories());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
        PhotoManager.InsertCategory(category);
        return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) 
        {
            if (PhotoManager.DeleteCategory(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
