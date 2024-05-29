using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoWebController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPhotos(string? name)
        {
            if (name == null)
            {
                return Ok(PhotoManager.GetAllPublicPhotos());
            }
            return Ok(PhotoManager.GetAllPublicPhotos(name));
        }

        [HttpPost]
        public IActionResult MessagePost([FromBody] Message message)
        {
            PhotoManager.InsertMessage(message);
            return Ok();
        }
    }
}
