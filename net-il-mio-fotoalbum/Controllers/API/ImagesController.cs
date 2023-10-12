using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")] // AGGIUNGERE SEMPRE /action
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ImageContext _myDB;
        public ImagesController(ImageContext myDB)
        {
            _myDB = myDB;
        }



        //===========GET ALL IMAGES===============
        [HttpGet]
        public IActionResult GetImages()
        {
            using (ImageContext db = new ImageContext())
            {
                List<Image> images = db.Images.Include(image => image.ategories).ToList();
                return Ok(images);
            }
        }




        //===============SEARCHBYNAME================
        [HttpGet]
        public IActionResult SearchImages(string? search)
        {
            if (search == null)
            {
                // return BadRequest(new { Message = "Non hai inserito una stringa di ricerca" });
                return GetImages();
            }

            using (ImageContext db = new ImageContext())
            {
                List<Image> foundedImages = db.Images.Where(image => image.Title.ToLower().Contains(search.ToLower())).ToList();
                return Ok(foundedImages);
            }
        }
    }
}
