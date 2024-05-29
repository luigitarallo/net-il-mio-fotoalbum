using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
