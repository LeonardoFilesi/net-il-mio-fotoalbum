using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")] // AGGIUNGERE SEMPRE /action
    [ApiController]
    public class FormContentsController : ControllerBase
    {
        private ImageContext _myDb;

        public FormContentsController(ImageContext myDb)
        {
            _myDb = myDb;
        }

        [HttpPost]
        public IActionResult StoreMessage([FromBody] FormContent formContent)
        {
            if (formContent == null)
            {
                return BadRequest("Dati non validi");
            }

            _myDb.FormContents.Add(formContent);
            _myDb.SaveChanges();

            return Ok(new { success = true });
        }
    }
}
