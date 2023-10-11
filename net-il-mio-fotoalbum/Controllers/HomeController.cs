using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Database;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImageContext _myDatabase;

        public HomeController(ILogger<HomeController> logger, ImageContext db)
        {
            _logger = logger;
            _myDatabase = db;
        }


        public IActionResult Index()
        {
            List<Image> images = _myDatabase.Images.Include(image => image.Categories).ToList<Image>();

            return View("Index", images);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UserIndex()
        {
            return View();
        }
    }
}
