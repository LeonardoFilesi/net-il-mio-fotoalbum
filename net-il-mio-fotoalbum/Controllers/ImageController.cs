using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageContext _myDatabase;

        public ImageController(ImageContext db)
        {
            _myDatabase = db;
        }



        // METODO copiato in HomeController
        // public IActionResult Index()
        // {
        //    List<Image> images = _myDatabase.Images.Include(image => image.Categories).ToList<Image>();
        //
        //    return View("Index", images);
        // }




        //================DETAILS==============
        public IActionResult Details(int id)
        {
            using (ImageContext db = new ImageContext())
            {
                Image? foundedImage = db.Images.Where(image => image.Id == id).Include(pizza => pizza.Categories).FirstOrDefault();

                if (foundedImage == null)
                {
                    return NotFound($"La foto con id:{id} non è stata trovata!");
                }
                else
                {
                    return View("Details", foundedImage);
                }
            }
        }



        //==============CREATE================
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            // OPERAZIONE NECESSARIA per passare al nuovo ImageFormModel solo le informazioni string Title e int Id delle istanze di Category
            List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
            List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
            foreach (Category category in databaseAllCategories)
            {
                allCategoriesSelectList.Add(
                    new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    });
            }

            ImageFormModel model = new ImageFormModel { Image = new Image(), Category = allCategoriesSelectList };

            return View("Create", model);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ImageFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
                }
                data.Category = allCategoriesSelectList;

                return View("Create", data);
            }

            data.Image.Categories = new List<Category>();

            if (data.SelectedCategoryId != null)
            {
                foreach (string categorySelectedid in data.SelectedCategoryId)
                {
                    int intCategorySelectedId = int.Parse(categorySelectedid);

                    Category? categoryInDataBase = _myDatabase.Categories.Where(category => category.Id == intCategorySelectedId).FirstOrDefault();

                    if (categoryInDataBase != null)
                    {
                        data.Image.Categories.Add(categoryInDataBase);
                    }
                }
            }

            _myDatabase.Images.Add(data.Image);
            _myDatabase.SaveChanges();
            return RedirectToAction("Index");
        }





        //==============UPDATE==================
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {

            Image? imageToEdit = _myDatabase.Images.Where(image => image.Id == id).Include(image => image.Categories).FirstOrDefault();

            if (imageToEdit == null)
            {
                return NotFound("La Foto non è stata trovata");
            }
            else
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = imageToEdit.Categories.Any(categoryAssociated => categoryAssociated.Id == category.Id)
                    });
                }

                ImageFormModel model = new ImageFormModel { Image = imageToEdit, Category = allCategoriesSelectList };
                return View("Update", model);
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, ImageFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                    });
                }
                data.Category = allCategoriesSelectList;

                return View("Update", data);
            }
            data.Image.Id = id;
            Image? imageToUpdate = _myDatabase.Images.Where(image => image.Id == id).Include(image => image.Categories).FirstOrDefault();

            if (imageToUpdate != null)
            {
                data.Image.Categories = new List<Category>();
                EntityEntry<Image> entryEntity = _myDatabase.Entry(imageToUpdate);

                if (data.SelectedCategoryId != null)
                {
                    foreach (string ingredientiSelectedid in data.SelectedCategoryId)
                    {
                        int intCategorySelectedId = int.Parse(ingredientiSelectedid);

                        Category? categoryInDataBase = _myDatabase.Categories.Where(category => category.Id == intCategorySelectedId).FirstOrDefault();

                        if (categoryInDataBase != null)
                        {
                            imageToUpdate.Categories.Add(categoryInDataBase);
                        }
                    }
                }
                entryEntity.CurrentValues.SetValues(data.Image);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Mi spiace, non sono state trovate Foto da aggiornare");
            }
        }




        //===========DELETE==========
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            Image? imageToDelete = _myDatabase.Images.Where(image => image.Id == id).FirstOrDefault();

            if (imageToDelete != null)
            {
                _myDatabase.Images.Remove(imageToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La Foto da eliminare non è stata trovata");
            }

        }
    }
}
