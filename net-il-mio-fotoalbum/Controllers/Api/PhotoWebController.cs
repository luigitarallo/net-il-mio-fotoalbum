﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;

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
    }
}
